using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �~�j�}�b�v�����̊Ǘ��N���X
/// </summary>
public class MiniMapManager : MonoBehaviour
{
    [Header("�Q��")]
    [Tooltip("�~�j�}�b�v�p�̃A�C�R���Ǘ��N���X")]
    [SerializeField] MiniMapCharacterIconController miniMapCharacterIconController;

    [Tooltip("�~�j�}�b�v�p�̃J�����Ǘ��N���X")]
    [SerializeField] MiniMapCameraController miniMapCameraController;

    [Tooltip("��Ƃ���Q�[���I�u�W�F�N�g")]
    [SerializeField] GameObject player;


    void Start()
    {
        // �^�O����Player���������ĕێ�
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// �����ō폜���ꂽ�ۂɍĘA�����ꂸ�A�}�b�v�̍X�V���s���Ȃ��o�O����
    /// </summary>
    void Update()
    {
        // �L�����N�^�[�A�C�R���̍��W�X�V
        miniMapCharacterIconController.MiniMapIconUpdate(player.transform.position);

        // �~�j�}�b�v�J�����̍��W�X�V
        miniMapCameraController.MiniMapCameraUpdate(player.transform.position);
    }
}