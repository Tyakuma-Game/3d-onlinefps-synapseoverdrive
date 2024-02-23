using UnityEngine;
using Photon.Pun;
using MiniMap;
using System;

public static class PlayerEvent
{
    public static Action onIdol;
    public static Action onWalk;
    public static Action onDash;

    public static Action onDamage;
    public static Action onSpawn;
    public static Action onDisappear;
}

/// <summary>
/// Player�Ǘ��N���X
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    //�@��������
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    [SerializeField] Animator gunAnimator;

    [Tooltip("�v���C���[�̃X�e�[�^�X���")]
    [SerializeField] PlayerStatus playerStatus;

    [SerializeField] EnemyIconController enemyIcon;

    

    [Tooltip("Player�̃A�j���[�V��������")]
    PlayerAnimator playerAnimator;

    [Tooltip("���n���Ă��邩���菈��")]
    PlayerLandDetector playerLandDetector;

    [SerializeField] PlayerSoundManager playerSoundManager;


    // ���̓V�X�e��
    [Tooltip("�L�[�{�[�h�̓��͏���")]
    KeyBoardInput keyBoardInput;
   

    IMouseCursorLock mouseCursorLock;

    

    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject spawnEffect;


    [Tooltip("Player�̃W�����v����")]
    IPlayerJump playerJump;


    Rigidbody myRigidbody;
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

    void Start()
    {
        // �w�莞�Ԍ�ɉ��o���~������
        Invoke("SpawnEffectNotActive", 1.5f);

        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            enemyIcon.SetIconVisibility(true);
            //�����I��
            return;
        }
        enemyIcon.SetIconVisibility(false);

        MiniMapController.instance.SetMiniMapTarget(this.transform);

        myRigidbody = GetComponent<Rigidbody>();

        // ���̓V�X�e��
        keyBoardInput = GetComponent<KeyBoardInput>();
        mouseCursorLock = GetComponent<IMouseCursorLock>();
        mouseCursorLock.LockScreen();

        // Player�V�X�e��
        playerLandDetector = GetComponent<PlayerLandDetector>();
        playerAnimator = GetComponent<PlayerAnimator>();


        playerJump = GetComponent<IPlayerJump>();

        // �X�e�[�^�X������
        playerJump.Init(myRigidbody);
        playerStatus.Init();

        //HP�X���C�_�[���f
        UIManager.instance.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);

        // ���݂�HP���Z�b�g
        playerAnimator.SetCurrentHP(playerStatus.CurrentHP);
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
                {
                    playerStatus.IsRunning();
                    PlayerEvent.onDash?.Invoke();
                }
            }
            else
            {
                if (playerStatus.AnimationState != PlayerAnimationState.Walk)
                {
                    playerStatus.IsWalking();
                    PlayerEvent.onWalk?.Invoke();
                }
                    
            }
        }

        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        // PLAYER����
        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        {
            // �ړ��@���ꂼ��ɃA�j���[�V�����ݒ肷�鏈��������ēK�p���銴���ɂ��H�@������A�N�V�����ɓn���Ĉړ��̂���Ăяo���Ȃ�
            Vector3 moveDirection = keyBoardInput.GetWASDAndArrowKeyInput();
            if (moveDirection != Vector3.zero)
            {
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
            {
                playerAnimator.IsGround(playerLandDetector.IsGrounded);
                float moveSpeed = moveDirection.magnitude * playerStatus.ActiveMoveSpeed;
                playerAnimator.UpdateMoveSpeed(moveSpeed);
                gunAnimator.SetFloat("MoveSpeed", moveSpeed);
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
            UIManager.instance.IsRunning();
        }
        else
        {
            UIManager.instance.IsNotRunning();
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
            playerAnimator.SetCurrentHP(playerStatus.CurrentHP);
            playerAnimator.Damage();

            //�J������h�炷
            PlayerEvent.onDamage?.Invoke();

            //���݂�HP��0�ȉ��̏ꍇ
            if (playerStatus.CurrentHP <= 0 && isShowDeath == false)
            {
                //���S�֐����Ă�
                Death(name, actor);
            }

            //HP���X���C�_�[�ɔ��f
            UIManager.instance.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);
        }
    }

    /// <summary>
    /// ���S����
    /// </summary>
    public void Death(string name, int actor)
    {
        //���SUI���X�V
        UIManager.instance.UpdateDeathUI(name);

        //�����̃f�X�����㏸(�����̎��ʔԍ��A�f�X�A���Z���l)
        GameManager.instance.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //�����Ă�������̃L�������㏸(�����Ă����G�̎��ʔԍ��A�L���A���Z���l)
        GameManager.instance.ScoreGet(actor, 0, 1);

        // ���S���o�ύX
        isShowDeath = true;

        // ���Ńp�[�e�B�N���o��
        photonView.RPC("SpawnEffectActive",RpcTarget.All);

        //���S�֐����Ăяo��
        SpawnManager.instance.StartRespawnProcess();
    }

    /// <summary>
    /// Player�̎n������
    /// </summary>
    public void OutGame()
    {
        GameManager.instance.OutPlayerGet(PhotonNetwork.LocalPlayer.ActorNumber); // �v���C���[�f�[�^�폜
        PhotonNetwork.AutomaticallySyncScene = false;                             // �����ؒf
        PhotonNetwork.LeaveRoom();                                                // ���[���ޏo
    }
}