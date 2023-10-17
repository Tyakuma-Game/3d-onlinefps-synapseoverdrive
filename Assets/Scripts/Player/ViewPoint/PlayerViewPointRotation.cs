using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerViewPointRotation : MonoBehaviour, IPlayerViewPointRotation
{
    [Tooltip("�J�����̈ʒu�I�u�W�F�N�g")]
    [SerializeField] Transform viewPoint;

    [Tooltip("���_�̏㉺��]�͈�")]
    [SerializeField] float VERTICAL_ROTATION_RANGE = 60f;
    float verticalMouseInput;   //y���̉�]���i�[�@��]����p

    /// <summary>
    /// Player�̎��_��]����
    /// </summary>
    /// <param name="rotaInput">��]�̂��߂̓��͏��</param>
    /// /// <param name="rotaSpeed">��]���x</param>
    public void Rotation(Vector2 rotaInput, float rotaSpeed)
    {
        //�ϐ���y���̃}�E�X���͕��̐��l�𑫂�
        verticalMouseInput += rotaInput.y;

        //�ϐ��̐��l���ۂ߂�i�㉺�̎��_�͈͐���j
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -VERTICAL_ROTATION_RANGE, VERTICAL_ROTATION_RANGE);

        //�c�̎��_��]�𔽉f
        viewPoint.rotation = Quaternion.Euler
            (-verticalMouseInput,                       //-��t���Ȃ��Ə㉺���]
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
    }
}