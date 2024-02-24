using Photon.Pun;
using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// �J�����̃Y�[�������N���X
/// </summary>
public class CameraZoom : MonoBehaviourPunCallbacks
{
    [Header(" Settings ")]
    [SerializeField] float zoomThreshold = 0.01f;

    /// <summary>
    /// �Y�[����Ԃ��ύX���ꂽ�Ƃ��ɔ��΂���C�x���g
    /// </summary>
    public static Action<float, float> OnZoomStateChanged;
    Camera myCamera;
    Coroutine zoomCoroutine;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // �擾
        myCamera = Camera.main;

        // �����o�^
        OnZoomStateChanged += HandleZoomChange;
    }

    void OnDestroy()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        // ��������
        OnZoomStateChanged -= HandleZoomChange;
    }

    /// <summary>
    /// �Y�[����ԕύX���ɌĂяo����鏈��
    /// �Y�[���v���Ɋ�Â��ăY�[�����J�n
    /// </summary>
    /// <param name="targetZoom">�ڕW�̃Y�[���{��</param>
    /// <param name="zoomSpeed">�Y�[�����x</param>
    void HandleZoomChange(float targetZoom, float zoomSpeed)
    {
        // �����̃Y�[���R���[�`��������Β�~
        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);

        // �V�����Y�[���R���[�`���J�n
        zoomCoroutine = StartCoroutine(AdjustCameraZoom(targetZoom, zoomSpeed));
    }

    /// <summary>
    /// �Y�[�������X�ɒ�������R���[�`��
    /// �ڕW�̃Y�[���l�ɓ��B����܂Ńt�B�[���h�I�u�r���[���X�V��������
    /// </summary>
    /// <param name="targetZoom">�ڕW�̃Y�[���{��</param>
    /// <param name="zoomSpeed">�Y�[�����x</param>
    /// <returns>�R���[�`���̎��s����Ɏg�p�����IEnumerator</returns>
    IEnumerator AdjustCameraZoom(float targetZoom, float zoomSpeed)
    {
        // myCamera.fieldOfView��targetZoom�ɂȂ�܂Ń��[�v
        while (Mathf.Abs(myCamera.fieldOfView - targetZoom) > zoomThreshold)
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetZoom, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        // �ŏI�I�ɖڕW�l�ݒ�
        myCamera.fieldOfView = targetZoom;
    }
}