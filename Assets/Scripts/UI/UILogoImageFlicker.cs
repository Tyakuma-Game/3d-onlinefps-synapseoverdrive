using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI�C���[�W�̓_�ŏ����N���X
/// </summary>
public class UILogoImageFlicker : MonoBehaviour
{
    [Tooltip("���S�̃C���[�W")]
    [SerializeField] Image titleLogoImage;

    [Tooltip("�A���t�@�l�̍ŏ��l")]
    [SerializeField] float minAlpha = 0.2f;

    [Tooltip("�A���t�@�l�̍ő�l")]
    [SerializeField] float maxAlpha = 1.0f;

    [Tooltip("�_�ő��x")]
    [SerializeField] float flickerSpeed = 1.0f;

    [Tooltip("���݂̓����x")]
    float currentAlpha;

    [Tooltip("�����x�����������ǂ����̃t���O")]
    bool increasing = true;

    void Start()
    {
        // �����ݒ�
        InitializeAlpha();
    }

    void Update()
    {
        // �����x�̑�����ԍX�V
        UpdateFlickerState();

        // �����x�̑�����ԍX�V
        UpdateAlphaValue();

        // �V�K�������K�p
        ApplyNewAlpha();
    }

    /// <summary>
    /// ������
    /// </summary>
    void InitializeAlpha()
    {
        currentAlpha = titleLogoImage.color.a;
    }

    /// <summary>
    /// �����x�̑�����ԍX�V
    /// </summary>
    void UpdateFlickerState()
    {
        if (currentAlpha >= maxAlpha)
        {
            increasing = false;
        }
        else if (currentAlpha <= minAlpha)
        {
            increasing = true;
        }
    }

    /// <summary>
    /// �����x�i���j�l�̍X�V
    /// </summary>
    private void UpdateAlphaValue()
    {
        float deltaAlpha = flickerSpeed * Time.deltaTime * (increasing ? 1 : -1);
        currentAlpha = Mathf.Clamp(currentAlpha + deltaAlpha, minAlpha, maxAlpha);
    }

    /// <summary>
    /// �V�K�������K�p
    /// </summary>
    void ApplyNewAlpha()
    {
        Color newColor = titleLogoImage.color;
        newColor.a = currentAlpha;
        titleLogoImage.color = newColor;
    }
}