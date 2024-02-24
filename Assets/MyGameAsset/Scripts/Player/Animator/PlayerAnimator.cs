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
    

    string hashDamage = "Damage";
    string hashHP = "HP";
    string hashAttackType = "AttackType";
    string hashAttack = "Attack";
    string hashMoveSpeed = "MoveSpeed";
    string hashIsGround = "IsGround";
    string hashWeaponChange = "WeaponChange";

    void Start()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        // �����o�^
        PlayerMove.OnSpeedChanged += UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange += OnGroundContactChange;
    }

    void OnDestroy()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        // ��������
        PlayerMove.OnSpeedChanged -= UpdateMoveSpeed;
        PlayerJump.OnGroundContactChange -= OnGroundContactChange;
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

    /// <summary>
    /// ��e���̃g���K�[�𗧂Ă�
    /// </summary>
    public void Damage()
    {
        playerAnimator.SetTrigger(hashDamage);
    }
}