using UnityEngine;
using Photon.Pun;
using MiniMap;
using System;

public static class PlayerEvent
{
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

    [Tooltip("�v���C���[�̃X�e�[�^�X���")]
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] EnemyIconController enemyIcon;

    [Tooltip("Player�̃A�j���[�V��������")]
    PlayerAnimator playerAnimator;

    [SerializeField] PlayerSoundManager playerSoundManager;


    // ���̓V�X�e��
    [Tooltip("�L�[�{�[�h�̓��͏���")]
    KeyBoardInput keyBoardInput;
   

    IMouseCursorLock mouseCursorLock;

   
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

        // ���̓V�X�e��
        keyBoardInput = GetComponent<KeyBoardInput>();
        mouseCursorLock = GetComponent<IMouseCursorLock>();
        mouseCursorLock.LockScreen();

        playerAnimator = GetComponent<PlayerAnimator>();
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
        }
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

            // HP�X�V����
            playerAnimator.SetCurrentHP(playerStatus.CurrentHP);

            // ���S�̂��̑�����
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