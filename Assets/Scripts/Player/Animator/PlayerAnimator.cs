using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̏�Ԉꗗ
/// </summary>
public enum PlayerAnimationState
{
    Idol,   // �ҋ@���
    Walk,   // �������
    Run,    // ������
    Jump    // �W�����v���
}

/// <summary>
/// �v���C���[�̃A�j���[�V�����Ǘ��N���X
/// </summary>
public class PlayerAnimator : MonoBehaviour,IPlayerAnimator
{
    [Tooltip("Player�̃A�j���[�^�[")]
    [SerializeField] Animator animator;

    /// <summary>
    /// �A�j���[�V�����̍X�V����
    /// </summary>
    /// <param name="playerAnimationState">���݂̃A�j���[�V�������</param>
    public void AnimationUpdate(PlayerAnimationState playerAnimationState)
    {
        //��������
        animator.SetBool("walk", playerAnimationState == PlayerAnimationState.Walk);

        //���蔻��
        animator.SetBool("run", playerAnimationState == PlayerAnimationState.Run);
    }
}



//�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
// �����쒆��Animator
//�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

//public interface ICharacterAnimationParam
//{
//    void Apply(float speed,float fallSpeed,bool isGround, int hp);
//    void Attack(ShotType type);
//    void Damage();
//}
//public enum ShotType : int
//{
//    OneShot = 0,    // �ꔭ
//    ManyShot,       // ����
//}

//public class AAA : ICharacterAnimationParam
//{
//    Animator animator;
//    int hashDamage;
//    int hashAttackType;
//    int hashHP;
//    bool hashIsGround;

//    public void Apply(float speed, float fallSpeed,bool isGround,int hp)
//    {
//        animator.SetInteger(hashHP,hp);
//        animator.SetBool("hashIsGround",isGround);
//        animator.SetFloat(hash)
//    }

//    public void Attack(ShotType type)
//    {
//        animator.SetInteger(hashAttackType , (int)type);
//        animator.SetTrigger(hashAttackType);
//    }


//    public void Damage()
//    {
//        animator.SetTrigger(hashDamage);
//    }
//}
//�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/