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

    [Tooltip("�J�����̈ʒu�I�u�W�F�N�g")]
    [SerializeField] Transform viewPoint;

    [Tooltip("���_�ړ��̑��x")]
    [SerializeField] float MOUSE_SENSITIVITY = 1f;

    [Tooltip("���_�̏㉺��]�͈�")]
    [SerializeField] float VERTICAL_ROTATION_RANGE = 60f;

    [Tooltip("�J�����̌��̍i��{��")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    Vector2 mouseInput;         //���[�U�[�̃}�E�X���͂��i�[
    float verticalMouseInput;   //y���̉�]���i�[�@��]����p


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

    void Update()
    {
        //�����ȊO�Ȃ�
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //���_�ړ��֐�
        PlayerRotate();

        //�J�������W����
        cam.transform.position = viewPoint.position;//���W
        cam.transform.rotation = viewPoint.rotation;//��]
    }

    /// <summary>
    /// Player�̉���]�Əc�̎��_�ړ�
    /// </summary>
    public void PlayerRotate()
    {
        //�ϐ��Ƀ��[�U�[�̃}�E�X�̓������i�[
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X") * MOUSE_SENSITIVITY,
            Input.GetAxisRaw("Mouse Y") * MOUSE_SENSITIVITY);

        //����]�𔽉f
        transform.rotation = Quaternion.Euler       //�I�C���[�p�Ƃ��Ă̊p�x���Ԃ����
            (transform.eulerAngles.x,
            transform.eulerAngles.y + mouseInput.x, //�}�E�X��x���̓��͂𑫂�
            transform.eulerAngles.z);



        //�ϐ���y���̃}�E�X���͕��̐��l�𑫂�
        verticalMouseInput += mouseInput.y;

        //�ϐ��̐��l���ۂ߂�i�㉺�̎��_�͈͐���j
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -VERTICAL_ROTATION_RANGE, VERTICAL_ROTATION_RANGE);

        //�c�̎��_��]�𔽉f
        viewPoint.rotation = Quaternion.Euler
            (-verticalMouseInput,                       //-��t���Ȃ��Ə㉺���]
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
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