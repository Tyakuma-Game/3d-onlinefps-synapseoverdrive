using UnityEngine;
using Photon.Pun;
using System.Collections;

/// <summary>
/// �v���C���[�̃X�|�[���Ɋւ��鏈�����Ǘ�����N���X
/// </summary>
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }

    [Header(" Settings ")]
    [SerializeField] Transform parentObject;
    [SerializeField] float respawnInterval = 5f;

    [Header(" Elements ")]
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject playerPrefab;
    GameObject playerInstance;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // �X�|�[���|�C���g��������
        InitializeSpawnPoints();
    }

    void Start()
    {
        // �l�b�g���[�N�ڑ�������ꍇ�̂݃v���C���[���X�|�[��
        if (PhotonNetwork.IsConnected)
            SpawnPlayer();
    }

    /// <summary>
    /// �X�|�[���|�C���g�̏�����
    /// </summary>
    void InitializeSpawnPoints()
    {
        foreach (var pos in spawnPositions)
            pos.gameObject.SetActive(false);
    }

    /// <summary>
    /// �v���C���[���l�b�g���[�N��ɐ������A�ʒu�Ɛe��ݒ�
    /// </summary>
    public void SpawnPlayer()
    {
        // �v���C���[����
        Transform spawnPoint = GetRandomSpawnPoint();
        playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        
        // �e�I�u�W�F�N�g�ݒ�
        if (parentObject != null)
            playerInstance.transform.SetParent(parentObject, false);
    }

    /// <summary>
    /// �v���C���[�����S�����ۃ��X�|�[���܂ł̈�A�̏���
    /// </summary>
    public void StartRespawnProcess() =>
        StartCoroutine(RespawnPlayer());

    /// <summary>
    /// �v���C���[�̃��X�|�[�����Ǘ�����R���[�`��
    /// </summary>
    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnInterval);   // ���S���o�p�̑҂�����
        PhotonNetwork.Destroy(playerInstance);
        SpawnPlayer();
    }

    /// <summary>
    /// �g�p�\�ȃX�|�[���|�C���g���烉���_���Ɉ�I��
    /// </summary>
    Transform GetRandomSpawnPoint()
    {
        return spawnPositions[Random.Range(0, spawnPositions.Length)];
    }
}