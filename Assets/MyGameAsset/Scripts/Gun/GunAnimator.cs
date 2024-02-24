using Photon.Pun;
using UnityEngine;

public class GunAnimator : MonoBehaviourPunCallbacks
{
    [SerializeField] Animator gunAnimator;

    //string hashDamage = "Damage";
    //string hashAttackType = "AttackType";
    //string hashAttack = "Attack";
    string hashMoveSpeed = "MoveSpeed";
    //string hashIsGround = "IsGround";
    //string hashWeaponChange = "WeaponChange";

    void Start()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        gunAnimator = GetComponent<Animator>();
        PlayerMove.OnSpeedChanged += UpdateMoveSpeed;
    }

    void OnDestroy()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        PlayerMove.OnSpeedChanged -= UpdateMoveSpeed;
    }

    /// <summary>
    /// ���݂̈ړ����x�X�V
    /// </summary>
    /// <param name="speed">���݂̈ړ����x</param>
    void UpdateMoveSpeed(float speed) =>
        gunAnimator.SetFloat(hashMoveSpeed, speed, 0.1f, Time.deltaTime);



}