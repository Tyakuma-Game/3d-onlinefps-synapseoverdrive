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
/// �A�j���[�V�����̏����Ɋւ���C���^�[�t�F�[�X
/// </summary>
public interface ICharacterAnimationParam
{
    void Apply(float speed, float fallSpedd, bool isGround, int hp);

    void Attack(AttackType type);

    void Damage();
}



/// <summary>
/// �A�j���[�V�������Ǘ�����N���X
/// </summary>
public class TestAnimatorController : MonoBehaviour
{

    [SerializeField] Animator animator;
    
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


    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    /// <summary>
    /// �A�j���[�V�������x����
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void TestMove(float moveSpeed)
    {
        animator.SetFloat(hashMoveSpeed, moveSpeed, 0.1f, Time.deltaTime);
    }

    
    public void TestSetHP(int hp)
    {
        animator.SetInteger(hashHP, hp);
    }

    public void TestWeaponChange()
    {
        animator.SetTrigger(hashWeaponChange);
    }

    public void TestIsGround(bool isGround)
    {
        animator.SetBool(hashIsGround, isGround);
    }

    public void Attack(AttackType typeID)
    {
        animator.SetInteger(hashAttackType, (int)typeID);
        animator.SetTrigger(hashAttack);
    }

    public void Damage()
    {
        animator.SetTrigger(hashDamage);
    }
}