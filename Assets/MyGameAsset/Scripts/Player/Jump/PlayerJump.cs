using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�̃W�����v�Ɋւ���N���X
/// </summary>
public class PlayerJump : MonoBehaviour, IPlayerJump
{
    Rigidbody myRigidbody;

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="rigidbody"></param>
    public void Init(Rigidbody rigidbody)
    {
        myRigidbody = rigidbody;
    }

    /// <summary>
    /// �W�����v����
    /// </summary>
    public void Jump(Vector3 jumpForth)
    {
        // �d���h�~��
        if (myRigidbody.velocity.y < jumpForth.y / 2)
        {
            // �͂�������
            myRigidbody.AddForce(jumpForth, ForceMode.VelocityChange);
        }
    }
}