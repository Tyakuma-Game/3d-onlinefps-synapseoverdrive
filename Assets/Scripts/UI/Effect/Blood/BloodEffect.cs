using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    [Tooltip("���̉摜")]
    [SerializeField] Image bloodImage;

    /// <summary>
    /// ���̉摜
    /// </summary>
    /// <param name="maxhp"></param>
    /// <param name="currentHp"></param>
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