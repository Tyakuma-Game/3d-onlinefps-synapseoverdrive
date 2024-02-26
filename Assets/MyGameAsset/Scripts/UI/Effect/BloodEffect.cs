using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: ��蒼��
// Event�o�^�^��On�_���[�W����HPCallback�����ɏ�����t���銴���ŗǂ����I

public class BloodEffect : MonoBehaviour
{
    [Tooltip("���̉摜")]
    [SerializeField] Image bloodImage;

    /// <summary>
    /// ���̉摜
    /// </summary>
    /// <param name="maxhp">�ő�HP��</param>
    /// <param name="currentHp">���݂�HP��</param>
    public void BloodUpdate(int maxhp, int currentHp)
    {
        // HP�̊������v�Z
        float hpRatio = (float)currentHp / maxhp;

        // �A���t�@�l���v�Z
        float alphaValue = 1.0f - hpRatio; // 1.0 - HP�����ŃA���t�@�l��ݒ�

        // �A���t�@�l��ύX
        Color imageColor = bloodImage.color;
        imageColor.a = alphaValue;
        bloodImage.color = imageColor;
    }
}