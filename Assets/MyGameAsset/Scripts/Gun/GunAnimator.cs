using Photon.Pun;
using UnityEngine;

/// <summary>
/// �e�̃A�j���[�V�����Ɋւ���Ǘ��N���X
/// </summary>
public class GunAnimator : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Animator gunAnimator;

    // �A�j���[�V�����p�����[�^�̃n�b�V��
    const string HASH_ATTACK_TYPE   = "AttackType";
    const string HASH_ATTACK        = "Attack";
    const string HASH_MOVE_SPEED    = "MoveSpeed";
    const string HASH_IS_ZOOM       = "IsZoom";
    const string HASH_WEAPON_CHANGE = "WeaponChange";

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
        gunAnimator.SetFloat(HASH_MOVE_SPEED, speed, 0.1f, Time.deltaTime);

    /// <summary>
    /// �Y�[����ԕύX
    /// </summary>
    /// <param name="isZoom">�Y�[�����Ȃ̂��ǂ���</param>
    void GunZoomStateChange(bool isZoom) =>
        gunAnimator.SetBool(HASH_IS_ZOOM, isZoom);

    /// <summary>
    /// �������
    /// </summary>
    void OnWeaponChange() =>
        gunAnimator.SetTrigger(HASH_WEAPON_CHANGE);

    /// <summary>
    /// �e����
    /// </summary>
    /// <param name="attackType">�e�̎��</param>
    void OnGunShot(int attackType)
    {
        gunAnimator.SetInteger(HASH_ATTACK_TYPE, attackType);
        gunAnimator.SetTrigger(HASH_ATTACK);
    }
}