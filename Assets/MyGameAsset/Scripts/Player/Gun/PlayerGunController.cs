using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

/// <summary>
/// �v���C���[�̏e�Ǘ��N���X
/// </summary>
public class PlayerGunController : MonoBehaviourPunCallbacks
{
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // �ύX�Ȃ�
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    [SerializeField] Animator gunAnimator;

    [Header("�Q��")]
    [Tooltip("Player�̎��_�Ɋւ���N���X")]
    [SerializeField] CameraController cameraController;

    TestAnimatorController testAnimatorController;

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    [Header("�e�֘A")]
    [Tooltip("��e���̃G�t�F�N�g")]
    [SerializeField] GameObject hitEffect;

    [Tooltip("�e�̊Ǘ��z��")]
    [SerializeField] List<GunStatus> guns = new List<GunStatus>();
    
    [Tooltip("�e�z���_�[ �������_�p")]
    [SerializeField] GunStatus[] gunsHolder;

    [Tooltip("�e�z���_�[ ���莋�_�p")]
    [SerializeField] GunStatus[] OtherGunsHolder;

    


    int selectedGun = 0;                                //�I�𒆂̕���Ǘ��p���l
    float shotTimer;                                    //�ˌ��Ԋu

    [Tooltip("�����e��")]
    [SerializeField] int[] ammunition;

    [Tooltip("�ō������e��")]
    [SerializeField] int[] maxAmmunition;

    [Tooltip("�}�K�W�����̒e��")]
    [SerializeField] int[] ammoClip;

    [Tooltip("�}�K�W���ɓ���ő�̐�")]
    [SerializeField] int[] maxAmmoClip;

    //UI�Ǘ�
    UIManager uIManager;

    void Start()
    {
        //�����Ȃ̂�
        if (photonView.IsMine)
        {
            //�e�̐������[�v
            foreach (GunStatus gun in gunsHolder)
            {
                //���X�g�ɒǉ�
                guns.Add(gun);
            }

            //�^�O����UIManager��T��
            uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        }
        else//���l��������OtherGunsHolder��\��
        {
            //�e�̐������[�v
            foreach (GunStatus gun in OtherGunsHolder)
            {
                //���X�g�ɒǉ�
                guns.Add(gun);
            }
        }

        testAnimatorController = GetComponent<TestAnimatorController>();

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
        uIManager.SettingBulletsText(GetGunAmmoClipMax(), GetGunAmmoClip(), GetGunAmmunition());
    }

    /// <summary>
    /// �ݒ莞�Ԗ��Ɏ��s
    /// </summary>
    void FixedUpdate()
    {
        //�����ȊO�Ȃ�
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //�e��e�L�X�g�X�V
        uIManager.SettingBulletsText(GetGunAmmoClipMax(),ammoClip[selectedGun], ammunition[selectedGun]);
    }

    /// <summary>
    /// �e�̐؂�ւ��L�[���͂����m
    /// </summary>
    public void SwitchingGuns()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            //�����e���Ǘ����鐔�l�𑝂₷
            selectedGun++;

            //���X�g���傫�Ȑ��l�ɂȂ�΂O�ɖ߂�
            if (selectedGun >= guns.Count)
            {
                selectedGun = 0;
            }

            //�e�̐؂�ւ��i���[�����̃v���C���[�S���Ăяo���j
            photonView.RPC("SetGun", RpcTarget.All, selectedGun);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            //�����e���Ǘ����鐔�l�����炷
            selectedGun--;

            //0��菬������΃��X�g�̍ő吔�|�P�̐��l�ɐݒ�
            if (selectedGun < 0)
            {
                selectedGun = guns.Count - 1;
            }

            //�e�̐؂�ւ��i���[�����̃v���C���[�S���Ăяo���j
            photonView.RPC("SetGun", RpcTarget.All, selectedGun);
        }

        //���l�L�[�̓��͌��m�ŕ����؂�ւ���
        for (int i = 0; i < guns.Count; i++)
        {
            //���[�v�̐��l�{�P�����ĕ�����ɕϊ��B���̌�A�����ꂽ������
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                //�e���������l��ݒ�
                selectedGun = i;

                //�e�̐؂�ւ��i���[�����̃v���C���[�S���Ăяo���j
                photonView.RPC("SetGun", RpcTarget.All, selectedGun);
            }
        }
    }


    IEnumerator DelayedSwitchGun()
    {
        yield return new WaitForSeconds(1f);

        //���X�g�����[�v����
        foreach (GunStatus gun in guns)
        {
            //�e���\��
            gun.gameObject.SetActive(false);
        }

        //�I�𒆂̏e�̂ݕ\��
        guns[selectedGun].gameObject.SetActive(true);
    }

    /// <summary>
    /// �e�̕\���؂�ւ�
    /// </summary>
    void switchGun()
    {
        //���X�g�����[�v����
        foreach (GunStatus gun in guns)
        {
            //�e���\��
            gun.gameObject.SetActive(false);
        }

        //�I�𒆂̏e�̂ݕ\��
        guns[selectedGun].gameObject.SetActive(true);
    }


    /// <summary>
    /// �E�N���b�N�Ŕ`������
    /// </summary>
    public void Aim()
    {
        //�}�E�X�E�{�^�������Ă���Ƃ�
        if (Input.GetMouseButton(1))
        {
            //�Y�[���C��
            cameraController.GunZoomIn(guns[selectedGun].AdsZoom, guns[selectedGun].AdsSpeed);
        }
        else
        {
            //�Y�[���A�E�g
            cameraController.GunZoomOut(guns[selectedGun].AdsSpeed);
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
            if (ammoClip[selectedGun] == 0)
            {
                // �e�؂�̉���炷
                photonView.RPC("NotShotSound", RpcTarget.All);

                // �I�[�g�����[�h
                Reload();

                // �����I��
                return;
            }

            // �A�j���[�V����
            gunAnimator.SetTrigger("Attack");
            Debug.Log($"�e�̃A�j���[�V�����Ă΂ꂽ�ŁI");

            //�e�̔��ˏ���
            FiringBullet();
        }
    }

    // �e�؂ꉹ����
    [PunRPC]
    void NotShotSound()
    {
        //�@���ʉ��Đ�
        guns[selectedGun].GetNotShotSE().Stop();
        guns[selectedGun].GetNotShotSE().Play();
    }


    // ���C���̃G�t�F�N�g����
    [PunRPC]
    void ShotEffect()
    {
        //�@���ʉ��̍Đ�
        guns[selectedGun].GetShotSE().Stop();
        guns[selectedGun].GetShotSE().Play();

        //�@����\��
        guns[selectedGun].GetShotLight().enabled = false;
        guns[selectedGun].GetShotLight().enabled = true;

        // ���˃G�t�F�N�g��\��
        guns[selectedGun].ShotEffectNotActive();
        guns[selectedGun].ActiveShotEffect();

        //�@�R���[�`���ŏ������������s
        StartCoroutine(DisableLight());
        StartCoroutine(DisableEffect());

        //�@�e��Effect������
        IEnumerator DisableEffect()
        {
            yield return new WaitForSeconds(0.1f);
            guns[selectedGun].ShotEffectNotActive();
        }

        //�@���C�g����������
        IEnumerator DisableLight()
        {
            yield return new WaitForSeconds(0.1f);
            guns[selectedGun].GetShotLight().enabled = false;
        }
    }


    /// <summary>
    /// �e�ۂ̔���
    /// </summary>
    void FiringBullet()
    {
        // �A�j���[�V����
        testAnimatorController.Attack(AttackType.Short);

        // Effect���U�炷
        photonView.RPC("ShotEffect", RpcTarget.All);

        //Ray(����)���J�����̒�������ݒ�
        Vector2 pos = new Vector2(.5f, .5f);
        Ray ray = cameraController.GenerateRay(pos);

        // �J�����̉��o(�U�����ɏ����������)
        cameraController.ApplyRecoil();

        //���C�𔭎�
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //�v���C���[�ɂԂ������ꍇ
            if (hit.collider.gameObject.tag == "Player")
            {
                //���̃G�t�F�N�g���l�b�g���[�N��ɐ���
                PhotonNetwork.Instantiate(hitEffect.name, hit.point, Quaternion.identity);

                // �q�b�g�֐���S�v���C���[�ŌĂяo���Č����ꂽ�v���C���[��HP�𓯊�
                hit.collider.gameObject.GetPhotonView().RPC("Hit",
                    RpcTarget.All,
                    guns[selectedGun].ShotDamage,
                    photonView.Owner.NickName,
                    PhotonNetwork.LocalPlayer.ActorNumber);
            }
            else
            {
                //�e���G�t�F�N�g���� 
                GameObject bulletImpactObject = Instantiate(guns[selectedGun].GetHitEffect(),
                    hit.point + (hit.normal * .002f),                   //�I�u�W�F�N�g���班���������Ă�����h�~
                    Quaternion.LookRotation(hit.normal, Vector3.up));   //���p�̕�����Ԃ��Ă��̕����ɉ�]������

                //���Ԍo�߂ō폜
                Destroy(bulletImpactObject, 10f);
            }
        }

        //�ˌ��Ԋu��ݒ�
        shotTimer = Time.time + guns[selectedGun].ShootInterval;

        //�I�𒆂̏e�̒e�򌸂炷
        ammoClip[selectedGun]--;
    }


    /// <summary>
    /// �����[�h
    /// </summary>
    void Reload()
    {
        //�����[�h�ŕ�[����e�����擾
        int amountNeed = maxAmmoClip[selectedGun] - ammoClip[selectedGun];

        //�K�v�Ȓe��ʂƏ����e��ʂ��r
        int ammoAvailable = amountNeed < ammunition[selectedGun] ? amountNeed : ammunition[selectedGun];

        //�e�򂪖��^���̎��̓����[�h�ł��Ȃ�&�e����������Ă���Ƃ�
        if (amountNeed != 0 && ammunition[selectedGun] != 0)
        {
            //�����e�򂩂烊���[�h����e�򕪂�����
            ammunition[selectedGun] -= ammoAvailable;

            //�e�ɑ��U����
            ammoClip[selectedGun] += ammoAvailable;
        }
    }


    /// <summary>
    /// �e�̐؂�ւ�����
    /// </summary>
    [PunRPC]
    public void SetGun(int gunNo)
    {
        //�e�̐؂�ւ�
        if (gunNo < guns.Count)
        {
            //�e�̔ԍ����Z�b�g
            selectedGun = gunNo;

            // �A�j���[�V����
            testAnimatorController.TestWeaponChange();

            // 1�b�҂��Ă���؂�ւ���
            StartCoroutine(DelayedSwitchGun());
        }
    }

    /// <summary>
    /// �I�����Ă���e�̏����e��
    /// </summary>
    public int GetGunAmmunition()
    {
        return ammunition[selectedGun];
    }


    /// <summary>
    /// �I�����Ă���e�̃}�K�W�����e��
    /// </summary>
    public int GetGunAmmoClip()
    {
        return ammoClip[selectedGun];
    }


    /// <summary>
    /// �I�����Ă���e�̃}�K�W�����e��ő吔
    /// </summary>
    public int GetGunAmmoClipMax()
    {
        return maxAmmoClip[selectedGun];
    }
}