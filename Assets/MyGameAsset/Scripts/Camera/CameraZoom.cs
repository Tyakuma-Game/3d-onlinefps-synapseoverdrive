using UnityEngine;
using System.Collections;

/// <summary>
/// �J�����̃Y�[�������N���X
/// </summary>
public class CameraZoom : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] float zoomThreshold = 0.01f;

    Camera myCamera;
    Coroutine zoomCoroutine;

    void Awake()
    {
        // �����o�^
        PlayerEvent.OnPlayerInstantiated += HandlePlayerInstantiated;
    }

    void OnDestroy()
    {
        // ��������
        PlayerEvent.OnPlayerInstantiated -= HandlePlayerInstantiated;
        CameraEvents.OnZoomStateChanged -= HandleZoomChange;
    }

    /// <summary>
    /// �v���C���[���C���X�^���X�����ꂽ�ۂɌĂ΂�鏈��
    /// </summary>
    void HandlePlayerInstantiated()
    {
        // �擾
        myCamera = Camera.main;

        // �����o�^
        CameraEvents.OnZoomStateChanged += HandleZoomChange;
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

        // �V�Y�[���R���[�`���J�n
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
        float deltaZoom = Mathf.Abs(myCamera.fieldOfView - targetZoom);
        while (deltaZoom > zoomThreshold)
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetZoom, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        // ������
        myCamera.fieldOfView = targetZoom;
    }
}