using Guns;
using Photon.Pun;
using UnityEngine;

public class GunAnimator : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Animator gunAnimator;

    // �A�N�Z�X�l
    const string hashAttackType     = "AttackType";
    const string hashAttack         = "Attack";
    const string hashMoveSpeed      = "MoveSpeed";
    const string hashIsZoom         = "IsZoom";
    const string hashWeaponChange   = "WeaponChange";

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // �����o�^
        PlayerMove.OnSpeedChanged += UpdateMoveSpeed;

        PlayerGunController.OnGunZoomStateChanged += GunZoomStateChange;
        PlayerGunController.OnWeaponChangeCallback += OnWeaponChange;
        PlayerGunController.OnGunShotAnimationCallback += OnGunShot;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // ��������
        PlayerMove.OnSpeedChanged -= UpdateMoveSpeed;

        PlayerGunController.OnGunZoomStateChanged -= GunZoomStateChange;
        PlayerGunController.OnWeaponChangeCallback -= OnWeaponChange;
        PlayerGunController.OnGunShotAnimationCallback -= OnGunShot;
    }

    /// <summary>
    /// ���݂̈ړ����x�X�V
    /// </summary>
    /// <param name="speed">���݂̈ړ����x</param>
    void UpdateMoveSpeed(float speed) =>
        gunAnimator.SetFloat(hashMoveSpeed, speed, 0.1f, Time.deltaTime);

    /// <summary>
    /// �Y�[����ԕύX
    /// </summary>
    /// <param name="isZoom">�Y�[�����Ȃ̂��ǂ���</param>
    void GunZoomStateChange(bool isZoom) =>
        gunAnimator.SetBool(hashIsZoom, isZoom);

    /// <summary>
    /// �������
    /// </summary>
    void OnWeaponChange() =>
        gunAnimator.SetTrigger(hashWeaponChange);

    /// <summary>
    /// �e����
    /// </summary>
    /// <param name="attackType">�e�̎��</param>
    void OnGunShot(int attackType)
    {
        gunAnimator.SetInteger(hashAttackType, attackType);
        gunAnimator.SetTrigger(hashAttack);
    }
}