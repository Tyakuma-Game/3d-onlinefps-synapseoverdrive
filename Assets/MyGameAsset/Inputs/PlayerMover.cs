using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �v���C���[�̈ړ����Ǘ�����N���X
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] PlayerData1 _playerData;

    private Rigidbody _rigidbody;
    private GameInputs _gameInputs;
    private Vector2 _moveInputValue;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // Action�X�N���v�g�̃C���X�^���X����
        _gameInputs = new GameInputs();

        // Action�C�x���g�o�^
        _gameInputs.Player.Move.started += OnMove;
        _gameInputs.Player.Move.performed += OnMove;
        _gameInputs.Player.Move.canceled += OnMove;
        _gameInputs.Player.Jump.performed += OnJump;

        // Input Action���@�\�����邽�߂ɂ́A
        // �L��������K�v������
        _gameInputs.Enable();
    }

    void OnDestroy()
    {
        // ���g�ŃC���X�^���X������Action�N���X��IDisposable���������Ă���̂ŁA
        // �K��Dispose����K�v������
        _gameInputs?.Dispose();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        // Move�A�N�V�����̓��͎擾
        _moveInputValue = context.ReadValue<Vector2>();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        // �W�����v����͂�^����
        _rigidbody.AddForce(Vector3.up * _playerData.JumpForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        // �ړ������̗͂�^����
        _rigidbody.AddForce(new Vector3(
            _moveInputValue.x,
            0,
            _moveInputValue.y
        ) * _playerData.MoveForce);
    }
}