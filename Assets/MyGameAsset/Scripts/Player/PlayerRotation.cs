using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Playerの回転を管理するクラス
/// </summary>
public class PlayerRotation : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] RotationSettings rotationSettings; //TODO： リファクタリングする！

    Vector2 rotationInput = Vector2.zero;
    InputAction lookAction;

    void Awake()
    {
        // 処理登録
        PlayerEvent.OnPlayerInstantiated += HandlePlayerInstantiated;
    }

    void OnDestroy()
    {
        // 処理解除
        PlayerEvent.OnPlayerInstantiated -= HandlePlayerInstantiated;
        if (lookAction != null)
        {
            lookAction.started -= OnLookPerformed;
            lookAction.performed -= OnLookPerformed;
            lookAction.canceled -= OnLookCanceled;
        }
    }

    void FixedUpdate()
    {
        if (rotationInput == Vector2.zero)
            return;

        Rotate(rotationInput);
    }

    /// <summary>
    /// プレイヤーがインスタンス化された際に呼ばれる処理
    /// </summary>
    void HandlePlayerInstantiated()
    {
        // 取得
        lookAction = InputManager.Controls.Player.Look;

        // メソッドをイベントに登録
        lookAction.started += OnLookPerformed;
        lookAction.performed += OnLookPerformed;
        lookAction.canceled += OnLookCanceled;
    }

    /// <summary>
    /// 入力時に傾き値を受け取る
    /// </summary>
    void OnLookPerformed(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 入力終了時に呼び出す
    /// </summary>
    void OnLookCanceled(InputAction.CallbackContext context)
    {
        rotationInput = Vector2.zero;
    }

    /// <summary>
    /// Playerの回転処理
    /// </summary>
    /// <param name="rotaInput">回転のための入力情報</param>
    void Rotate(Vector2 rotaInput)
    {
        // 計算
        Vector2 rotation = new Vector2(rotaInput.x * rotationSettings.rotationSpeed, 0f);

        //横回転を反映
        transform.rotation = Quaternion.Euler           // オイラー角としての角度が返される
                (transform.eulerAngles.x,
                transform.eulerAngles.y + rotation.x,   // x軸の入力を足す
                transform.eulerAngles.z);
    }
}