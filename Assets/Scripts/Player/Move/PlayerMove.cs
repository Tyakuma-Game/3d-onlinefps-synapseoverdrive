using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�̈ړ��N���X
/// </summary>
public class PlayerMove : MonoBehaviour, IPlayerMove
{
    Rigidbody myRigidbody;

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="rigidbody">�����v�Z�p</param>
    public void Init(Rigidbody rigidbody)
    {
        myRigidbody = rigidbody;
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="direction">�ړ��̂��߂̓��͏��</param>
    /// <param name="moveSpeed">�ړ����x</param>
    public void Move(Vector3 direction, float moveSpeed)
    {
        // �v�Z
        Vector3 movement = ((transform.forward * direction.z)
                            + (transform.right * direction.x)).normalized * moveSpeed * Time.deltaTime;

        // TODO: Rigidbody���g�p�������W�ړ��ɏC��
        // ����Rigidbody���g�p������@�ɐ؂�ւ����Ƃ��뎩���Photon�l�b�g���[�N��ł̍��W�\����������肭���삵�Ȃ��Ȃ���
        // position�����ł��������̂Ƀo�O�͔������Ă��Ȃ��׌�قǏC��

        // �ړ�
        transform.position += movement;
    }
}