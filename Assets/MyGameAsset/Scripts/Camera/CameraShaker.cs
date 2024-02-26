using System.Collections;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// �J�����̗h�ꉉ�o�N���X
/// </summary>
public class CameraShaker : MonoBehaviourPunCallbacks
{
    [Header(" Settings ")]
    [SerializeField] float shakeMagnitude = 0.2f;
    [SerializeField] float shakeTime = 0.1f;

    [Header(" Elements ")]
    [SerializeField] Transform viewPoint;
    [SerializeField] Transform sabViewPoint;
    Camera myCamera;
    float shakeCount = 0;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        myCamera = Camera.main;         // ���C���J�������擾
        PlayerEvent.onDamage += Shake;  // �����o�^
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // ��������
        PlayerEvent.onDamage -= Shake;
    }

    /// <summary>
    /// �J�����̗h�ꉉ�o
    /// </summary>
    void Shake()
    {
        shakeCount = 0;                     // �h��J�E���g�����Z�b�g
        StartCoroutine(ViewPointShake());   // �h��R���[�`�����J�n
    }

    /// <summary>
    /// ���ۂɃJ������h�炷�R���[�`��
    /// </summary>
    IEnumerator ViewPointShake()
    {
        while (shakeCount < shakeTime)// �w�肵�����Ԃ��o�߂���܂Ń��[�v
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

        // �h�ꂪ�I�������J���������̈ʒu��
        viewPoint.transform.position = sabViewPoint.transform.position;
    }
}