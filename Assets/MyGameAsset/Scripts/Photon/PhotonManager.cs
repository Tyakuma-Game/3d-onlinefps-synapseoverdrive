using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// TODO:��K�͂ȏC�����K�v
// �S�̓I�ɃX�N���v�g�𕪊����AMVC���f���`���ō�蒼��

public class RoomDataSettingTest
{
    [SerializeField] int RoomMember;
}

/// <summary>
/// Photon�Ǘ��̃N���X
/// </summary>
public class PhotonManager : MonoBehaviourPunCallbacks
{
    [Header("�萔")]
    [Tooltip("�v���C���[�̍ő�Q���l��")]
    public int ROOM_MEMBER_MAX = 6;

    [Header("�Q��")]
    [Tooltip("photon�Ǘ��N���X")]
    public static PhotonManager instance;

    [Header("���[�h��ʊ֘A")]
    [SerializeField] GameObject loadingPanel; //���[�h�p�l��
    [SerializeField] Text loadingText;        //���[�h�e�L�X�g
    [SerializeField] GameObject buttons;      //�{�^��

    [Header("���[���쐬���")]
    [SerializeField] GameObject createRoomPanel;  //���[���쐬�p�l��
    [SerializeField] Text enterRoomName;          //���͂��ꂽ���[�����e�L�X�g

    [Header("���[�����")]
    [SerializeField] GameObject roomPanel;    //���[���p�l��
    [SerializeField] Text roomName;           //���[�����e�L�X�g

    [Header("���[���̃G���[���")]
    [SerializeField] GameObject errorPanel;   //�G���[�p�l��
    [SerializeField] Text errorText;          //�G���[�e�L�X�g

    [Header("���[���̈ꗗ���")]
    [SerializeField] GameObject roomListPanel;//���[���ꗗ�p�l��

    [Header("���[���Ǘ��p")]
    [SerializeField] Room originalRoomButton;                                             //���[���{�^���i�[
    [SerializeField] GameObject roomButtonContent;                                        //���[���{�^���̐e�I�u�W�F�N�g
    Dictionary<string, RoomInfo> roomsList = new Dictionary<string, RoomInfo>();          //���[���̏�����������
    List<Room> allRoomButtons = new List<Room>();                                         //���[���{�^�����������X�g

    [Header("Player�Ǘ��p")]
    [SerializeField] Text playerNameText;                             //�v���C���[�e�L�X�g
    List<Text> allPlayerNames = new List<Text>();                     //�v���C���[�̊Ǘ����X�g
    [SerializeField] GameObject playerNameContent;                    //�v���C���[�l�[���̐e�I�u�W�F�N�g
    [SerializeField] GameObject nameInputPanel;                       //���O���̓p�l��
    [SerializeField] Text placeholderText;                            //�\���e�L�X�g�A
    [SerializeField] InputField nameInput;                            //���O���̓t�H�[��
    bool setName;                                                     //���O���͔���

    [Header("�Q�[���J�n�p")]
    [SerializeField] GameObject startButton;  //�Q�[���J�n���邽�߂̃{�^��

    [Header("�J�ڐ�̃V�[��")]
    [SerializeField] string levelToPlay;      //�J�ڐ�̃V�[����


    void Awake()
    {
        if(instance ==  null)
            instance = this;
        else 
            Destroy(this.gameObject);
    }


    void Start()
    {
        //���j���[��S�ĕ���
        CloseMenuUI();

        AAA();
    }

    void AAA()
    {
        //���[�h�p�l����\�����ăe�L�X�g�X�V
        loadingPanel.SetActive(true);
        loadingText.text = "�l�b�g���[�N�ɐڑ���...";

        //To DO : off-line���֖߂邩�I������\������悤�ύX����
        if (!IsInternetConnected())
        {
            loadingText.text = "�l�b�g���[�N�̐ڑ��Ɏ��s�������ߎ��s�ł��܂���B";
            Debug.LogError("Error:NotConnect Internet");
        }

        if (!PhotonNetwork.IsConnected)
        {
            //PhotonServerSettings�t�@�C���̐ݒ�ɏ]����Photon�ɐڑ�
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>
    /// �l�b�g���[�N�ڑ��m�F
    /// </summary>
    bool IsInternetConnected()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            return false;
        else
            return true;
    }


    /// <summary>
    /// ���j���[��S�ĕ���
    /// </summary>
    void CloseMenuUI()
    {
        //���[�h�p�l����\��
        loadingPanel.SetActive(false);

        //�{�^����\��
        buttons.SetActive(false);

        //���[���쐬�p�l��
        createRoomPanel.SetActive(false);

        //���[���p�l��
        roomPanel.SetActive(false);

        //�G���[�p�l��
        errorPanel.SetActive(false);

        //���[���ꗗ�p�l��
        roomListPanel.SetActive(false);
        
        //���O���̓p�l��
        nameInputPanel.SetActive(false);
    }


    /// <summary>
    /// �N���C�A���g��Master Server�ɐڑ�+�������������Ƃ�
    /// </summary>
    public override void OnConnectedToMaster()
    {
        //�}�X�^�[�T�[�o�[��̃f�t�H���g���r�[�ɎQ��
        PhotonNetwork.JoinLobby();

        //�e�L�X�g�X�V
        loadingText.text = "���r�[�֎Q��...";

        //�}�X�^�[�Ɠ����V�[���ɍs���悤�ɐݒ�
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    /// <summary>
    /// �}�X�^�[�T�[�o�[�̃��r�[�ɓ���Ƃ��ɌĂяo��
    /// </summary>
    public override void OnJoinedLobby()
    {
        //���r�[���j���[��\��
        LobbyMenuDisplay();

        //�����̏�����
        roomsList.Clear();

        //���̃��[�U�[�l�[��������
        PhotonNetwork.NickName = Random.Range(0, 1000).ToString();

        //���O�����͂���Ă���ꍇ�͂��̖��O�𔽉f
        ConfirmationName();
    }


    /// <summary>
    /// ���r�[���j���[�\��
    /// </summary>
    public void LobbyMenuDisplay()
    {
        //���j���[����
        CloseMenuUI();

        //�{�^�����\��
        buttons.SetActive(true);
    }


    /// <summary>
    /// ���[���쐬��ʕ\��
    /// </summary>
    public void OpenCreateRoomPanel()
    {
        //���j���[����
        CloseMenuUI();
        createRoomPanel.SetActive(true);
    }


    /// <summary>
    /// �i�����ʁj���[���쐬
    /// </summary>
    public void CreateRoomButton()
    {
        //�C���v�b�g�t�B�[���h�̃e�L�X�g�ɉ������͂���Ă邩
        if (!string.IsNullOrEmpty(enterRoomName.text))
        {
            //���[���̃I�v�V�������C���X�^���X�����ĕϐ��ɑ��
            RoomOptions options = new RoomOptions();

            //�v���C���[�̍ő�Q���l���̐ݒ�
            options.MaxPlayers = ROOM_MEMBER_MAX;

            //���[���𐶐�(���[�����F�����̐ݒ�)
            PhotonNetwork.CreateRoom(enterRoomName.text, options);

            //���j���[����
            CloseMenuUI();

            //�e�L�X�g�X�V
            loadingText.text = "���[���쐬��...";

            //�ǂݍ��݃p�l���\��
            loadingPanel.SetActive(true);
        }
    }


    /// <summary>
    /// ���[���Q�����Ɏ��s����鏈��
    /// </summary>
    public override void OnJoinedRoom()
    {
        //���j���[����
        CloseMenuUI();

        //���[���p�l���\��
        roomPanel.SetActive(true);

        //���݂��郋�[�����擾���A�e�L�X�g�Ƀ��[�����𔽉f
        roomName.text = PhotonNetwork.CurrentRoom.Name;

        //���[���ɎQ�����Ă���v���C���[��\��
        GetAllPlayer();

        //���[���}�X�^�[�����肷��
        CheckRoomMaster();
    }


    /// <summary>
    /// �Q�����̃��[������ޏo
    /// </summary>
    public void LeavRoom()
    {
        //���݂̃��[������ޏo
        PhotonNetwork.LeaveRoom();

        //���j���[����
        CloseMenuUI();

        //�e�L�X�g���X�V
        loadingText.text = "�ޏo���E�E�E";

        //�ǂݍ��ݒ��̉�ʂ�\��
        loadingPanel.SetActive(true);
    }


    /// <summary>
    /// ���[���ޏo�����s
    /// </summary>
    public override void OnLeftRoom()
    {
        // �ʂ̕���\������Ȃ�K�v�����H

        //���r�[���j���[��\��
        //LobbyMenuDisplay();
    }


    /// <summary>
    /// �T�[�o�Ƀ��[���쐬���s�����s
    /// </summary>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //�G���[�e�L�X�g��ҏW
        errorText.text = "���[���̍쐬�Ɏ��s���܂���" + message;

        //���j���[����
        CloseMenuUI();

        //�G���[��ʕ\��
        errorPanel.SetActive(true);
    }


    /// <summary>
    /// ���[���ꗗ��ʂ�\��
    /// </summary>
    public void FindRoom()
    {
        //���j���[����
        CloseMenuUI();

        //���[���ꗗ��ʂ�\��
        roomListPanel.SetActive(true);
    }


    /// <summary>
    /// Master Server�̃��r�[�ҋ@���̃��[�����X�g�X�V
    /// </summary>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //���[��UI�̏�����
        RoomUIinitialization();

        //���[�����������Ɋi�[
        UpdateRoomList(roomList);
    }


    /// <summary>
    /// ���[���̏���������
    /// </summary>
    public void UpdateRoomList(List<RoomInfo> roomList)
    {
        //���[���̐������[�v
        for (int i = 0; i < roomList.Count; i++)
        {
            //���[������ϐ��Ɋi�[
            RoomInfo info = roomList[i];

            //���r�[�Ŏg�p����A���X�g����Ȃ��Ȃ����������}�[�N�i�����A���A�܂��͔�\���j
            if (info.RemovedFromList)
            {
                //��������폜
                roomsList.Remove(info.Name);
            }
            else
            {
                //���[�������L�[�ɂ��āA�����ɒǉ�
                roomsList[info.Name] = info;
            }
        }

        //�����ɂ��邷�ׂẴ��[����\��
        RoomListDisplay(roomsList);
    }


    /// <summary>
    /// ���[���\��
    /// </summary>
    void RoomListDisplay(Dictionary<string, RoomInfo> cachedRoomList)
    {
        //�����̃L�[(�l)�ŉ�
        foreach (var roomInfo in cachedRoomList)
        {
            //���[���{�^���쐬
            Room newButton = Instantiate(originalRoomButton);

            //���������{�^���Ƀ��[���̏���ݒ�
            newButton.RegisterRoomDetails(roomInfo.Value);

            //���������{�^���ɐe�̐ݒ�
            newButton.transform.SetParent(roomButtonContent.transform);

            //���X�g�ɒǉ�
            allRoomButtons.Add(newButton);
        }
    }


    /// <summary>
    /// ���[���{�^��UI������
    /// </summary>
    void RoomUIinitialization()
    {
        // ���[���I�u�W�F�N�g�̐������[�v
        foreach (Room rm in allRoomButtons)
        {
            // �{�^���I�u�W�F�N�g���폜
            Destroy(rm.gameObject);
        }

        //���X�g�v�f�폜
        allRoomButtons.Clear();
    }


    /// <summary>
    /// �����̃��[���ɓ���
    /// </summary>
    public void JoinRoom(RoomInfo roomInfo)
    {
        //�����̃��[���ɎQ��
        PhotonNetwork.JoinRoom(roomInfo.Name);

        //���j���[����
        CloseMenuUI();

        //�e�L�X�g��ҏW
        loadingText.text = "���[���Q����...";

        //�p�l����\������
        loadingPanel.SetActive(true);
    }


    /// <summary>
    /// ���[���ɂ���v���C���[���擾
    /// </summary>
    public void GetAllPlayer()
    {
        //������
        InitializePlayerList();

        //�v���C���[�\��
        PlayerDisplay();
    }


    /// <summary>
    /// �v���C���[�ꗗ������
    /// </summary>
    void InitializePlayerList()
    {
        //���X�g�ŊǗ����Ă��鐔�����[�v
        foreach (var rm in allPlayerNames)
        {
            //text�폜
            Destroy(rm.gameObject);
        }

        //���X�g������
        allPlayerNames.Clear();
    }


    /// <summary>
    /// ���[���ɂ���v���C���[��\��
    /// </summary>
    void PlayerDisplay()
    {
        //���[���ɐڑ����Ă���v���C���[�̐������[�v
        foreach (var players in PhotonNetwork.PlayerList)
        {
            //�e�L�X�g�̐���
            PlayerTextGeneration(players);
        }
    }


    /// <summary>
    /// �v���C���[�e�L�X�g����
    /// </summary>
    void PlayerTextGeneration(Player players)
    {
        //�p�ӂ��Ă���e�L�X�g���x�[�X�Ƀv���C���[�e�L�X�g�𐶐�
        Text newPlayerText = Instantiate(playerNameText);

        //�e�L�X�g�ɖ��O�𔽉f
        newPlayerText.text = players.NickName;

        //�e�I�u�W�F�N�g�̐ݒ�
        newPlayerText.transform.SetParent(playerNameContent.transform);

        //���X�g�ɒǉ�
        allPlayerNames.Add(newPlayerText);
    }


    /// <summary>
    /// ���O�̔���
    /// </summary>
    void ConfirmationName()
    {
        //���O�����͂���Ă��Ȃ��Ȃ�
        if (!setName)
        {
            //���j���[����
            CloseMenuUI();

            //���O���̓p�l����\��
            nameInputPanel.SetActive(true);

            //�L�[���ۑ�����Ă��邩�m�F
            if (PlayerPrefs.HasKey("playerName"))
            {
                placeholderText.text = PlayerPrefs.GetString("playerName");

                //�C���v�b�g�t�B�[���h�ɖ��O��\��
                nameInput.text = PlayerPrefs.GetString("playerName");
            }

        }
        else//���ɓ��͂���Ă���ꍇ�͎����I�ɖ��O���Z�b�g
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("playerName");
        }
    }


    /// <summary>
    /// ���O�ۑ�����͔���؂�ւ�
    /// </summary>
    public void SetName()
    {
        //���͂���Ă���ꍇ
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            //���[�U�[���ɓ��͂��ꂽ���O�𔽉f
            PhotonNetwork.NickName = nameInput.text;

            //���O��ۑ�
            PlayerPrefs.SetString("playerName", nameInput.text);

            //���r�[�ɖ߂�
            LobbyMenuDisplay();

            //���O���͍ςݔ���
            setName = true;
        }
    }


    /// <summary>
    /// �v���C���[�����[���Q�����ɌĂ΂�鏈��
    /// </summary>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //�e�L�X�g�𐶐�
        PlayerTextGeneration(newPlayer);
    }


    /// <summary>
    /// �v���C���[�����[���ޏo���ɌĂ΂�鏈��
    /// </summary>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //���[�����v���C���[���X�g�X�V
        GetAllPlayer();
    }


    /// <summary>
    /// �����̃}�X�^�[���m�F
    /// </summary>
    void CheckRoomMaster()
    {
        //�����̃}�X�^�[���m�F
        if (PhotonNetwork.IsMasterClient)
        {
            //�����̃}�X�^�[�Ȃ�Q�[���J�n�{�^����\��
            startButton.gameObject.SetActive(true);
        }
        else
        {
            //�����̃}�X�^�[�łȂ��Ȃ�Q�[���J�n�{�^�����\��
            startButton.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// MasterClient�I������MasterClient�؂�ւ�
    /// </summary>
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //�����̃}�X�^�[���m�F
        if (PhotonNetwork.IsMasterClient)
        {
            //�����̃}�X�^�[�Ȃ�Q�[���J�n�{�^����\��
            startButton.gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// �w��V�[���ɑJ�ڂ�����
    /// </summary>
    public void PlayGame()
    {
        //�����̃X�e�[�W��ǂݍ���
        PhotonNetwork.LoadLevel(levelToPlay);
    }


    /// <summary>
    ///  �Q�[���I���֐�
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}