using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UIElements;

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

    [SerializeField] CurveControlledBob curveControlledBob;

    void Start()
    {
        // �J�����i�[
        myCamera = Camera.main;

        // �����擾
        cameraZoom = GetComponent<ICameraZoom>();
        cameraRay = GetComponent<ICameraRay>();
    }


    public void UpdatePosition(Transform viewPoint,float moveSpeed)
    {

        Vector3 cameraPositionOffset = CurveControlledBobDoHeadBob(moveSpeed);
        myCamera.transform.localPosition = cameraPositionOffset;

        // �J�����ʒu�X�V
        //myCamera.transform.position = viewPoint.position;//���W
        //myCamera.transform.rotation = viewPoint.rotation;//��]
    }

    /// <summary>
    /// ���_�̗h��Z�b�g�A�b�v
    /// </summary>
    /// <param name="bobBaseInterval">�{�u�̊�{�Ԋu</param>
    public void CurveControlledBobSetUp(float bobBaseInterval)
    {
        curveControlledBob.Setup(myCamera, bobBaseInterval);
    }

    /// <summary>
    /// ���_�̗h����s��
    /// </summary>
    /// <param name="speed">�h��̑��x</param>
    /// <returns></returns>
    public Vector3 CurveControlledBobDoHeadBob(float speed)
    {
        return curveControlledBob.DoHeadBob(speed);
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