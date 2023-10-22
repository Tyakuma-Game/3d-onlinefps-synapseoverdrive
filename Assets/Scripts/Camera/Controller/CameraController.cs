using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

/// <summary>
/// �J�����Ɋւ��鏈�����܂Ƃ߂ĊǗ�����N���X
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("�J�����̌��̍i��{��")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    // ���삷��J�����I�u�W�F�N�g
    Camera myCamera;


    ICameraZoom cameraZoom;
    ICameraRay cameraRay;

    void Start()
    {
        // �J�����i�[
        myCamera = Camera.main;

        // �����擾
        cameraZoom = GetComponent<ICameraZoom>();
        cameraRay = GetComponent<ICameraRay>();
    }

    /// <summary>
    /// �J�n�n�_���珙�X�ɃY�[������
    /// </summary>
    /// <param name="adsZoom">�Y�[���{��</param>
    /// <param name="adsSpeed">�Y�[�����x</param>
    public void GunZoomIn(float adsZoom,float adsSpeed)
    {
        cameraZoom.GunZoomIn(myCamera,adsZoom,adsSpeed);
    }

    /// <summary>
    /// ���̒n�_�ɏ��X�ɖ߂�
    /// </summary>
    /// <param name="adsSpeed">�Y�[�����x</param>
    public void GunZoomOut(float adsSpeed)
    {
        cameraZoom.GunZoomOut(myCamera, CAMERA_APERTURE_BASE_FACTOR, adsSpeed);
    }

    /// <summary>
    /// �J��������ꏊ���w�肵��Ray�𐶐�
    /// </summary>
    /// <param name="camera">��������J����</param>
    /// <param name="generationPos">����������W</param>
    /// <returns>��������Ray</returns>
    public Ray GenerateRay(Vector2 generationPos)
    {
         return cameraRay.GenerateRay(myCamera, generationPos);
    }
}