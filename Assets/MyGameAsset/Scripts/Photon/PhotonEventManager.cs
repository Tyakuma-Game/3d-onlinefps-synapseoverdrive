using Photon.Pun;
using System;
using UnityEngine;

/// <summary>
/// �l�b�g���[�N��ŃI�u�W�F�N�g���������ꂽ���ǂ����𔻒肷�鏈��
/// </summary>
public class PhotonEventManager : MonoBehaviour, IPunInstantiateMagicCallback
{
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.Sender.IsLocal)
        {
            Debug.Log("���g���l�b�g���[�N�I�u�W�F�N�g�𐶐����܂���");
        }
        else
        {
            Debug.Log("���v���C���[���l�b�g���[�N�I�u�W�F�N�g�𐶐����܂���");
        }

        //OnPlayerInstantiated?.Invoke(info.photonView.gameObject);
    }

}