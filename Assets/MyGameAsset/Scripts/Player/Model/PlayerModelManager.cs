using UnityEngine;
using Photon.Pun;

/// <summary>
/// �v���C���[�̃��f���Ɋւ��鏈�����s���N���X
/// </summary>
public class PlayerModelManager : MonoBehaviourPunCallbacks
{
    [Header("�v���C���[�̃��f��")]
    [SerializeField] GameObject[] playerModel;

    void Start()
    {
        // �����ȊO�̏ꍇ��
        if (!photonView.IsMine)
            return; // �����I��

        // ���f�����\����
        foreach (var model in playerModel)
            model.SetActive(false);
    }
}