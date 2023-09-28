using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI�Ǘ��N���X
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("�Q��")]

    [Tooltip("�e��̏������e�L�X�g")]
    [SerializeField] Text bulletsIHaveText;

    [Tooltip("�e��̑��U���ƍő呕�U�\��")]
    [SerializeField] Text bulletText;

    [Tooltip("HP�X���C�_�[�i�[")]
    [SerializeField] Slider hpSlider;

    [Tooltip("HP�̃e�L�X�g�i�[")]
    [SerializeField] Text hpText;

    [Tooltip("���S�p�l��")]
    [SerializeField] GameObject deathPanel;

    [Tooltip("�f�X�e�L�X�g")]
    [SerializeField] Text deathText;

    [Tooltip("�X�R�A�{�[�hUI")]
    [SerializeField] GameObject scoreboard;

    [Tooltip("�v���C���[���Z�b�g�X�N���v�g")]
    [SerializeField] PlayerInformation info;

    [Tooltip("�I���p�l��")]
    [SerializeField] GameObject endPanel;

    [Tooltip("�^�C�g����ʂɖ߂邩�I��������")]
    [SerializeField]
    GameObject returnToTitlePanel;


    /// <summary>
    /// �������̏e�̒e�����f
    /// </summary>
    /// <param name="ammoClip">�}�K�W�����e��</param>
    /// <param name="ammoClip">�}�K�W�����e��</param>
    /// <param name="ammunition">�����e��</param>
    public void SettingBulletsText(int ammoClipMax,int ammoClip, int ammunition)
    {
        bulletsIHaveText.text = "�~"+ammunition.ToString();
        bulletText.text = ammoClip + "/" + ammoClipMax;
    }


    /// <summary>
    /// HP�̍X�V
    /// </summary>
    /// <param name="maxhp">�ő�HP</param>
    /// <param name="currentHp">���݂�HP</param>
    public void UpdateHP(int maxhp, int currentHp)
    {
        hpText.text = $"{currentHp.ToString()} / {maxhp.ToString()}";
        hpSlider.maxValue = maxhp;
        hpSlider.value = currentHp;
    }


    /// <summary>
    /// �f�XUI�X�V
    /// </summary>
    /// <param name="name">�|�����v���C���[�̖��O</param>
    public void UpdateDeathUI(string name)
    {
        //�\��
        deathPanel.SetActive(true);

        //�e�L�X�g�X�V
        deathText.text = name + "�ɂ��ꂽ";

        //��莞�Ԍ�ɕ���
        Invoke("CloseDeathUI", 5f);
    }


    /// <summary>
    /// �������Ă��܂����ۂ̃f�XUI�\��
    /// </summary>
    public void UpdateDeathUI()
    {
        //�\��
        deathPanel.SetActive(true);

        //�e�L�X�g�X�V
        deathText.text = "���E�̋��ԂɈ��ݍ��܂�Ă��܂���...";

        //��莞�Ԍ�ɕ���
        Invoke("CloseDeathUI", 5f);
    }


    /// <summary>
    /// �f�XUI��\��
    /// </summary>
    private void CloseDeathUI()
    {
        deathPanel.SetActive(false);
    }


    /// <summary>
    /// �L���f�X�\�̕\���ؑ�
    /// </summary>
    public void ChangeScoreUI()
    {
        //�\�����Ă������\���ɁA��\���Ȃ�\��
        scoreboard.SetActive(!scoreboard.activeInHierarchy);
    }


    /// <summary>
    /// �^�C�g���ɖ߂邩�I����ʂ̕\���ؑ�
    /// </summary>
    public void ChangeReturnToTitlePanel()
    {
        //�\�����Ă������\���ɁA��\���Ȃ�\��
        returnToTitlePanel.SetActive(!returnToTitlePanel.activeInHierarchy);
    }


    /// <summary>
    /// �Q�[���I���p�l���\��
    /// </summary>
    public void OpenEndPanel()
    {
        endPanel.SetActive(true);
    }


    /// <summary>
    /// PlayerInformation�̏����擾
    /// </summary>
    public PlayerInformation GetPlayerInformation()
    {
        return this.info;
    }
}