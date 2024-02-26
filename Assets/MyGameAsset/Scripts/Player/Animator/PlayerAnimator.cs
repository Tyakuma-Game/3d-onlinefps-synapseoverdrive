using Photon.Pun;
using UnityEngine;

/// <summary>
/// �A�j���[�V�������Ǘ�����N���X
/// </summary>
public class PlayerAnimator : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Animator playerAnimator;

    // �A�j���[�V�����p�����[�^�̃n�b�V��
    const string HASH_DAMAGE        = "Damage";
    const string HASH_HP            = "HP";
    const string HASH_ATTACK_TYPE   = "AttackType";
    const string HASH_ATTACK        = "Attack";
    const string HASH_MOVE_SPEED    = "MoveSpeed";
    const string HASH_IS_GROUND     = "IsGround";
    const string HASH_WEAPON_CHANGE = "WeaponChange";

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // �����o�^
        PlayerMove.OnSpeedChanged += UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange += OnGroundContactChange;
        PlayerEvent.onDamage += OnDamage;

        PlayerGunController.OnWeaponChangeCallback += OnWeaponChange;
        PlayerGunController.OnGunShotAnimationCallback += OnGunShot;

        PlayerController.OnHPChanged += SetCurrentHP;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // ��������
        PlayerMove.OnSpeedChanged -= UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange -= OnGroundContactChange;
        PlayerEvent.onDamage -= OnDamage;

        PlayerGunController.OnWeaponChangeCallback -= OnWeaponChange;
        PlayerGunController.OnGunShotAnimationCallback -= OnGunShot;

        PlayerController.OnHPChanged -= SetCurrentHP;
    }

    /// <summary>
    /// �ړ����x�̍X�V
    /// </summary>
    /// <param name="speed">�ړ����x</param>
    void UpdateMoveSpeed(float speed) =>
        playerAnimator.SetFloat(HASH_MOVE_SPEED, speed, 0.1f, Time.deltaTime);

    /// <summary>
    /// �n�ʐڐG��Ԃ̍X�V
    /// </summary>
    /// <param name="isGround">�n�ʐڐG���</param>
    void OnGroundContactChange(bool isGround) =>
        playerAnimator.SetBool(HASH_IS_GROUND, isGround);

    /// <summary>
    /// ��e
    /// </summary>
    void OnDamage() =>
        playerAnimator.SetTrigger(HASH_DAMAGE);

    /// <summary>
    /// HP�̍X�V
    /// </summary>
    /// <param name="hp">HP</param>
    void SetCurrentHP(int hp) =>
        playerAnimator.SetInteger(HASH_HP, hp);

    /// <summary>
    /// �������
    /// </summary>
    void OnWeaponChange() =>
        playerAnimator.SetTrigger(HASH_WEAPON_CHANGE);

    /// <summary>
    /// �e����
    /// </summary>
    /// <param name="attackType">�e�̎��</param>
    void OnGunShot(int attackType)
    {
        playerAnimator.SetInteger(HASH_ATTACK_TYPE, attackType);
        playerAnimator.SetTrigger(HASH_ATTACK);
    }
}