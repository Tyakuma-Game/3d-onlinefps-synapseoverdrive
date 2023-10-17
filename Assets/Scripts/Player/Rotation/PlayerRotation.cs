using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�̉�]�N���X
/// </summary>
public class PlayerRotation : MonoBehaviour, IPlayerRotation
{
    /// <summary>
    /// Player�̉�]����
    /// </summary>
    /// <param name="rotaInput">��]�̂��߂̓��͏��</param>
    /// /// <param name="rotaSpeed">��]���x</param>
    public void Rotation(Vector2 rotaInput, float rotaSpeed)
    {
        // �v�Z
        Vector2 rotation = new Vector2(rotaInput.x * rotaSpeed, 0);

        //����]�𔽉f
        transform.rotation = Quaternion.Euler           //�I�C���[�p�Ƃ��Ă̊p�x���Ԃ����
                (transform.eulerAngles.x,
                transform.eulerAngles.y + rotation.x,   //�}�E�X��x���̓��͂𑫂�
                transform.eulerAngles.z);
    }
}