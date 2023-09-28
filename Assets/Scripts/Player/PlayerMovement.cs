using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Player�̈ړ��Ɋւ���N���X
/// </summary>
public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [Header("�ړ��֘A")]

    [Tooltip("Player�̕������x")]
    [SerializeField] float WALK_SPEED = 4f;

    [Tooltip("Player�̑��鑬�x")]
    [SerializeField] float RUN_SPEED = 8f;

    Vector3 moveDir;            //�v���C���[�̓��͊i�[�i�ړ��j
    Vector3 movement;           //�i�ޕ����̊i�[�ϐ�
    float activeMoveSpeed = 4;  //���݂̈ړ����x


    [Header("�W�����v�֘A")]

    [Tooltip("Player�̃W�����v��")]
    [SerializeField] Vector3 JUMP_FORCE = new Vector3(0, 6, 0);

    [Tooltip("�n�ʂ��ƔF�����郌�C���[")]
    [SerializeField] LayerMask groundLayers;

    [Tooltip("�W�����v���Ȃ̂�����t���O")]
    bool isJumping = false;

    Rigidbody rb;
    

    void Awake()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            return;//�����I��
        }
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            return;//�����I��
        }

        //�ړ��֐�
        PlayerMove();

        //�W�����v���Ă��Ȃ��Ȃ�
        if (!isJumping)
        {
            //����̊֐����Ă�
            PlayerRun();

            //�W�����v�֐����Ă�
            PlayerJump();
        }
    }

    /// <summary>
    /// Player�̈ړ�
    /// </summary>
    public void PlayerMove()
    {
        //���͂��i�[�iwasd����̓��́j
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"),
            0, Input.GetAxisRaw("Vertical"));

        //�x�N�g�����v�Z���Đ��K��
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized;

        //�ړ��ʂ��v�Z���Ĉړ�
        transform.position += movement * activeMoveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// �I�u�W�F�N�g�ڐG��
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        //�W�����v�����n�ʃ��C���[�̃I�u�W�F�N�g�Ƃ̐ڐG�Ȃ�
        if (isJumping && (groundLayers.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            //�W�����v������
            isJumping = false;
        }
    }

    /// <summary>
    /// Player�̃W�����v����
    /// </summary>
    public void PlayerJump()
    {
        //�W�����v�L�[��������Ă��邩����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�W�����v�t���O�𗧂Ă�
            isJumping = true;

            //�u�ԓI�ɐ^��ɗ͂�������
            rb.AddForce(JUMP_FORCE, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Player�̑��菈��
    /// </summary>
    public void PlayerRun()
    {
        //�_�b�V�����̈ړ����x�؂�ւ�
        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = RUN_SPEED;
        }
        else
        {
            activeMoveSpeed = WALK_SPEED;
        }
    }

    /// <summary>
    /// ���݂̈ړ��l���擾
    /// </summary>
    public Vector3 GetMoveDir()
    {
        return this.moveDir;
    }
}