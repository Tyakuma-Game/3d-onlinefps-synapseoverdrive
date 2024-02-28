using UnityEngine;
using Photon.Pun;
using MiniMap;
using System;

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


    [SerializeField] PlayerSoundManager playerSoundManager;
   
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

        playerStatus.Init();

        //HP�X���C�_�[���f
        UIManager.instance.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);

        // ���݂�HP���Z�b�g
        OnHPChanged?.Invoke(playerStatus.CurrentHP);
    }


    void Update()
    {
        if (!photonView.IsMine)
            return;

        // ���S���o���Ȃ�
        if (isShowDeath)
        {
            Debug.Log("���S���o�ŏ����𒆒f�����Ă܂��B");

            // �����I��
            return;
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
    /// HP�̍X�V����
    /// </summary>
    public static event Action<int> OnHPChanged;

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
            OnHPChanged?.Invoke(playerStatus.CurrentHP);

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