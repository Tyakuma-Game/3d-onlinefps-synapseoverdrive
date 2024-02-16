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
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    
    // �_���[�W�֘A
    string hashDamage = "Damage";
    string hashHP = "HP";

    // �U���֘A
    string hashAttackType = "AttackType";
    string hashAttack = "Attack";

    // �ړ��֘A
    string hashMoveSpeed = "MoveSpeed";

    // Jump�֘A
    string hashIsGround = "IsGround";

    // �������
    string hashWeaponChange = "WeaponChange";

    /// <summary>
    /// ���݂̈ړ����x���X�V
    /// </summary>
    /// <param name="moveSpeed">�ړ����x</param>
    public void UpdateMoveSpeed(float moveSpeed)
    {
        playerAnimator.SetFloat(hashMoveSpeed, moveSpeed, 0.1f, Time.deltaTime);
    }

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
    /// �n�ʂɒ��n���Ă��邩��ݒ�
    /// </summary>
    /// <param name="isGround">���ݒ��n���Ă��邩</param>
    public void IsGround(bool isGround)
    {
        playerAnimator.SetBool(hashIsGround, isGround);
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