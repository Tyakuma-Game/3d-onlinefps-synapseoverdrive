using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃X�e�[�^�X�l���Ǘ�����N���X
/// </summary>
public class PlayerStatus
{
    [Tooltip("Player��HP�ő�l")]
    [SerializeField] int PLAYER_MAX_HP = 100;

    [Tooltip("Player�̕������x")]
    [SerializeField] float PLAYER_WALK_SPEED = 4f;

    [Tooltip("Player�̑��葬�x")]
    [SerializeField] float PLAYER_RUN_SPEED = 8f;

    [Tooltip("Player�̃W�����v��")]
    [SerializeField] Vector3 PLAYER_JUMP_FORTH = new Vector3(0, 3f, 0);

    [Tooltip("Player�̉�]���x")]
    [Range(1f, 10f)]
    [SerializeField] float ROTATION_SPEED = 5.0f;


    //[Tooltip("���_�ړ��̑��x")]
    //[SerializeField] float MOUSE_SENSITIVITY = 1f;

    //[Tooltip("���_�̏㉺��]�͈�")]
    //[SerializeField] float VERTICAL_ROTATION_RANGE = 60f;

    int currentHp;                          // ���݂�HP
    float activeMoveSpeed;                  // ���݂̈ړ����x
    Vector3 jumpForth;                      // �W�����v��
    PlayerAnimationState animationState;    // ���݂̏��

    /// <summary>
    /// HP�ő�l
    /// </summary>
    public int MAX_HP
    {
        get { return PLAYER_MAX_HP; }
    }

    /// <summary>
    /// ��]���x
    /// </summary>
    public float ROTA_SPEED
    {
        get { return ROTATION_SPEED; }
    }

    /// <summary>
    /// ���݂�HP
    /// </summary>
    public int CurrentHP
    {
        get { return currentHp; }
    }

    /// <summary>
    /// ���݂̈ړ����x
    /// </summary>
    public float ActiveMoveSpeed
    {
        get { return activeMoveSpeed; }
    }

    /// <summary>
    /// �W�����v��
    /// </summary>
    public Vector3 JumpForth
    {
        get { return jumpForth; }
    }

    /// <summary>
    /// ���݂̃A�j���[�V�������
    /// </summary>
    public PlayerAnimationState AnimationState
    {
        get { return animationState; }
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
    /// �X�e�[�^�X������
    /// </summary>
    public void Init()
    {
        currentHp = PLAYER_MAX_HP;                   // �̗�
        activeMoveSpeed = PLAYER_WALK_SPEED;         // �ړ����x
        jumpForth = PLAYER_JUMP_FORTH;               // �W�����v��
        animationState = PlayerAnimationState.Idol;  // ���
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
        activeMoveSpeed = PLAYER_WALK_SPEED;
        animationState = PlayerAnimationState.Walk;
    }

    /// <summary>
    /// �����Ԃ֑J��
    /// </summary>
    public void IsRunning()
    {
        activeMoveSpeed = PLAYER_RUN_SPEED;
        animationState = PlayerAnimationState.Run;
    }
}