using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �v���C���[�̎��_��]�Ɋւ���N���X
/// </summary>
public class PlayerViewPointRotation : MonoBehaviour, IPlayerViewPointRotation
{
    // y���̉�]���i�[�@��]����p
    float verticalMouseInput;

    /// <summary>
    /// Player�̎��_��]����
    /// </summary>
    /// <param name="rotaInput">��]�̂��߂̓��͏��</param>
    /// <param name="viewPoint">���_���W</param>
    /// <param name="rotationRange">��]�͈�</param>
    public Transform Rotation(Vector2 rotaInput, Transform viewPoint, float rotationRange)
    {
        //�ϐ���y���̃}�E�X���͕��̐��l�𑫂�
        verticalMouseInput += rotaInput.y;

        //�ϐ��̐��l���ۂ߂�i�㉺�̎��_�͈͐���j
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -rotationRange, rotationRange);

        //�c�̎��_��]�𔽉f
        viewPoint.rotation = Quaternion.Euler
            (-verticalMouseInput,                       //-��t���Ȃ��Ə㉺���]
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);

        return viewPoint;
    }
}