using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�̈ړ��N���X
/// </summary>
public class PlayerMove : MonoBehaviour, IPlayerMove
{
    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="direction">�ړ��̂��߂̓��͏��</param>
    /// <param name="moveSpeed">�ړ����x</param>
    public void Move(Vector3 direction, float moveSpeed)
    {
        // �v�Z
        Vector3 movement = ((transform.forward * direction.z)
                            + (transform.right * direction.x)).normalized;

        // �ړ�
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}