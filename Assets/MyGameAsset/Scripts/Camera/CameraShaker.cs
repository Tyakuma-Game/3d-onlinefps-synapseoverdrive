using System.Collections;
using UnityEngine;
using Photon.Pun;

public class CameraShaker : MonoBehaviourPunCallbacks
{
    [SerializeField] float shakeMagnitude = 0.2f;
    [SerializeField] float shakeTime = 0.1f;
    [SerializeField] float shakeCount = 0;

    [Tooltip("�J�����̈ʒu�I�u�W�F�N�g")]
    [SerializeField] Transform viewPoint;

    [Tooltip("�J�����̈ʒu�I�u�W�F�N�g�̗\��")]
    [SerializeField] Transform sabViewPoint;
    Camera myCamera;

    void Start()
    {
        // ���g�łȂ��ꍇ�͏����I��
        if (!photonView.IsMine)
            return;

        // �J�����i�[
        myCamera = Camera.main;

        // �J�����̗h����_���[�WEvent�ɒǉ�
        PlayerEvent.onDamage += Shake;
    }

    void OnDestroy()
    {
        // ���g�łȂ��ꍇ�͏����I��
        if (!photonView.IsMine)
            return;

        // �J�����̗h���Event����폜
        PlayerEvent.onDamage -= Shake;
    }


    void Shake()
    {
        shakeCount = 0;
        StartCoroutine(ViewPointShake());
    }

    IEnumerator ViewPointShake()
    {
        while (shakeCount < shakeTime)
        {
            float x = sabViewPoint.transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = sabViewPoint.transform.position.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            viewPoint.transform.position = new Vector3(x, y, sabViewPoint.transform.position.z);
            myCamera.transform.position = viewPoint.transform.position;

            shakeCount += Time.deltaTime;

            yield return null;
        }
        viewPoint.transform.position = sabViewPoint.transform.position;
    }
}