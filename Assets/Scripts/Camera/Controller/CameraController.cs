using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�����Ɋւ��鏈�����܂Ƃ߂ĊǗ�����N���X
/// </summary>
public class CameraController : MonoBehaviour
{
    Camera myCamera;


    ICameraZoom cameraZoom;

    // ���̓V�X�e��
    [Tooltip("�L�[�{�[�h�̓��͏���")]
    IKeyBoardInput keyBoardInput;

    [Tooltip("�}�E�X�̓��͏���")]
    IMouseInput mouseInput;


    void Start()
    {
        // �J�����i�[
        myCamera = Camera.main;

        // �����擾
        cameraZoom = GetComponent<ICameraZoom>();

        // ���͏���
        keyBoardInput = GetComponent<IKeyBoardInput>();
        mouseInput = GetComponent<IMouseInput>();
    }

    /// <summary>
    /// �J�����̃Y�[������
    /// </summary>
    /// <param name="adsZoom"></param>
    /// <param name="adsSpeed"></param>
    public void GunZoomIn(float adsZoom,float adsSpeed)
    {
        cameraZoom.GunZoomIn(myCamera,adsZoom,adsSpeed);
    }
}