using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̒萔���Ǘ�����N���X
/// </summary>
public class PlayerConstants : MonoBehaviour
{
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // ���_�֘A
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    [Header("���_�֘A")]

    [Tooltip("�J�����̌��̍i��{��")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    /// <summary>
    /// Player�̃J�������̍i��{��
    /// </summary>
    public float CameraApertureBaseFactor
    {
        get { return CAMERA_APERTURE_BASE_FACTOR; }
    }

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // �̗͊֘A
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    [Header("�̗͊֘A")]
    [Tooltip("Player��HP�ő�l")]
    [SerializeField] int PLAYER_MAX_HP = 100;

    /// <summary>
    /// Player��HP�ő�l
    /// </summary>
    public int MaxHP
    {
        get { return PLAYER_MAX_HP; }
    }

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // �ړ��֘A
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    [Header("�ړ��֘A")]
    [Tooltip("Player�̕������x")]
    [SerializeField] float PLAYER_WALK_SPEED = 4f;

    /// <summary>
    /// Player�̕������x
    /// </summary>
    public float WalkSpeed
    {
        get { return PLAYER_WALK_SPEED; }
    }

    [Tooltip("Player�̑��葬�x")]
    [SerializeField] float PLAYER_RUN_SPEED = 8f;

    /// <summary>
    /// Player�̑��葬�x
    /// </summary>
    public float RunSpeed
    {
        get { return PLAYER_RUN_SPEED; }
    }

    [Tooltip("Player�̃W�����v��")]
    [SerializeField] Vector3 PLAYER_JUMP_FORTH = new Vector3(0, 3f, 0);

    /// <summary>
    /// Player�̃W�����v��
    /// </summary>
    public Vector3 JumpForce
    {
        get { return PLAYER_JUMP_FORTH; }
    }

    [Tooltip("Player�̉�]���x")]
    [SerializeField] float ROTATION_SPEED = 5.0f;

    /// <summary>
    /// Player�̉�]���x
    /// </summary>
    public float RotationSpeed
    {
        get { return ROTATION_SPEED; }
    }
}