using UnityEngine;

public class UIRotateImage : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // ��]���x�i�x/�b�j

    private void Update()
    {
        // �t���[�����ɉ�]������p�x���v�Z
        float rotationAngle = rotationSpeed * Time.deltaTime;

        // Z���𒆐S��Image����]������
        transform.Rotate(Vector3.forward, rotationAngle);
    }
}
