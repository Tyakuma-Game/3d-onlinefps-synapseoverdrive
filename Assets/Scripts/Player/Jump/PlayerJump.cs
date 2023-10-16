using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�̃W�����v�Ɋւ���N���X
/// </summary>
public class PlayerJump : MonoBehaviour, IPlayerJump
{
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// �W�����v����
    /// </summary>
    public void Jump(Vector3 jumpForth)
    {
        // �W�����v����
        myRigidbody.AddForce(jumpForth, ForceMode.Impulse);
    }
}