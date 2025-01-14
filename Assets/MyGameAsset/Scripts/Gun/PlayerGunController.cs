using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.InputSystem;

// TODO:
// 大規模に各スクリプトへの分割を行う！

/// <summary>
/// プレイヤーの銃管理クラス
/// </summary>
public class PlayerGunController : MonoBehaviourPunCallbacks
{
    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
    //　改善部分
    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
    [SerializeField] CameraController cameraController;     // アクションに統合する感じでリファクタリングする!

    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

    [Header(" データ関連 ")]
    [SerializeField] GunData[] gunDates;                // 銃データ一覧
    List<GameObject> guns = new List<GameObject>();     // 銃Data管理用
    List<int> ammunition = new List<int>();             // 現在の所持弾薬
    List<int> ammoClip = new List<int>();               // マガジン内の弾薬
    GunType selectedGunType = GunType.HandGun;          // 現在選択中の銃種類
    float shotTimer;                                    // 射撃間隔

    [Header(" 見た目関連 ")]
    [SerializeField] GameObject[] gunsHolder;       // 自分視点の銃
    [SerializeField] GameObject[] otherGunsHolder;  // 相手視点の銃


    void Start()
    {
        // 使用する銃ホルダー選択（自分視点か相手視点かを基に決定）
        GameObject[] selectedGunsHolder = photonView.IsMine ? gunsHolder : otherGunsHolder;

        // 銃登録
        foreach (GameObject gun in selectedGunsHolder)
            guns.Add(gun);

        // 自身の場合のみ、弾薬とマガジンの初期化
        if (photonView.IsMine)
        {
            foreach (var gun in gunDates)
            {
                ammunition.Add(gun.MaxAmmunition);  // 所持弾薬
                ammoClip.Add(gun.MaxAmmoClip);      // マガジン内弾薬
            }

            // ズーム関連処理登録
            InputManager.Controls.Gun.Zoom.started += ZoomIn;
            InputManager.Controls.Gun.Zoom.canceled += ZoomOut;

            // 武器交換処理登録
            InputManager.Controls.Gun.WeaponChange.performed += SwitchingGuns;

            // 発射処理登録
            InputManager.Controls.Gun.Shot.started += Shot;
            InputManager.Controls.Gun.Shot.canceled += Shot;
        }

        // 銃の表示切替
        ChangeActiveGun();
    }
    void OnDestroy()
    {
        // 自身が操作するオブジェクトでなければ処理をスキップ
        if (!photonView.IsMine)
            return;

        // ズーム関連処理解除
        InputManager.Controls.Gun.Zoom.started -= ZoomIn;
        InputManager.Controls.Gun.Zoom.canceled -= ZoomOut;

        // 武器交換処理解除
        InputManager.Controls.Gun.WeaponChange.performed -= SwitchingGuns;

        // 発射処理解除
        InputManager.Controls.Gun.Shot.started -= Shot;
        InputManager.Controls.Gun.Shot.canceled -= Shot;
    }

    void Update()
    {
        // 自分以外なら処理終了
        if (!photonView.IsMine)
            return;

        // リロード関数
        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        // 弾薬テキスト更新
        UIManager.instance.SettingBulletsText(gunDates[(int)selectedGunType].MaxAmmoClip,
            ammoClip[(int)selectedGunType], ammunition[(int)selectedGunType]);
    }

    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
    //　武器切り替え
    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

    // TODO:リファクタする！

    /// <summary>
    /// 武器変更のコールバック処理
    /// </summary>
    public static Action OnWeaponChangeCallback;

    /// <summary>
    /// 銃の切り替えキー入力を検知
    /// </summary>
    void SwitchingGuns(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        int direction = 0;

        // y成分を使って方向を決定（上スクロールまたは十字キー上で次の武器に、下スクロールまたは十字キー下で前の武器に）
        if (inputVector.y > 0)
        {
            // 次の武器へ
            direction = 1;
        }
        else if (inputVector.y < 0)
        {
            // 前の武器へ
            direction = -1;
        }

        // 武器の切り替えを行うメソッドを呼び出す（既存のロジックを利用）
        if (direction != 0)
        {
            UpdateSelectedGunType(direction, Enum.GetValues(typeof(GunType)).Length);
        }
    }

    /// <summary>
    /// 銃のタイプを更新し、変更を通知
    /// </summary>
    void UpdateSelectedGunType(int direction, int gunCount)
    {
        // 銃のタイプを更新
        selectedGunType += direction;

        // 範囲外にならないように調整
        if (selectedGunType < 0)
        {
            selectedGunType = (GunType)(gunCount - 1);
        }
        else if ((int)selectedGunType >= gunCount)
        {
            selectedGunType = GunType.HandGun;
        }

        // 更新後の銃のタイプを設定し、通知
        SetGunTypeAndNotify(selectedGunType);
    }

    /// <summary>
    /// 銃のタイプを設定し、変更を通知する
    /// </summary>
    void SetGunTypeAndNotify(GunType gunType)
    {
        OnWeaponChangeCallback?.Invoke();

        selectedGunType = gunType;
        photonView.RPC("SetGun", RpcTarget.All, (int)selectedGunType);
    }

    /// <summary>
    /// 銃の切り替え処理
    /// </summary>
    [PunRPC]
    public void SetGun(int gunNo)
    {
        //銃の切り替え
        if (gunNo < Enum.GetValues(typeof(GunType)).Length)
        {
            //銃の番号をセット
            selectedGunType = (GunType)gunNo;

            // 指定時間後切り替え
            StartCoroutine(DelayedSwitchGun(1f));
        }
    }

    /// <summary>
    /// Photonで呼び出す武器変更処理
    /// </summary>
    /// <param name="waitTime">待ち時間</param>
    IEnumerator DelayedSwitchGun(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ChangeActiveGun();
    }

    /// <summary>
    /// 銃の表示切り替え
    /// </summary>
    void ChangeActiveGun()
    {
        // 全ての銃を非表示に
        foreach (GameObject gun in guns)
            gun.gameObject.SetActive(false);

        // 選択中の銃のみ表示する
        guns[(int)selectedGunType].SetActive(true);
    }

    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
    //　ズーム処理
    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

    /// <summary>
    /// 銃のズーム状態変更時のイベントハンドラ
    /// </summary>
    public static Action<bool> OnGunZoomStateChanged;

    /// <summary>
    /// ズーム開始
    /// </summary>
    void ZoomIn(InputAction.CallbackContext context)
    {
        OnGunZoomStateChanged?.Invoke(true);
        CameraEvents.OnZoomStateChanged?.Invoke(gunDates[(int)selectedGunType].AdsZoom, gunDates[(int)selectedGunType].AdsSpeed);
    }

    /// <summary>
    /// ズーム終了
    /// </summary>
    void ZoomOut(InputAction.CallbackContext context)
    {
        OnGunZoomStateChanged?.Invoke(false);
        CameraEvents.OnZoomStateChanged?.Invoke(60f, gunDates[(int)selectedGunType].AdsSpeed);
    }


    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
    //　発射処理とリロード
    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

    /// <summary>
    /// 武器攻撃のコールバック処理
    /// </summary>
    public static Action<int> OnGunShotAnimationCallback;

    [SerializeField] private float autoFireRate = 0.1f; // 連射間隔

    // 連射コルーチン
    IEnumerator AutoFireCoroutine()
    {
        while (true)
        {
            // Button型はfloatでプレス状態を表すことが多い（押されている=1、押されていない=0）
            if (Time.time > shotTimer)
            {
                // 弾薬の残りがあるか判定
                if (ammoClip[(int)selectedGunType] == 0)
                {
                    // オートリロード
                    Reload();

                    // 処理中断
                    yield break;
                }

                // アニメーション
                OnGunShotAnimationCallback?.Invoke((int)selectedGunType);

                //銃の発射処理
                FiringBullet();
            }

            yield return new WaitForSeconds(autoFireRate);
        }
    }

    void Shot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // 連射コルーチン開始
            StartCoroutine("AutoFireCoroutine");
        }
        else if (context.canceled)
        {
            // 連射コルーチン停止
            StopCoroutine("AutoFireCoroutine");
        }
    }

    /// <summary>
    /// 弾丸の発射
    /// </summary>
    void FiringBullet()
    {
        //Ray(光線)をカメラの中央から設定
        Vector2 pos = new Vector2(.5f, .5f);
        Ray ray = cameraController.GenerateRay(pos);

        //レイを発射
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //プレイヤーにぶつかった場合
            if (hit.collider.gameObject.tag == "Player")
            {
                //血のエフェクトをネットワーク上に生成
                PhotonNetwork.Instantiate(gunDates[(int)selectedGunType].PlayerHitEffect.name, hit.point, Quaternion.identity);

                // ヒット関数を全プレイヤーで呼び出して撃たれたプレイヤーのHPを同期
                hit.collider.gameObject.GetPhotonView().RPC("Hit",
                    RpcTarget.All,
                    gunDates[(int)selectedGunType].ShotDamage,
                    photonView.Owner.NickName,
                    PhotonNetwork.LocalPlayer.ActorNumber);
            }
            else
            {
                //弾痕エフェクト生成 
                GameObject bulletImpactObject = Instantiate(gunDates[(int)selectedGunType].NonPlayerHitEffect,
                    hit.point + (hit.normal * .002f),                   //オブジェクトから少し浮かしてちらつき防止
                    Quaternion.LookRotation(hit.normal, Vector3.up));   //直角の方向を返してその方向に回転させる

                //時間経過で削除
                Destroy(bulletImpactObject, 10f);
            }
        }

        //射撃間隔を設定
        shotTimer = Time.time + gunDates[(int)selectedGunType].ShootInterval;

        //選択中の銃の弾薬減らす
        ammoClip[(int)selectedGunType]--;
    }


    /// <summary>
    /// リロード
    /// </summary>
    void Reload()
    {
        int gunTypeIndex = (int)selectedGunType;

        // リロード補充分の弾数計算
        int ammoToReload = Math.Min(gunDates[gunTypeIndex].MaxAmmoClip - ammoClip[gunTypeIndex], ammunition[gunTypeIndex]);

        // 弾薬がある場合のみリロード
        if (ammoToReload > 0)
        {
            //　TODO: 時間がある際にここを調整する
            // アニメーション
            // gunAnimator.SetTrigger("Reload");

            // 所持弾薬を更新と弾薬装填
            ammunition[gunTypeIndex] -= ammoToReload;
            ammoClip[gunTypeIndex] += ammoToReload;
        }
    }
}