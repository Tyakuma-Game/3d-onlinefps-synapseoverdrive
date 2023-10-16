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
    /// <param name="playerAnimationState">���ݑI�𒆂̃A�j���[�V����</param>
    public void AnimationUpdate(PlayerAnimationState playerAnimationState)
    {
        //��������
        animator.SetBool("walk", playerAnimationState == PlayerAnimationState.Walk);

        //���蔻��
        animator.SetBool("run", playerAnimationState == PlayerAnimationState.Run);
    }
}