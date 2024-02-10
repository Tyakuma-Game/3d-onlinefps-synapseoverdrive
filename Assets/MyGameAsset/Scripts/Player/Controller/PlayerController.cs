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
    UIManager uIManager;        //UI�Ǘ�
    SpawnManager spawnManager;  //�X�|�[���}�l�[�W���[�Ǘ�
    GameManager gameManager;    //�Q�[���}�l�[�W���[

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    //�@��������
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    [SerializeField] Animator gunAnimator;

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


    TestAnimatorController testAnimatorController;
    [SerializeField] GameObject spawnEffect;


    bool isShowDeath = false;

    [PunRPC]
    public void SpawnEffectActive()
    {
        spawnEffect.SetActive(true);
    }

    public void SpawnEffectNotActive()
    {
        spawnEffect.SetActive(false);
    }


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
        // �w�莞�Ԍ�ɉ��o���~������
        Invoke("SpawnEffectNotActive", 1.5f);

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
        playerMove.Init(myRigidbody);
        playerJump.Init(myRigidbody);
        playerStatus.Init();

        //HP�X���C�_�[���f
        uIManager.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);

        testAnimatorController = GetComponent<TestAnimatorController>();
        testAnimatorController.TestSetHP(playerStatus.CurrentHP);
    }


    void Update()
    {
        // �����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        // ���S���o���Ȃ�
        if (isShowDeath)
        {
            Debug.Log("���S���o�ŏ����𒆒f�����Ă܂��B");

            // �����I��
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

            //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
            // �A�j���[�V�����X�V
            //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
            testAnimatorController.TestIsGround(playerLandDetector.IsGrounded);
            if (testAnimatorController == null)
            {
                Debug.Log("testAnimatorController��NULL!!");
            }
            else
            {
                float moveSpeed = moveDirection.magnitude*playerStatus.ActiveMoveSpeed;
                testAnimatorController.TestMove(moveSpeed);

                gunAnimator.SetFloat("MoveSpeed",moveSpeed);
            }
            

            if (playerLandDetector.IsGrounded == false)
            {
                playerStatus.IsIdol();
                gunAnimator.SetFloat("MoveSpeed", 0f);
            }

            // Sound����
            playerSoundManager.SoundPlays(playerStatus.AnimationState);
        }

        if (playerStatus.AnimationState == PlayerAnimationState.Run)
        {
            uIManager.IsRunning();
        }
        else
        {
            uIManager.IsNotRunning();
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
            // Damage���󂯂��ۂ̉���炷
            playerSoundManager.DamageSound();

            //�_���[�W
            playerStatus.OnDamage(damage);
            
            // �A�j���[�V����
            testAnimatorController.TestSetHP(playerStatus.CurrentHP);
            testAnimatorController.Damage();

            //�J������h�炷
            cameraController.Shake();

            //���݂�HP��0�ȉ��̏ꍇ
            if (playerStatus.CurrentHP <= 0 && isShowDeath == false)
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
        //���SUI���X�V
        uIManager.UpdateDeathUI(name);

        //�����̃f�X�����㏸(�����̎��ʔԍ��A�f�X�A���Z���l)
        gameManager.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //�����Ă�������̃L�������㏸(�����Ă����G�̎��ʔԍ��A�L���A���Z���l)
        gameManager.ScoreGet(actor, 0, 1);

        // ���S���o�ύX
        isShowDeath = true;

        // ���Ńp�[�e�B�N���o��
        photonView.RPC("SpawnEffectActive",RpcTarget.All);

        //���S�֐����Ăяo��
        spawnManager.Die();
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