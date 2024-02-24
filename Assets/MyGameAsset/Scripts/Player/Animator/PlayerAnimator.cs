using Photon.Pun;
using UnityEngine;

/// <summary>
/// �U���̎��
/// </summary>
public enum AttackType : int
{
    Short = 0,
    Normal = 1,
    Power = 2,
}

/// <summary>
/// �A�j���[�V�������Ǘ�����N���X
/// </summary>
public class PlayerAnimator : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Animator playerAnimator;
    
    // �e�v�f�̐ڑ��p�X
    string hashDamage = "Damage";
    string hashHP = "HP";
    string hashAttackType = "AttackType";
    string hashAttack = "Attack";
    string hashMoveSpeed = "MoveSpeed";
    string hashIsGround = "IsGround";
    string hashWeaponChange = "WeaponChange";

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // �����o�^
        PlayerMove.OnSpeedChanged += UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange += OnGroundContactChange;
        PlayerEvent.onDamage += OnDamage;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // ��������
        PlayerMove.OnSpeedChanged -= UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange -= OnGroundContactChange;
        PlayerEvent.onDamage -= OnDamage;
    }

    /// <summary>
    /// ���݂̈ړ����x�X�V
    /// </summary>
    /// <param name="speed">���݂̈ړ����x</param>
    void UpdateMoveSpeed(float speed) =>
        playerAnimator.SetFloat(hashMoveSpeed, speed, 0.1f, Time.deltaTime);

    /// <summary>
    /// �n�ʂɐڐG���Ă��邩�ǂ����̍X�V
    /// </summary>
    /// <param name="isGround">�ڐG���Ă��邩</param>
    void OnGroundContactChange(bool isGround) =>
        playerAnimator.SetBool(hashIsGround, isGround);

    /// <summary>
    /// ��e
    /// </summary>
    void OnDamage() =>
        playerAnimator.SetTrigger(hashDamage);

    /// <summary>
    /// �������
    /// </summary>
    void OnWeaponChange() =>
        playerAnimator.SetTrigger(hashWeaponChange);

    

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // ���t�@�N�^�����O��
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    /// <summary>
    /// ���݂�HP������
    /// </summary>
    /// <param name="hp">���݂�HP</param>
    public void SetCurrentHP(int hp)
    {
        playerAnimator.SetInteger(hashHP, hp);
    }

    /// <summary>
    /// ��������̃g���K�[�𗧂Ă�
    /// </summary>
    public void IsWeaponChange()
    {
        playerAnimator.SetTrigger(hashWeaponChange);
    }

    /// <summary>
    /// �U���A�j���[�V�������Đ�
    /// </summary>
    /// <param name="typeID">�U���̎��</param>
    public void Attack(AttackType typeID)
    {
        playerAnimator.SetInteger(hashAttackType, (int)typeID);
        playerAnimator.SetTrigger(hashAttack);
    }
}