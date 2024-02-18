using System.Collections;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

/// <summary>
/// �J�����Ɋւ��鏈�����܂Ƃ߂ĊǗ�����N���X
/// </summary>
public class CameraController : MonoBehaviour
{
    public static Action onZoom;
    public static Action<Vector2> onGenerateRay;

    [Tooltip("�J�����̌��̍i��{��")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    [Tooltip("�J�����̈ʒu�I�u�W�F�N�g")]
    [SerializeField] Transform viewPoint;

    [Tooltip("�J�����̈ʒu�I�u�W�F�N�g�̗\��")]
    [SerializeField] Transform sabViewPoint;

    // ���삷��J�����I�u�W�F�N�g
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
    // Damage���̗h�ꏈ��
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    public void Shake()
    {
        shakeCount = 0;
        StartCoroutine(ViewPointShake());
    }

    float shakeMagnitude = 0.2f;
    float shakeTime = 0.1f;
    float shakeCount = 0;

    IEnumerator ViewPointShake()
    {
        while(shakeCount < shakeTime)
        {
            float x = sabViewPoint.transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = sabViewPoint.transform.position.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            viewPoint.transform.position = new Vector3(x,y, sabViewPoint.transform.position.z);
            myCamera.transform.position = viewPoint.transform.position;

            shakeCount += Time.deltaTime;

            yield return null;
        }
        viewPoint.transform.position = sabViewPoint.transform.position;
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

    /// <summary>
    /// �J�n�n�_���珙�X�ɃY�[������
    /// </summary>
    /// <param name="adsZoom">�Y�[���{��</param>
    /// <param name="adsSpeed">�Y�[�����x</param>
    public void GunZoomIn(float adsZoom,float adsSpeed)
    {
        GunZoomIn(myCamera,adsZoom,adsSpeed);
    }

    /// <summary>
    /// ���̒n�_�ɏ��X�ɖ߂�
    /// </summary>
    /// <param name="adsSpeed">�Y�[�����x</param>
    public void GunZoomOut(float adsSpeed)
    {
        GunZoomOut(myCamera, CAMERA_APERTURE_BASE_FACTOR, adsSpeed);
    }

    /// <summary>
    /// �J��������ꏊ���w�肵��Ray�𐶐�
    /// </summary>
    /// <param name="camera">��������J����</param>
    /// <param name="generationPos">����������W</param>
    /// <returns>��������Ray</returns>
    public Ray GenerateRay(Vector2 generationPos)
    {
         return GenerateRay(myCamera, generationPos);
    }

    /// <summary>
    /// �J��������ꏊ���w�肵��Ray�𐶐�
    /// </summary>
    /// <param name="camera">��������J����</param>
    /// <param name="generationPos">����������W</param>
    /// <returns>��������Ray</returns>
    public Ray GenerateRay(Camera camera, Vector2 generationPos)
    {
        return camera.ViewportPointToRay(generationPos);
    }


    /// <summary>
    /// �J�n�n�_���珙�X�ɃY�[������
    /// </summary>
    /// <param name="camera">�Ώۂ̃J����</param>
    /// <param name="adsZoom">�Y�[���{��</param>
    /// <param name="adsSpeed">�Y�[�����x</param>
    public void GunZoomIn(Camera camera, float adsZoom, float adsSpeed)
    {
        camera.fieldOfView = Mathf.Lerp(
            camera.fieldOfView,         //�J�n�n�_
            adsZoom,                    //�ړI�n�_
            adsSpeed * Time.deltaTime); //�⊮���l
    }

    /// <summary>
    /// ���̒n�_�ɏ��X�ɖ߂�
    /// </summary>
    /// <param name="camera">�Ώۂ̃J����</param>
    /// <param name="adsSpeed">�Y�[�����x</param>
    public void GunZoomOut(Camera camera, float CameraBaseFactor, float adsSpeed)
    {
        camera.fieldOfView = Mathf.Lerp(
            camera.fieldOfView,         //�J�n�n�_
            CameraBaseFactor,           //�ړI�n�_
            adsSpeed * Time.deltaTime); //�⊮���l
    }
}