using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

enum GunType
{
    HandGun,
    AssaultRifle,
    SniperRifle
}

/// <summary>
/// �v���C���[�̏e�Ǘ��N���X
/// </summary>
public class PlayerGunController : MonoBehaviourPunCallbacks
{
    [Header(" �e���̃X�e�[�^�X ")]
    [SerializeField] GunData[] gunDates;
    GunType selectedGunType = GunType.HandGun;
    List<int> ammunition = new List<int>();    // ���݂̏����e��
    List<int> ammoClip = new List<int>();      // �}�K�W�����̒e��


    [Header(" �e�̌����� ")]
    [Tooltip("�������_�p")]
    [SerializeField] GameObject[] gunsHolder;

    [Tooltip("���莋�_�p")]
    [SerializeField] GameObject[] OtherGunsHolder;

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    // ����̓A�N�V�����ɓ������銴���ŏ����I�ɏC������
    [SerializeField] Animator gunAnimator;

    [Header("�Q��")]
    [Tooltip("Player�̎��_�Ɋւ���N���X")]
    [SerializeField] CameraController cameraController;
    [SerializeField] PlayerAnimator playerAnimator; // Callback���g�p���銴���Ń��t�@�N�^�����O����I

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    List<GameObject> guns = new List<GameObject>();     // �eData�Ǘ��p    
    float shotTimer;                                    // �ˌ��Ԋu

    
    void Start()
    {
        //�����Ȃ̂�
        if (photonView.IsMine)
        {
            //�e�̐������[�v
            foreach (GameObject gun in gunsHolder)
            {
                //���X�g�ɒǉ�
                guns.Add(gun);
            }

            // GunData�̐��������[�v���āA�e�e�̒e���������
            foreach (var gun in gunDates)
            {
                // �����e����ő��
                ammunition.Add(gun.MaxAmmunition);
                // �}�K�W�����e����ő��
                ammoClip.Add(gun.MaxAmmoClip);
            }
        }
        else//���l��������OtherGunsHolder��\��
        {
            //�e�̐������[�v
            foreach (GameObject gun in OtherGunsHolder)
            {
                //���X�g�ɒǉ�
                guns.Add(gun);
            }
        }

        //�e�̕\���ؑ�
        switchGun();
    }

    void Update()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }
        
        //�e�̐؂�ւ�
        SwitchingGuns();

        //�`������
        Aim();

        //�ˌ��֐�
        Fire();

        //�����[�h�֐�
        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        //�e��e�L�X�g�X�V
        UIManager.instance.SettingBulletsText(gunDates[(int)selectedGunType].MaxAmmoClip,
            ammoClip[(int)selectedGunType], ammunition[(int)selectedGunType]);
    }

    /// <summary>
    /// �e�̐؂�ւ��L�[���͂����m
    /// </summary>
    public void SwitchingGuns()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            //�����e���Ǘ����鐔�l�𑝂₷
            selectedGunType++;

            //���X�g���傫�Ȑ��l�ɂȂ�΂O�ɖ߂�
            if ((int)selectedGunType >= Enum.GetValues(typeof(GunType)).Length)
            {
                selectedGunType = GunType.HandGun;
            }

            // �A�j���[�V����
            gunAnimator.SetTrigger("WeaponChange");

            //�e�̐؂�ւ��i���[�����̃v���C���[�S���Ăяo���j
            photonView.RPC("SetGun", RpcTarget.All, (int)selectedGunType);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            //�����e���Ǘ����鐔�l�����炷
            selectedGunType--;

            //0��菬������΃��X�g�̍ő吔�|�P�̐��l�ɐݒ�
            if (selectedGunType < 0)
            {
                selectedGunType = (GunType)guns.Count - 1;
            }
            // �A�j���[�V����
            gunAnimator.SetTrigger("WeaponChange");

            //�e�̐؂�ւ��i���[�����̃v���C���[�S���Ăяo���j
            photonView.RPC("SetGun", RpcTarget.All, (int)selectedGunType);
        }

        //���l�L�[�̓��͌��m�ŕ����؂�ւ���
        for (int i = 0; i < guns.Count; i++)
        {
            //���[�v�̐��l�{�P�����ĕ�����ɕϊ��B���̌�A�����ꂽ������
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                //�e���������l��ݒ�
                selectedGunType = (GunType)i;
                
                // �A�j���[�V����
                gunAnimator.SetTrigger("WeaponChange");

                //�e�̐؂�ւ��i���[�����̃v���C���[�S���Ăяo���j
                photonView.RPC("SetGun", RpcTarget.All, (int)selectedGunType);
            }
        }
    }


    IEnumerator DelayedSwitchGun()
    {
        yield return new WaitForSeconds(1f);

        //���X�g�����[�v����
        foreach (GameObject gun in guns)
        {
            //�e���\��
            gun.SetActive(false);
        }

        //�I�𒆂̏e�̂ݕ\��
        guns[(int)selectedGunType].SetActive(true);
    }

    /// <summary>
    /// �e�̕\���؂�ւ�
    /// </summary>
    void switchGun()
    {
        //���X�g�����[�v����
        foreach (GameObject gun in guns)
        {
            //�e���\��
            gun.gameObject.SetActive(false);
        }

        //�I�𒆂̏e�̂ݕ\��
        guns[(int)selectedGunType].SetActive(true);
    }


    /// <summary>
    /// �E�N���b�N�Ŕ`������
    /// </summary>
    public void Aim()
    {
        //�}�E�X�E�{�^�������Ă���Ƃ�
        if (Input.GetMouseButton(1))
        {
            // �A�j���[�V����
            gunAnimator.SetBool("IsZoom",true);

            //�Y�[���C��
            cameraController.GunZoomIn(gunDates[(int)selectedGunType].AdsZoom, gunDates[(int)selectedGunType].AdsSpeed);
        }
        else
        {
            // �A�j���[�V����
            gunAnimator.SetBool("IsZoom", false);

            //�Y�[���A�E�g
            cameraController.GunZoomOut(gunDates[(int)selectedGunType].AdsSpeed);
        }
    }


    /// <summary>
    /// ���N���b�N�̌��m
    /// </summary>
    public void Fire()
    {
        if (Input.GetMouseButton(0) && Time.time > shotTimer)
        {
            // �e��̎c�肪���邩����
            if (ammoClip[(int)selectedGunType] == 0)
            {
                // �e�؂�̉���炷
                // �A�j���[�V�������g�p������@�ɕ�����
                //photonView.RPC("NotShotSound", RpcTarget.All);

                // �I�[�g�����[�h
                Reload();

                // �����I��
                return;
            }

            //�e�̔��ˏ���
            FiringBullet();
        }
    }

    /// <summary>
    /// �e�ۂ̔���
    /// </summary>
    void FiringBullet()
    {
        // �A�j���[�V����
        gunAnimator.SetTrigger("Attack");

        // �A�j���[�V����
        playerAnimator.Attack(AttackType.Short);

        //Ray(����)���J�����̒�������ݒ�
        Vector2 pos = new Vector2(.5f, .5f);
        Ray ray = cameraController.GenerateRay(pos);

        //���C�𔭎�
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //�v���C���[�ɂԂ������ꍇ
            if (hit.collider.gameObject.tag == "Player")
            {
                //���̃G�t�F�N�g���l�b�g���[�N��ɐ���
                PhotonNetwork.Instantiate(gunDates[(int)selectedGunType].PlayerHitEffect.name, hit.point, Quaternion.identity);

                // �q�b�g�֐���S�v���C���[�ŌĂяo���Č����ꂽ�v���C���[��HP�𓯊�
                hit.collider.gameObject.GetPhotonView().RPC("Hit",
                    RpcTarget.All,
                    gunDates[(int)selectedGunType].ShotDamage,
                    photonView.Owner.NickName,
                    PhotonNetwork.LocalPlayer.ActorNumber);
            }
            else
            {
                //�e���G�t�F�N�g���� 
                GameObject bulletImpactObject = Instantiate(gunDates[(int)selectedGunType].NonPlayerHitEffect,
                    hit.point + (hit.normal * .002f),                   //�I�u�W�F�N�g���班���������Ă�����h�~
                    Quaternion.LookRotation(hit.normal, Vector3.up));   //���p�̕�����Ԃ��Ă��̕����ɉ�]������

                //���Ԍo�߂ō폜
                Destroy(bulletImpactObject, 10f);
            }
        }

        //�ˌ��Ԋu��ݒ�
        shotTimer = Time.time + gunDates[(int)selectedGunType].ShootInterval;

        //�I�𒆂̏e�̒e�򌸂炷
        ammoClip[(int)selectedGunType]--;
    }


    /// <summary>
    /// �����[�h
    /// </summary>
    void Reload()
    {
        // TODO: ���Ԃ�����ۂɂ����𒲐�����
        // �A�j���[�V����
        //gunAnimator.SetTrigger("Reload");

        //�����[�h�ŕ�[����e�����擾
        
        int amountNeed = gunDates[(int)selectedGunType].MaxAmmoClip - ammoClip[(int)selectedGunType];

        //�K�v�Ȓe��ʂƏ����e��ʂ��r
        int ammoAvailable = amountNeed < ammunition[(int)selectedGunType] ? amountNeed : ammunition[(int)selectedGunType];

        //�e�򂪖��^���̎��̓����[�h�ł��Ȃ�&�e����������Ă���Ƃ�
        if (amountNeed != 0 && ammunition[(int)selectedGunType] != 0)
        {
            //�����e�򂩂烊���[�h����e�򕪂�����
            ammunition[(int)selectedGunType] -= ammoAvailable;

            //�e�ɑ��U����
            ammoClip[(int)selectedGunType] += ammoAvailable;
        }
    }


    /// <summary>
    /// �e�̐؂�ւ�����
    /// </summary>
    [PunRPC]
    public void SetGun(int gunNo)
    {
        //�e�̐؂�ւ�
        if (gunNo < Enum.GetValues(typeof(GunType)).Length)
        {
            //�e�̔ԍ����Z�b�g
            selectedGunType = (GunType)gunNo;

            // �A�j���[�V����
            playerAnimator.IsWeaponChange();

            // 1�b�҂��Ă���؂�ւ���
            StartCoroutine(DelayedSwitchGun());
        }
    }
}