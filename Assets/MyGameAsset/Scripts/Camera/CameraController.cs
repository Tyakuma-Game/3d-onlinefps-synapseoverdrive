using System.Collections;
using UnityEngine;

/// <summary>
/// �J�����Ɋւ��鏈�����܂Ƃ߂ĊǗ�����N���X
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("�J�����̈ʒu�I�u�W�F�N�g")]
    [SerializeField] Transform viewPoint;
    Camera myCamera;

    void Start()
    {
        // �J�����i�[
        myCamera = Camera.main;
    }

    /// <summary>
    /// �J�����̍X�V����
    /// </summary>
    public void UpdatePosition()
    {
        // �J�����ʒu�X�V
        myCamera.transform.position = viewPoint.position;//���W
        myCamera.transform.rotation = viewPoint.rotation;//��]
    }

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // ���_�̉�]�pProgram
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    // y���̉�]���i�[�@��]����p
    float verticalMouseInput;

    /// <summary>
    /// Player�̎��_��]����
    /// </summary>
    /// <param name="rotaInput">��]�̂��߂̓��͏��</param>
    /// <param name="rotaSpeed">��]���x</param>
    /// <param name="rotationRange">��]�͈�</param>
    public void Rotation(Vector2 rotaInput, float rotaSpeed, float rotationRange)
    {
        //�ϐ���y���̃}�E�X���͕��̐��l�𑫂�
        verticalMouseInput += rotaInput.y * rotaSpeed;

        //�ϐ��̐��l���ۂ߂�i�㉺�̎��_�͈͐���j
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -rotationRange, rotationRange);

        //�c�̎��_��]�𔽉f
        viewPoint.rotation = Quaternion.Euler
            (-verticalMouseInput,                       //-��t���Ȃ��Ə㉺���]
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
    }

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // �Y�[���֘A
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    //[Tooltip("�J�����̌��̍i��{��")]
    //[SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    /// <summary>
    /// �J�����̃Y�[���𒲐�����
    /// </summary>
    /// <param name="targetZoom">�ڕW�̃Y�[���{��</param>
    /// <param name="zoomSpeed">�Y�[�����x</param>
    public void AdjustCameraZoom(float targetZoom, float zoomSpeed)
    {
        myCamera.fieldOfView = Mathf.Lerp(
            myCamera.fieldOfView,      //�J�n�n�_
            targetZoom,                //�ړI�n�_
            zoomSpeed * Time.deltaTime //�⊮���l
        );
    }

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    //�@Ray����
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    /// <summary>
    /// �J��������ꏊ���w�肵��Ray�𐶐�
    /// </summary>
    /// <param name="generationPos">����������W</param>
    /// <returns>��������Ray</returns>
    public Ray GenerateRay(Vector2 generationPos)
    {
         return myCamera.ViewportPointToRay(generationPos);
    }
}