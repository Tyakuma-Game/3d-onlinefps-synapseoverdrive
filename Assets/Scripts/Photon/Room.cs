using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

/// <summary>
/// ���[�����N���X
/// </summary>
public class Room : MonoBehaviour
{
    [Header("�Q��")]
    [Tooltip("���[�������f�p�e�L�X�g")]
    [SerializeField] Text buttonText;

    [Tooltip("�������̊i�[�ϐ�")]
    RoomInfo info;


    /// <summary>
    /// ���[���{�^���ɏڍׂ�o�^
    /// </summary>
    public void RegisterRoomDetails(RoomInfo info)
    {
        //���[����info�Ɉ�����info���i�[
        this.info = info;

        //���������X�V
        buttonText.text = this.info.Name;
    }


    /// <summary>
    /// ���̃��[���{�^�����Ǘ����Ă��郋�[���ɎQ��
    /// </summary>
    public void OpenRoom()
    {
        //���[���Q���֐����Ăяo��
        PhotonManager.instance.JoinRoom(info);
    }
}