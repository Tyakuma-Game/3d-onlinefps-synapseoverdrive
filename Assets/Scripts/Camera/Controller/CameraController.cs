using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// �J�����Ɋւ��鏈�����܂Ƃ߂ĊǗ�����N���X
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("�J�����̌��̍i��{��")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    [Tooltip("�J�����̈ʒu�I�u�W�F�N�g")]
    [SerializeField] Transform viewPoint;

    [Tooltip("�J�����̈ʒu�I�u�W�F�N�g�̗\��")]
    [SerializeField] Transform sabViewPoint;

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
    /// �J�����̍X�V����
    /// </summary>
    public void UpdatePosition()
    {
        // �J�����ʒu�X�V
        myCamera.transform.position = viewPoint.position;//���W
        myCamera.transform.rotation = viewPoint.rotation;//��]
    }

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // �e���ˎ��̉��o
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    bool isRecoiling = false;       // �e���ˉ��o�����ǂ����������t���O
    Quaternion originalRotation;
    float recoilAngle = 1.5f;       // ������Ɍ�����p�x
    float recoilDuration = 0.3f;    // ������ɂ��鎞��
    float returnDuration = 0.3f;    // ���̊p�x�ɖ߂鎞��

    public void ApplyRecoil()
    {
        // ��������������U�����I

        //if (!isRecoiling)
        //{
        //    StartCoroutine(RecoilCoroutine());
        //}
    }

    IEnumerator RecoilCoroutine()
    {
        isRecoiling = true;

        Quaternion targetRotation = Quaternion.Euler(sabViewPoint.transform.eulerAngles + new Vector3(-recoilAngle, 0, 0));
        Quaternion startRotation = myCamera.transform.rotation;
        float startTime = Time.time;

        // ������ɉ�]
        while (Time.time < startTime + recoilDuration)
        {
            float t = (Time.time - startTime) / recoilDuration;
            myCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        // ���̊p�x�ɖ߂�
        startTime = Time.time;
        while (Time.time < startTime + returnDuration)
        {
            float t = (Time.time - startTime) / returnDuration;
            myCamera.transform.rotation = Quaternion.Slerp(myCamera.transform.rotation, originalRotation, t);
            yield return null;
        }

        isRecoiling = false;
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