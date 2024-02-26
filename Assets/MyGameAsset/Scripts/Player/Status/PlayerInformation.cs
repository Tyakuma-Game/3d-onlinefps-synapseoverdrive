using UnityEngine;
using UnityEngine.UI;

// TODO: ��蒼��

/// <summary>
/// Player�̏ڍ׏��Ɋւ���N���X
/// </summary>
public class PlayerInformation : MonoBehaviour
{
    [Header("�Q��")]

    [Tooltip("�v���C���[�̖��O�e�L�X�g")]
    [SerializeField] Text playerNameText;

    [Tooltip("�������e�L�X�g")]
    [SerializeField] Text kilesText;

    [Tooltip("���S���e�L�X�g")]
    [SerializeField] Text deathText;


    /// <summary>
    /// �v���C���[�̏ڍ׏����i�[����
    /// </summary>
    /// <param name="name">���O</param>
    /// <param name="kill">������</param>
    /// <param name="death">���S��</param>
    public void SetPlayerDetailes(string name, int kill, int death)
    {
        //�e��f�[�^���i�[
        playerNameText.text = name;
        kilesText.text = kill.ToString();
        deathText.text = death.ToString();
    }
}