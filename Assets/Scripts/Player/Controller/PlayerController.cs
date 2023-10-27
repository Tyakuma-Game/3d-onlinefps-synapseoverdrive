using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Playables;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

/// <summary>
/// Player�Ǘ��N���X
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    [Tooltip("Player�̏e�Ǘ��N���X")]
    [SerializeField] PlayerGunController playerGunController;

    UIManager uIManager;        //UI�Ǘ�
    SpawnManager spawnManager;  //�X�|�[���}�l�[�W���[�Ǘ�
    GameManager gameManager;    //�Q�[���}�l�[�W���[

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    //�@����������Program
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    [Tooltip("�v���C���[�̃X�e�[�^�X���")]
    [SerializeField] PlayerStatus playerStatus;

    // Player�@�\
    [Tooltip("Player�̈ړ�����")]
    IPlayerMove playerMove;

    [Tooltip("Player�̉�]����")]
    IPlayerRotation playerRotation;

    [Tooltip("Player�̃W�����v����")]
    IPlayerJump playerJump;

    [Tooltip("Player�̃A�j���[�V��������")]
    IPlayerAnimator playerAnimator;

    [Tooltip("���n���Ă��邩���菈��")]
    PlayerLandDetector playerLandDetector;

    [SerializeField] PlayerSoundManager playerSoundManager;


    // ���̓V�X�e��
    [Tooltip("�L�[�{�[�h�̓��͏���")]
    IKeyBoardInput keyBoardInput;
    
    [Tooltip("�}�E�X�̓��͏���")]
    IMouseInput mouseInput;

    IMouseCursorLock mouseCursorLock;


    Rigidbody myRigidbody;
    Camera myCamera;

    [SerializeField] CameraController cameraController;

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    void Awake()
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
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        myRigidbody = GetComponent<Rigidbody>();
        myCamera = Camera.main;

        // ���̓V�X�e��
        keyBoardInput = GetComponent<IKeyBoardInput>();
        mouseInput = GetComponent<IMouseInput>();
        mouseCursorLock = GetComponent<IMouseCursorLock>();
        mouseCursorLock.LockScreen();


        // Player�V�X�e��
        playerLandDetector = GetComponent<PlayerLandDetector>();
        playerAnimator = GetComponent<IPlayerAnimator>();
        playerMove = GetComponent<IPlayerMove>();
        playerJump = GetComponent<IPlayerJump>();
        playerRotation = GetComponent<IPlayerRotation>();

        // �X�e�[�^�X������
        playerJump.Init(myRigidbody);
        playerStatus.Init();

        //HP�X���C�_�[���f
        uIManager.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);
    }


    void Update()
    {
        // �����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        // ��ԕύX����
        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        {
            // �}�E�X�J�[�\���̃��b�N��ԕύX
            if (keyBoardInput.GetCursorLockKeyInput())
            {
                if (mouseCursorLock.IsLocked())
                    mouseCursorLock.LockScreen();
                else
                    mouseCursorLock.UnlockScreen();
            }

            // ��ԑJ��
            if (keyBoardInput.GetRunKeyInput())
            {
                if (playerStatus.AnimationState != PlayerAnimationState.Run)
                    playerStatus.IsRunning();
            }
            else
            {
                if (playerStatus.AnimationState != PlayerAnimationState.Walk)
                    playerStatus.IsWalking();
            }
        }

        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        // PLAYER����
        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        {
            // ��]
            Vector2 roteDirection = mouseInput.GetMouseMove();
            if (roteDirection != Vector2.zero)
            {
                playerRotation.Rotation(roteDirection, playerStatus.Constants.RotationSpeed);
                cameraController.Rotation(roteDirection, playerStatus.Constants.RotationSpeed, playerStatus.Constants.VerticalRotationRange);
            }

            // �ړ�
            Vector3 moveDirection = keyBoardInput.GetWASDAndArrowKeyInput();
            if (moveDirection != Vector3.zero)
            {
                playerMove.Move(moveDirection, playerStatus.ActiveMoveSpeed);
            }
            else
            {
                playerStatus.IsIdol();
            }

            // �W�����v
            if (playerLandDetector.IsGrounded)
            {
                if (keyBoardInput.GetJumpKeyInput())
                {
                    playerJump.Jump(playerStatus.ActiveJumpForth);
                    playerLandDetector.OnJunpingChangeFlag();
                }
            }

            //TO DO ����͍폜����I
            if(Input.GetKeyDown(KeyCode.M))
            {
                // �e�X�g�R�[�h�iDamage�ŉ�ʂɌ����o����j
                playerStatus.OnDamage(10);
                uIManager.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);
                cameraController.Shake();
            }
            
            // �A�j���[�V�����X�V
            playerAnimator.AnimationUpdate(playerStatus.AnimationState);

            if(playerLandDetector.IsGrounded == false)
            {
                playerStatus.IsIdol();
            }

            // Sound����
            playerSoundManager.SoundPlays(playerStatus.AnimationState);
        }

        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        // �J��������
        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

        // �J�����̍��W�X�V
        cameraController.UpdatePosition();
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
            playerStatus.OnDamage(damage);

            //�J������h�炷
            cameraController.Shake();

            //���݂�HP��0�ȉ��̏ꍇ
            if (playerStatus.CurrentHP <= 0)
            {
                //���S�֐����Ă�
                Death(name, actor);
            }

            //HP���X���C�_�[�ɔ��f
            uIManager.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);
        }
    }

    /// <summary>
    /// ���S����
    /// </summary>
    public void Death(string name, int actor)
    {
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