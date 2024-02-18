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

        // �X�|�[���|�C���g�����������Ĕ�\����
        InitializeSpawnPoints();
    }

    void Start()
    {
        // �l�b�g���[�N�ڑ�������ꍇ�̂݃v���C���[���X�|�[��
        if (PhotonNetwork.IsConnected)
            SpawnPlayer();
    }

    /// <summary>
    /// �X�|�[���|�C���g�̏������E��\���ɐݒ�
    /// </summary>
    void InitializeSpawnPoints()
    {
        foreach (var pos in spawnPositions)
            pos.gameObject.SetActive(false);
    }

    /// <summary>
    /// �v���C���[���l�b�g���[�N��ɐ������A�ʒu��ݒ�
    /// </summary>
    public void SpawnPlayer()
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }

    /// <summary>
    /// �v���C���[�����S�����ۂ̏����@���X�|�[���܂ł̈�A�̗���J�n
    /// </summary>
    public void StartRespawnProcess()
    {
        StartCoroutine(RespawnPlayer());
    }

    /// <summary>
    /// �v���C���[�̃��X�|�[�����Ǘ�����R���[�`��
    /// </summary>
    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnInterval);   // ���S���o�ׂ̈̑҂�����
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