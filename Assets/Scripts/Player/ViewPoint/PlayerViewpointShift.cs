using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Player�̎��_�ړ��Ɋւ���N���X
/// </summary>
public class PlayerViewpointShift : MonoBehaviourPunCallbacks
{
    [Header("Player���_�ړ��֘A")]

    [Tooltip("�J����")]
    [SerializeField] Camera cam;

    [Tooltip("�J�����̌��̍i��{��")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    void Start()
    {
        //�����ȊO�Ȃ�
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //���C���J�����i�[
        cam = Camera.main;
    }

    /// <summary>
    /// �J�n�n�_���珙�X�ɃY�[������
    /// </summary>
    /// <param name="adsZoom">�`�����ݎ��̃Y�[��</param>
    /// <param name="adsSpeed">�`�����݂̑��x</param>
    public void GunZoomIn(float adsZoom, float adsSpeed)
    {
        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,            //�J�n�n�_
            adsZoom,                    //�ړI�n�_
            adsSpeed * Time.deltaTime); //�⊮���l
    }

    /// <summary>
    /// ���̒n�_�ɏ��X�ɖ߂�
    /// </summary>
    /// <param name="adsSpeed">�`�����݂̑��x</param>
    public void GunZoomOut(float adsSpeed)
    {
        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,            //�J�n�n�_
            CAMERA_APERTURE_BASE_FACTOR,//�ړI�n�_
            adsSpeed * Time.deltaTime); //�⊮���l
    }

    /// <summary>
    /// �J�����̒�������Ray�𐶐�
    /// </summary>
    public Ray GenerateRayFromCameraCenter()
    {
        return cam.ViewportPointToRay(new Vector2(.5f, .5f));
    }
}