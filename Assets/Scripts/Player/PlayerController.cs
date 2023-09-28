using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


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
    [Tooltip("Player�̈ړ��Ɋւ���N���X")]
    [SerializeField] PlayerMovement playerMovement;

    [Tooltip("Player�̎��_�ړ��Ɋւ���N���X")]
    [SerializeField] PlayerViewpointShift playerViewpointShift;

    [Tooltip("Player�̏e�Ǘ��N���X")]
    [SerializeField] PlayerGunController playerGunController;

    UIManager uIManager;        //UI�Ǘ�
    SpawnManager spawnManager;  //�X�|�[���}�l�[�W���[�Ǘ�
    GameManager gameManager;    //�Q�[���}�l�[�W���[


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


    private void Start()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //���݂�HP��MAXHP�̐��l�ɐݒ�
        currentHp = PLAYER_MAX_HP;

        //HP���X���C�_�[�ɔ��f
        uIManager.UpdateHP(PLAYER_MAX_HP, currentHp);
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