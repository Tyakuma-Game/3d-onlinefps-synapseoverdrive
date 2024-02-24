using UnityEngine;
using Photon.Pun;


/// <summary>
/// �v���C���[�̓ޗ�����Ǘ��N���X
/// </summary>
public class PlayerAbyssRespawner : MonoBehaviourPunCallbacks
{
    [Header("�萔")]
    [Tooltip("�ޗ�������s������")]
    [SerializeField] float PITFALL_COORDINATE = -25f;
    bool isRespawns = false;

    void Update()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
            return;

        //�����˔j���Ă���Ȃ�
        if (transform.position.y <= PITFALL_COORDINATE && isRespawns == false)
        {
            isRespawns = true;
            AbyssRespawn();
        }
    }

    /// <summary>
    /// Player�̓ޗ�����
    /// </summary>
    void AbyssRespawn()
    {
        //���S�֐����Ăяo��
        SpawnManager.instance.StartRespawnProcess();

        //���SUI���X�V
        UIManager.instance.UpdateDeathUI();
    }
}