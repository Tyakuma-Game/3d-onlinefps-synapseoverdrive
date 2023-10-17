using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Playables;

public struct PlayerStatus
{
    public float activeMoveSpeed;                       // ���݂̈ړ����x
    public Vector3 jumpForth;                           // �W�����v��
    public PlayerAnimationState playerAnimationState;   // ���݂̏��
}

/// <summary>
/// Player�Ǘ��N���X
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    [Header("�萔")]
    [Tooltip("Player��HP�ő�l")]
    [SerializeField] int PLAYER_MAX_HP = 100;   //�ő�HP
    int currentHp;                              //���݂�HP

    [Header("�Q��")]

    [Tooltip("Player�̎��_�ړ��Ɋւ���N���X")]
    [SerializeField] PlayerViewpointShift playerViewpointShift;

    [Tooltip("Player�̏e�Ǘ��N���X")]
    [SerializeField] PlayerGunController playerGunController;

    UIManager uIManager;        //UI�Ǘ�
    SpawnManager spawnManager;  //�X�|�[���}�l�[�W���[�Ǘ�
    GameManager gameManager;    //�Q�[���}�l�[�W���[

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    //�@����������Program
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    [Tooltip("�v���C���[��Status���")]
    PlayerStatus playerStatus;


    // Player�@�\
    [Tooltip("Player�̈ړ�����")]
    IPlayerMove playerMove;

    [Tooltip("Player�̃W�����v����")]
    IPlayerJump playerJump;

    [Tooltip("Player�̃A�j���[�V��������")]
    IPlayerAnimator playerAnimator;

    [Tooltip("���n���Ă��邩���菈��")]
    PlayerLandDetector playerLandDetector;


    // ���̓V�X�e��
    [Tooltip("�L�[�{�[�h�̓��͏���")]
    IKeyBoardInput keyBoardInput;


    [SerializeField] float PLAYER_WALK_SPEED = 4f;
    [SerializeField] float PLAYER_RUN_SPEED = 8f;
    [SerializeField] Vector3 PLAYER_JUMP_FORTH = new Vector3(0,3f,0);

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    private void Awake()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //�^�O����UIManager��T��
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //�^�O����UIManager��T��
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        //�^�O����SpawnManager��T��
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();

    }

    void Start()
    {
        //���݂�HP��MAXHP�̐��l�ɐݒ�
        currentHp = PLAYER_MAX_HP;

        //HP���X���C�_�[�ɔ��f
        uIManager.UpdateHP(PLAYER_MAX_HP, currentHp);


        // ���̓V�X�e��
        keyBoardInput = GetComponent<IKeyBoardInput>();

        // Player�V�X�e��
        playerAnimator = GetComponent<PlayerAnimator>();
        playerMove = GetComponent<IPlayerMove>();
        playerJump = GetComponent<IPlayerJump>();
        playerJump = GetComponent<IPlayerJump>();
        playerLandDetector = GetComponent<PlayerLandDetector>();

        // ����������
        playerStatus.activeMoveSpeed = PLAYER_WALK_SPEED;               // �ړ����x
        playerStatus.jumpForth = PLAYER_JUMP_FORTH;                     // �W�����v��
        playerStatus.playerAnimationState = PlayerAnimationState.Idol;  // ���
    }


    void Update()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //�@���񑖂��Ă��Ă������ɂȂ�A�낭�ȏ������łȂ�����C������
        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

        // �����ԑJ�� ����L�[���͂���Ă��違�����ԂłȂ��Ȃ�
        if (keyBoardInput.GetRunKeyInput() && !(playerStatus.playerAnimationState == PlayerAnimationState.Run))
        {
            playerStatus.activeMoveSpeed = PLAYER_RUN_SPEED;
            playerStatus.playerAnimationState = PlayerAnimationState.Run;
        }// ����L�[��������Ă��Ȃ� & ��Ԃ�������Ԃł͂Ȃ�
        else if (!keyBoardInput.GetRunKeyInput() && !(playerStatus.playerAnimationState == PlayerAnimationState.Walk))
        {
            playerStatus.activeMoveSpeed = PLAYER_WALK_SPEED;
            playerStatus.playerAnimationState = PlayerAnimationState.Walk;
        }

        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

        // �ړ�
        Vector3 moveDirection = keyBoardInput.GetWASDAndArrowKeyInput();
        if (moveDirection != Vector3.zero)
        {
            playerMove.Move(moveDirection, playerStatus.activeMoveSpeed);
        }
        else
        {
            playerStatus.playerAnimationState = PlayerAnimationState.Idol;
        }

        // �W�����v
        if (playerLandDetector.IsGrounded)
        {
            Debug.Log("�n�ʂɂ��܂�");
            if (keyBoardInput.GetJumpKeyInput())
            {
                playerJump.Jump(playerStatus.jumpForth);
            }
        }
        else
        {
            Debug.Log("�n�ʂɂ��܂���");
        }

        // �A�j���[�V�����X�V
        playerAnimator.AnimationUpdate(playerStatus.playerAnimationState);
    }


    /// <summary>
    /// �e�ɓ����������Ă΂�鏈��
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    /// <param name="name">��������̖��O</param>
    /// <param name="actor">��������̔ԍ�</param>
    [PunRPC]
    public void Hit(int damage, string name, int actor)
    {
        //�_���[�W�֐��Ăяo��
        ReceiveDamage(name, damage, actor);
    }


    /// <summary>
    /// �_���[�W���󂯂鏈��
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    /// <param name="name">��������̖��O</param>
    /// <param name="actor">��������̔ԍ�</param>
    public void ReceiveDamage(string name, int damage, int actor)
    {
        //�����Ȃ�
        if (photonView.IsMine)
        {
            //�_���[�W
            currentHp -= damage;

            //���݂�HP��0�ȉ��̏ꍇ
            if (currentHp <= 0)
            {
                //���S�֐����Ă�
                Death(ref currentHp, name, actor);
            }

            //HP���X���C�_�[�ɔ��f
            uIManager.UpdateHP(PLAYER_MAX_HP, currentHp);
        }
    }


    /// <summary>
    /// ���S����
    /// </summary>
    public void Death(ref int currentHp, string name, int actor)
    {
        //���݂�HP���O�ɒ���
        currentHp = 0;

        //���S�֐����Ăяo��
        spawnManager.Die();

        //���SUI���X�V
        uIManager.UpdateDeathUI(name);

        //�����̃f�X�����㏸(�����̎��ʔԍ��A�f�X�A���Z���l)
        gameManager.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //�����Ă�������̃L�������㏸(�����Ă����G�̎��ʔԍ��A�L���A���Z���l)
        gameManager.ScoreGet(actor, 0, 1);
    }


    /// <summary>
    /// ���S����
    /// </summary>
    public void Death(string name, int actor)
    {
        //���݂�HP���O�ɒ���
        currentHp = 0;

        //���S�֐����Ăяo��
        spawnManager.Die();

        //���SUI���X�V
        uIManager.UpdateDeathUI(name);

        //�����̃f�X�����㏸(�����̎��ʔԍ��A�f�X�A���Z���l)
        gameManager.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //�����Ă�������̃L�������㏸(�����Ă����G�̎��ʔԍ��A�L���A���Z���l)
        gameManager.ScoreGet(actor, 0, 1);
    }


    /// <summary>
    /// Player�̎n������
    /// </summary>
    public void OutGame()
    {
        // GameManager�I�u�W�F�N�g���Q��
        gameManager = GameObject.FindObjectOfType<GameManager>();

        //�v���C���[�f�[�^�폜
        gameManager.OutPlayerGet(PhotonNetwork.LocalPlayer.ActorNumber);

        //������ؒf
        PhotonNetwork.AutomaticallySyncScene = false;

        //���[������ޏo
        PhotonNetwork.LeaveRoom();
    }
}