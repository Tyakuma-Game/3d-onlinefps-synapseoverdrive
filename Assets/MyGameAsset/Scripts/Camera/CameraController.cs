using UnityEngine;

// TODO: �����Event�쓮�^�ɕύX���Č���������I

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
        // �擾
        myCamera = Camera.main;
    }

    void Update()
    { 
        // �ʒu�X�V
        myCamera.transform.position = viewPoint.position;
        myCamera.transform.rotation = viewPoint.rotation;
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