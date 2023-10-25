using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �V���A���C�Y�\�ȃN���X�Ƃ��Ē�`
[Serializable]
public class CurveControlledBob : MonoBehaviour
{
    [Header("�w�b�h�{�u�͈̔�")]
    [Tooltip("�w�b�h�{�u�̐����͈�")]
    [SerializeField] float HorizontalBobRange = 0.33f;
    [Tooltip("�w�b�h�{�u�̐����͈�")]
    [SerializeField] float VerticalBobRange = 0.33f;

    [Header("�w�b�h�{�u�̃A�j���[�V�����J�[�u")]
    [SerializeField]
    AnimationCurve Bobcurve = new AnimationCurve(
        new Keyframe(0f, 0f),
        new Keyframe(0.5f, 1f),
        new Keyframe(1f, 0f),
        new Keyframe(1.5f, -1f),
        new Keyframe(2f, 0f)); // �T�C���J�[�u�ɂ��w�b�h�{�u

    [Header("���������Ɛ��������̔䗦")]
    [Tooltip("���������Ɛ��������̃{�u�̔䗦")]
    [SerializeField] float VerticaltoHorizontalRatio = 1f;

    [Tooltip("�{�u�T�C�N����X���ʒu")]
    float m_CyclePositionX;
    [Tooltip("�{�u�T�C�N����Y���ʒu")]
    float m_CyclePositionY;

    [Tooltip("�{�u�̊�{�Ԋu")]
    float m_BobBaseInterval;

    [Tooltip("�I���W�i���̃J�����ʒu��ێ�")]
    Vector3 m_OriginalCameraPosition;

    [Tooltip("�^�C�����v��")]
    float m_Time;

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="camera">�J�����̏����ʒu</param>
    /// <param name="bobBaseInterval">�{�u�̊�{�Ԋu</param>
    public void Setup(Camera camera, float bobBaseInterval)
    {
        m_BobBaseInterval = bobBaseInterval;
        m_OriginalCameraPosition = camera.transform.localPosition;

        // �A�j���[�V�����J�[�u�̍Ō�̃L�[�t���[���̎��Ԃ��擾
        m_Time = Bobcurve[Bobcurve.length - 1].time;
    }

    /// <summary>
    /// �w�b�h�{�u�����s
    /// </summary>
    /// <param name="speed">���������x</param>
    /// <returns>�V�����J�����ʒu</returns>
    public Vector3 DoHeadBob(float speed)
    {
        // X�������̃{�u�ʒu��Y�������̃{�u�ʒu���v�Z
        float xPos = m_OriginalCameraPosition.x + (Bobcurve.Evaluate(m_CyclePositionX) * HorizontalBobRange);
        float yPos = m_OriginalCameraPosition.y + (Bobcurve.Evaluate(m_CyclePositionY) * VerticalBobRange);

        // �{�u�T�C�N���ʒu���X�V
        m_CyclePositionX += (speed * Time.deltaTime) / m_BobBaseInterval;
        m_CyclePositionY += ((speed * Time.deltaTime) / m_BobBaseInterval) * VerticaltoHorizontalRatio;

        // �{�u�T�C�N���ʒu���^�C���𒴂����ꍇ�A���������
        if (m_CyclePositionX > m_Time)
        {
            m_CyclePositionX = m_CyclePositionX - m_Time;
        }
        if (m_CyclePositionY > m_Time)
        {
            m_CyclePositionY = m_CyclePositionY - m_Time;
        }

        // �V�����J�����ʒu��Ԃ�
        return new Vector3(xPos, yPos, 0f);
    }
}
