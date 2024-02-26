using Guns;
using Photon.Pun;
using UnityEngine;

public class GunAnimator : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Animator gunAnimator;

    // �A�N�Z�X�p�̃n�b�V���l
    string hashAttackType = "AttackType";
    string hashAttack = "Attack";
    string hashMoveSpeed = "MoveSpeed";
    string hashIsZoom = "IsZoom";
    string hashWeaponChange = "WeaponChange";

    void Start()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
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
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
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