using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// �v���C���[�̃��f���Ɋւ��鏈�����s���N���X
/// </summary>
public class PlayerModelManager : MonoBehaviourPunCallbacks
{
    [Header("�v���C���[�̃��f��")]
    [Tooltip("�v���C���[���f�����i�[")]
    [SerializeField] GameObject[] playerModel;

    void Start()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //���f���S��
        foreach (var model in playerModel)
        {
            model.SetActive(false);//��\��
        }
    }
}