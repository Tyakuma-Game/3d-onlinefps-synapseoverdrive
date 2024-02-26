using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.SceneManagement;

// TODO: ��������K�͂ɍ�蒼��

/// <summary>
/// �Q�[�����ɔ��������C�x���g�����N���X
/// </summary>
public class EventManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>
    /// �R���|�[�l���g���I���ɂȂ�Ǝ��s
    /// </summary>
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);//�ǉ�
    }


    /// <summary>
    /// �R���|�[�l���g���I�t�ɂȂ�Ǝ��s
    /// </summary>
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);//�폜
    }


    /// <summary>
    /// ���[�J���v���C���[�̃��[���ޏo���Ɏ��s
    /// </summary>
    public override void OnLeftRoom()
    {
        //���N���X�����Ăяo��
        base.OnLeftRoom();

        //�^�C�g���V�[����ǂݍ���
        SceneManager.LoadScene(0);
    }


    /// <summary>
    /// �C�x���g�������ɌĂяo�����
    /// </summary>
    /// <param name="photonEvent">���̃C�x���g�f�[�^</param>
    public void OnEvent(EventData photonEvent)
    {
        //����̃C�x���g������
        if (photonEvent.Code < 200) //��200�ȏ��photon�Ǝ��̃C�x���g
        {
            //�C�x���g�R�[�h���i�[�i�^�ϊ��j
            EventCodes eventCode = (EventCodes)photonEvent.Code;

            //�C�x���g�̃J�X�^���f�[�^�ɃA�N�Z�X
            object[] data = (object[])photonEvent.CustomData;

            //�Ή����������s
            switch (eventCode)
            {
                case EventCodes.NewPlayer:  //�}�X�^�[���V�K���[�U�[��񏈗�����
                    GameManager.instance.NewPlayerSet(data);
                    break;

                case EventCodes.ListPlayers://���[�U�[�������L
                    GameManager.instance.ListPlayersSet(data);
                    break;

                case EventCodes.UpdateStat: //�L���f�X���̍X�V
                    GameManager.instance.ScoreSet(data);
                    break;
            }
        }
    }
}