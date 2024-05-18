using UnityEngine;
using System.Collections;

/// <summary>
/// �J�����̗h�ꉉ�o�N���X
/// </summary>
public class CameraShaker : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] float shakeMagnitude = 0.2f;
    [SerializeField] float shakeTime = 0.1f;

    [Header(" Elements ")]
    [SerializeField] Transform viewPoint;
    [SerializeField] Transform sabViewPoint;
    Camera myCamera;
    float shakeCount = 0;


    void Awake()
    {
        // �����o�^
        PlayerEvent.OnPlayerInstantiated += HandlePlayerInstantiated;
    }

    void OnDestroy()
    {
        // ��������
        PlayerEvent.OnPlayerInstantiated -= HandlePlayerInstantiated;
        if (PlayerEvent.OnDamage != null)
            PlayerEvent.OnDamage -= OnShake;  // �����o�^
    }

    /// <summary>
    /// �v���C���[���C���X�^���X�����ꂽ�ۂɌĂ΂�鏈��
    /// </summary>
    void HandlePlayerInstantiated()
    {
        // �擾
        myCamera = Camera.main;

        // �����o�^
        PlayerEvent.OnDamage += OnShake;  // �����o�^
    }

    /// <summary>
    /// �J�����̗h�ꉉ�o
    /// </summary>
    void OnShake()
    {       
        shakeCount = 0;                     // �h��J�E���g�����Z�b�g
        StartCoroutine(ViewPointShake());   // �h��R���[�`�����J�n
    }

    /// <summary>
    /// ���ۂɃJ������h�炷�R���[�`��
    /// </summary>
    IEnumerator ViewPointShake()
    {
        while (shakeCount < shakeTime)
        {
            // �h��v�Z
            float x = sabViewPoint.transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = sabViewPoint.transform.position.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            viewPoint.transform.position = new Vector3(x, y, sabViewPoint.transform.position.z);
            
            // �X�V
            myCamera.transform.position = viewPoint.transform.position;
            shakeCount += Time.deltaTime;
            yield return null;
        }

        // ���̈ʒu��
        viewPoint.transform.position = sabViewPoint.transform.position;
    }
}