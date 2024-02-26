using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

// �S�̓I�Ƀ��t�@�N�^�����O����
// ��������������

/// <summary>
/// �Q�[���̏�ԊǗ��N���X
/// </summary>
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance { get; private set; }

    [Header("�萔")]
    [Tooltip("�N���A����܂ł̃L����")]
    [SerializeField] int GAMECLEAR_KILL_SCORE = 3;

    [Tooltip("�I�����Ă���̑ҋ@����")]
    [SerializeField] float WAIT_ENDING_TIME = 5f;


    [Header("�Q��")]
    [Tooltip("�v���C���[���������N���X�̃��X�g")]
    [SerializeField] List<PlayerInfo> playerList = new List<PlayerInfo>();

    [Tooltip("�Q�[���̏�Ԃ��i�[")]
    [SerializeField] GameState state;


    [Tooltip("playerinfo�̃��X�g")]
    List<PlayerInformation> playerInfoList = new List<PlayerInformation>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }


    void Start()
    {
        //�l�b�g���[�N�ڑ�����Ă��Ȃ��ꍇ
        if (!PhotonNetwork.IsConnected)
        {
            //�^�C�g���ɖ߂�
            SceneManager.LoadScene(0);
        }
        else//�q�����Ă���ꍇ
        {
            //�}�X�^�[�Ƀ��[�U�[���𔭐M
            NewPlayerGet(PhotonNetwork.NickName);

            //��Ԃ��Q�[�����ɐݒ肷��
            state = GameState.Playing;
        }
    }

    void Update()
    {
        //�^�u�{�^���������ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //�X�V���J��
            ShowScoreboard();
        }
        //�^�u�{�^���������ꂽ��
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            UIManager.instance.ChangeScoreUI();
        }

        //Home�������ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.Home))
        {
            UIManager.instance.ChangeReturnToTitlePanel();
        }
        //�^�u�{�^���������ꂽ��
        else if (Input.GetKeyUp(KeyCode.Home))
        {
            UIManager.instance.ChangeReturnToTitlePanel();
        }
    }


    /// <summary>
    ///  �V�K���[�U�[���l�b�g���[�N�o�R�Ń}�X�^�[�Ɏ����̏��𑗐M
    /// </summary>
    public void NewPlayerGet(string name)//�C�x���g����������֐�
    {
        //�ϐ��錾
        object[] info = new object[4];                  //�f�[�^�i�[�z����쐬

        //�i�[
        info[0] = name;                                 //Player���O
        info[1] = PhotonNetwork.LocalPlayer.ActorNumber;//���[�U�[�̊Ǘ��ԍ�
        info[2] = 0;                                    //�L����
        info[3] = 0;                                    //�f�X��

        // RaiseEvent�ŃJ�X�^���C�x���g�𔭐��F�f�[�^���M
        PhotonNetwork.RaiseEvent((byte)EventCodes.NewPlayer,                    //����������C�x���g
            info,                                                               //���M�f�[�^         �i�v���C���[�j
            new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient },   //���M����ݒ�       �i���[���}�X�^�[�̂݁j
            new SendOptions { Reliability = true }                              //�M�����̐ݒ�       �i�M���ł���̂Ńv���C���[�ɑ��M�����j
        );
    }


    /// <summary>
    /// �����Ă����V�v���C���[�̏������X�g�Ɋi�[
    /// </summary>
    public void NewPlayerSet(object[] data)//�}�X�^�[���s�������@�C�x���g�������ɍs������
    {
        //�l�b�g���[�N����v���C���[�����擾
        PlayerInfo player = new PlayerInfo((string)data[0], (int)data[1], (int)data[2], (int)data[3]);

        //���X�g�ɒǉ�
        playerList.Add(player);

        //���̃v���C���[�ɋ��L
        ListPlayersGet();
    }


    /// <summary>
    /// �擾�����v���C���[�������[�����̑S�v���C���[�ɑ��M
    /// </summary>
    public void ListPlayersGet()//�}�X�^�[���s�������@�C�x���g�𔭐�������֐�
    {
        //�Q�[���̏󋵁��v���C���[���i�[�z��쐬
        object[] info = new object[playerList.Count + 1];

        //�Q�[���̏󋵂��i�[
        info[0] = state;

        //�Q�����[�U�[�̐������[�v
        for (int i = 0; i < playerList.Count; i++)
        {
            //�ꎞ�I�i�[����z��
            object[] temp = new object[4];

            //���i�[
            temp[0] = playerList[i].name;
            temp[1] = playerList[i].actor;
            temp[2] = playerList[i].kills;
            temp[3] = playerList[i].deaths;

            //�v���C���[�����i�[���Ă���z��Ɋi�[ (0�ɂ̓Q�[���̏�Ԃ������Ă��邽�߁{�P)
            info[i + 1] = temp;
        }

        // RaiseEvent�ŃJ�X�^���C�x���g�𔭐��F�f�[�^���M
        PhotonNetwork.RaiseEvent((byte)EventCodes.ListPlayers,          //����������C�x���g
            info,                                                       //���M�f�[�^         �i�v���C���[�j
            new RaiseEventOptions { Receivers = ReceiverGroup.All },    //���M����ݒ�       �i�S���j
            new SendOptions { Reliability = true }                      //�M�����̐ݒ�       �i�M���ł���̂Ńv���C���[�ɑ��M�����j
        );
    }


    /// <summary>
    /// ListPlayersSend�ŐV�����v���C���[��񂪑����Ă����̂ŁA���X�g�Ɋi�[
    /// </summary>
    public void ListPlayersSet(object[] data)//�C�x���g������������Ă΂��֐��@�S�v���C���[�ŌĂ΂��
    {
        //���Ɏ����Ă���v���C���[�̃��X�g��������
        playerList.Clear();

        //�Q�[����Ԃ�ϐ��Ɋi�[
        state = (GameState)data[0];

        //1�ɂ��� 0�̓Q�[����ԂȂ̂�1����n�߂�
        for (int i = 1; i < data.Length; i++)
        {
            object[] info = (object[])data[i];

            PlayerInfo player = new PlayerInfo(
                (string)info[0],    //���O
                (int)info[1],       //�Ǘ��ԍ�
                (int)info[2],       //�L��
                (int)info[3]);      //�f�X

            //���X�g�ɒǉ�
            playerList.Add(player);
        }

        //�Q�[���̏�Ԕ���
        StateCheck();
    }


    /// <summary>
    /// �L������f�X���̎擾�֐�
    /// </summary>
    /// <param name="actor">�v���C���[���ʔԍ�</param>
    /// <param name="stat">�L�����f�X�𐔒l�Ŕ���</param>
    /// <param name="amount">���Z���鐔�l</param>
    public void ScoreGet(int actor, int stat, int amount)
    {
        object[] package = new object[] { actor, stat, amount };

        //�f�[�^�𑗂�C�x���g
        PhotonNetwork.RaiseEvent((byte)EventCodes.UpdateStat,           //����������C�x���g
            package,                                                    //���M�f�[�^         �i�v���C���[�̃L���f�X�j
            new RaiseEventOptions { Receivers = ReceiverGroup.All },    //���M����ݒ�       �i�S���j
            new SendOptions { Reliability = true }                      //�M�����̐ݒ�       �i�M���ł���̂Ńv���C���[�ɑ��M�����)
        );
    }


    /// <summary>
    /// �󂯎�����f�[�^���烊�X�g�ɃL���f�X����ǉ�
    /// </summary>
    public void ScoreSet(object[] data)
    {
        //�ϐ�
        int actor = (int)data[0];   //���ʐ�
        int stat = (int)data[1];    //�L���Ȃ̂��f�X�Ȃ̂�
        int amount = (int)data[2];  //���Z���鐔�l

        //�v���C���[�̐������[�v
        for (int i = 0; i < playerList.Count; i++)
        {
            //�����擾�����v���C���[�Ɛ��l�����v�����Ƃ�
            if (playerList[i].actor == actor)
            {
                switch (stat)
                {
                    case 0://�L��
                        playerList[i].kills += amount;
                        break;

                    case 1://�f�X
                        playerList[i].deaths += amount;
                        break;
                }

                //��������
                break;
            }
        }

        //�X�R�A�m�F
        TargetScoreCheck();
    }


    /// <summary>
    /// �X�R�A�{�[�h�\��
    /// </summary>
    void ShowScoreboard()
    {
        //�X�R�AUI���J��
        UIManager.instance.ChangeScoreUI();

        //���X�g�̐������[�v
        foreach (PlayerInformation info in playerInfoList)
        {
            //�X�R�A�{�[�h�ɕ\������Ă���S���̐�т��폜
            Destroy(info.gameObject);
        }

        //���X�g����폜
        playerInfoList.Clear();

        //�Q�[���ɎQ�����Ă���v���C���[�̐������[�v
        foreach (PlayerInfo player in playerList)
        {
            //�v���C���[����\������I�u�W�F�N�g�𐶐�
            PlayerInformation newPlayerDisplay = Instantiate(UIManager.instance.GetPlayerInformation(), UIManager.instance.GetPlayerInformation().transform.parent);

            //���������I�u�W�F�N�g�ɐ�т𔽉f
            newPlayerDisplay.SetPlayerDetailes(player.name, player.kills, player.deaths);

            //�\��
            newPlayerDisplay.gameObject.SetActive(true);

            //���X�g�ɒǉ�
            playerInfoList.Add(newPlayerDisplay);
        }
    }


    /// <summary>
    /// �Q�[���N���A������B���������m�F
    /// </summary>
    void TargetScoreCheck()
    {
        //�Q�[���N���A����t���O
        bool clearFlg = false;

        //�l�������[�v
        foreach (PlayerInfo player in playerList)
        {
            //��������
            if (player.kills >= GAMECLEAR_KILL_SCORE && GAMECLEAR_KILL_SCORE > 0)
            {
                clearFlg = true;   //�N���A����
                break;             //�����I��
            }
        }

        //�N���A����
        if (clearFlg)
        {
            if (PhotonNetwork.IsMasterClient && state != GameState.Ending)
            {
                state = GameState.Ending;   //��ԕύX
                ListPlayersGet();           //�ŏI�I�ȃv���C���[�����X�V
            }
        }
    }


    /// <summary>
    /// �Q�[���̏�Ԏ���ŃQ�[���I��
    /// </summary>
    void StateCheck()
    {
        //��Ԃ̔���
        if (state == GameState.Ending)
        {
            //�I���֐����Ă�
            EndGame();
        }
    }


    /// <summary>
    /// �Q�[���I���֐�
    /// </summary>
    void EndGame()
    {
        //�}�X�^�[�Ȃ�
        if (PhotonNetwork.IsMasterClient)
        {
            //�l�b�g���[�N�ォ��폜
            PhotonNetwork.DestroyAll();
        }

        //�Q�[���I���p�l���\��
        UIManager.instance.OpenEndPanel();

        //�X�R�A�\��
        ShowScoreboard();

        //�J�[�\���\��        
        Cursor.lockState = CursorLockMode.None;

        //��莞�Ԍ�I�����������s
        Invoke("ProcessingAfterCompletion", WAIT_ENDING_TIME);
    }


    /// <summary>
    /// �I����̏���
    /// </summary>
    private void ProcessingAfterCompletion()
    {
        PhotonNetwork.AutomaticallySyncScene = false;   //�V�[���̓����ݒ��ؒf
        PhotonNetwork.LeaveRoom();                      //������ޏo
    }


    /// <summary>
    /// �v���C���[���Ǘ����X�g����Y���v���C���[���폜
    /// </summary>
    public void OutPlayerGet(int actor)
    {
        //�Q���l�������s
        for (int i = 0; i < playerList.Count; i++)
        {
            //�����擾�������[�U�[�Ɛ��l�����v�����Ƃ�
            if (playerList[i].actor == actor)
            {
                //���������[�U�[�̏�񂾂��폜
                playerList.RemoveAt(i);

                //��������
                break;
            }
        }

        //�v���C���[�����X�V
        ListPlayersGet();
    }
}