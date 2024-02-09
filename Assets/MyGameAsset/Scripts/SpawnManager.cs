using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    [Header("�萔")]
    [Tooltip("�X�|�[������܂ł̗P�\����")]
    [SerializeField] float RESPAWN_INTERVAL = 5f;
    

    [Header("�Q��")]
    [Tooltip("�X�|�[���|�C���g�̃I�u�W�F�N�g�i�[�p�z��")]
    [SerializeField] Transform[] spawnPositons;

    [Tooltip("��������I�u�W�F�N�g")]
    [SerializeField] GameObject playerPrefab;

    [Tooltip("�v���C���[�̊K�w�Ǘ��p")]
    [SerializeField] GameObject Players;

    [Tooltip("���������v���C���[���i�[")]
    GameObject player;

    GameObject aaa;


    void Start()
    {
        //�X�|�[���|�C���g�I�u�W�F�N�g�����ׂĔ�\����
        foreach (var pos in spawnPositons)
        {
            pos.gameObject.SetActive(false);
        }

        //�l�b�g���[�N�ɐڑ�����Ă���Ȃ�
        if (PhotonNetwork.IsConnected)
        {
            //�v���C���[����
            SpawnPlayer();
        }
    }



    /// <summary>
    /// ���X�|�[���n�_�������_���擾
    /// </summary>
    public Transform GetSpawnPoint()
    {
        //�����_���ŃX�|�[���|�C���g���P�I��ňʒu����Ԃ�
        return spawnPositons[Random.Range(0, spawnPositons.Length)];
    }


    /// <summary>
    /// Player�𐶐�
    /// </summary>
    public void SpawnPlayer()
    {
        //�X�|�[�����W�����X�g�̒����烉���_���Ɏ擾
        Transform spawnPoint = GetSpawnPoint();

        //�l�b�g���[�N��Ƀv���C���[�𐶐�
        player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);//�폜�p�ɕۑ�
    }

    void DestryNetWarkPlayer()
    {
        //player���l�b�g���[�N�ォ��폜
        PhotonNetwork.Destroy(player);
    }

    /// <summary>
    /// Player�̃��X�|�[������
    /// </summary>
    public void Die()
    {
        if (player != null)
        {
            //��莞�Ԍ�v���C���[���X�|�[��
            Invoke("SpawnPlayer", RESPAWN_INTERVAL);
        }

        //��莞�Ԍ�v���C���[��j��
        Invoke("DestryNetWarkPlayer", RESPAWN_INTERVAL-0.5f);
    }
}