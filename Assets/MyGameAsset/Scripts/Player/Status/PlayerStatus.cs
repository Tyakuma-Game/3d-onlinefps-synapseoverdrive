using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃X�e�[�^�X�l���Ǘ�����N���X
/// </summary>
public class PlayerStatus: MonoBehaviour
{
    [Tooltip("�v���C���[�̒萔�N���X")]
    [SerializeField] PlayerConstants playerConstants;

    [Tooltip("HP�c��")]
    int currentHp;

    [Tooltip("�ړ����x")]
    float activeMoveSpeed;

    [Tooltip("�W�����v��")]
    Vector3 activeJumpForth;

    [Tooltip("���݂̃A�j���[�V�������")]
    PlayerAnimationState animationState;

    
    /// <summary>
    /// �X�e�[�^�X������
    /// </summary>
    public void Init()
    {
        currentHp = playerConstants.MaxHP;            // �̗�
        activeMoveSpeed = playerConstants.WalkSpeed;  // �ړ����x
        activeJumpForth = playerConstants.JumpForce;  // �W�����v��
        animationState = PlayerAnimationState.Idol;   // ���
    }

    /// <summary>
    /// �_���[�W����
    /// </summary>
    /// <param name="damage"></param>
    public void OnDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
        }
    }

    /// <summary>
    /// �ҋ@��Ԃ֑J��
    /// </summary>
    public void IsIdol()
    {
        animationState = PlayerAnimationState.Idol;
    }

    /// <summary>
    /// ������Ԃ֑J��
    /// </summary>
    public void IsWalking()
    {
        activeMoveSpeed = playerConstants.WalkSpeed;
        animationState = PlayerAnimationState.Walk;
    }

    /// <summary>
    /// �����Ԃ֑J��
    /// </summary>
    public void IsRunning()
    {
        activeMoveSpeed = playerConstants.RunSpeed;
        animationState = PlayerAnimationState.Run;
    }


    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // �Q�b�^�[
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    /// <summary>
    /// PLAYER�̒萔�擾
    /// </summary>
    public PlayerConstants Constants
    {
        get { return playerConstants; }
    }

    /// <summary>
    /// ���݂�HP��
    /// </summary>
    public int CurrentHP { get { return currentHp; } }

    /// <summary>
    /// ���݂̈ړ����x
    /// </summary>
    public float ActiveMoveSpeed { get { return activeMoveSpeed; } }

    /// <summary>
    /// ���݂̃W�����v��
    /// </summary>
    public Vector3 ActiveJumpForth { get { return activeJumpForth; } }

    /// <summary>
    /// ���݂̃A�j���[�V�������
    /// </summary>
    public PlayerAnimationState AnimationState
    {
        get { return animationState; }
    }
}