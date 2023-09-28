using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// ���݂̐F���
/// </summary>
public enum ColorState
{
    Red,    // �ԐF
    Blue    // �F
}

/// <summary>
/// player�̐F�Ǘ��N���X
/// </summary>
public class PlayerCollar : MonoBehaviour
{
    [Tooltip("���̃����O")]
    [SerializeField] GameObject magicRing;

    SpriteRenderer magicRingRenderer;
    ColorState currentColorState;


    void Start()
    {
        magicRingRenderer = magicRing.GetComponent<SpriteRenderer>();

        // �����̐ԐF��ݒ肷��
        currentColorState = ColorState.Red;     // �F�̃X�e�[�^�X��ݒu����
        magicRingRenderer.color = Color.red;    // �F����ݒ�
    }


    void Update()
    {
        // Q�L�[�������ꂽ��F��؂�ւ���
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // �F��Ԃ�؂�ւ�
            SwitchColor();
        }
    }


    /// <summary>
    /// �F��؂�ւ���
    /// </summary>
    void SwitchColor()
    {
        // ���݂̐F���ԂȂ�ɁA�Ȃ�Ԃɐ؂�ւ���
        if (currentColorState == ColorState.Red)
        {
            
            magicRingRenderer.color = Color.blue;
            currentColorState = ColorState.Blue;
        }
        else
        {
            magicRingRenderer.color = Color.red;

            currentColorState = ColorState.Red;
        }
    }


    /// <summary>
    /// ���݂̐F�����擾����
    /// </summary>
    /// <returns>���݂̐F���</returns>
    public ColorState GetColorState()
    {
        return this.currentColorState;
    }
}
