using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.InputSystem;

namespace Guns
{
    /// <summary>
    /// �e�̎�ނ�\���񋓑�
    /// </summary>
    enum GunType
    {
        HandGun,        // �n���h�K��
        AssaultRifle,   // �A�T���g���C�t��
        SniperRifle     // �X�i�C�p�[���t��
    }

    /// <summary>
    /// �v���C���[�̏e�Ǘ��N���X
    /// </summary>
    public class PlayerGunController : MonoBehaviourPunCallbacks
    {
        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        //�@���P����
        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/


        [SerializeField] Animator gunAnimator;                  // �A�N�V�����ɓ������銴���Ń��t�@�N�^�����O����I
        [SerializeField] CameraController cameraController;     // �A�N�V�����ɓ������銴���Ń��t�@�N�^�����O����!
        [SerializeField] PlayerAnimator playerAnimator;         // �A�N�V�����ɓ������銴���Ń��t�@�N�^�����O����I

        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

        [Header(" �f�[�^�֘A ")]
        [SerializeField] GunData[] gunDates;                // �e�f�[�^�ꗗ
        List<GameObject> guns = new List<GameObject>();     // �eData�Ǘ��p
        List<int> ammunition = new List<int>();             // ���݂̏����e��
        List<int> ammoClip = new List<int>();               // �}�K�W�����̒e��
        GunType selectedGunType = GunType.HandGun;          // ���ݑI�𒆂̏e���
        float shotTimer;                                    // �ˌ��Ԋu

        [Header(" �����ڊ֘A ")]
        [SerializeField] GameObject[] gunsHolder;       // �������_�̏e
        [SerializeField] GameObject[] otherGunsHolder;  // ���莋�_�̏e


        void Start()
        {
            // �g�p����e�z���_�[�I���i�������_�����莋�_������Ɍ���j
            GameObject[] selectedGunsHolder = photonView.IsMine ? gunsHolder : otherGunsHolder;

            // �e�o�^
            foreach (GameObject gun in selectedGunsHolder)
                guns.Add(gun);

            // ���g�̏ꍇ�̂݁A�e��ƃ}�K�W���̏�����
            if (photonView.IsMine)
            {
                foreach (var gun in gunDates)
                {
                    ammunition.Add(gun.MaxAmmunition);  // �����e��
                    ammoClip.Add(gun.MaxAmmoClip);      // �}�K�W�����e��
                }

                // �Y�[���֘A�����o�^
                InputManager.Controls.Gun.Zoom.started += ZoomIn;
                InputManager.Controls.Gun.Zoom.canceled += ZoomOut;
            }

            // �e�̕\���ؑ�
            ChangeActiveGun();
        }
        void OnDestroy()
        {
            // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
            if (!photonView.IsMine)
                return;

            // �Y�[���֘A��������
            InputManager.Controls.Gun.Zoom.started -= ZoomIn;
            InputManager.Controls.Gun.Zoom.canceled -= ZoomOut;
        }

        void Update()
        {
            // �����ȊO�Ȃ珈���I��
            if (!photonView.IsMine)
                return;

            // �e�̐؂�ւ�
            SwitchingGuns();

            // �ˌ��֐�
            Fire();

            // �����[�h�֐�
            if (Input.GetKeyDown(KeyCode.R))
                Reload();

            // �e��e�L�X�g�X�V
            UIManager.instance.SettingBulletsText(gunDates[(int)selectedGunType].MaxAmmoClip,
                ammoClip[(int)selectedGunType], ammunition[(int)selectedGunType]);
        }

        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        //�@����؂�ւ�
        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

        /// <summary>
        /// �e�̐؂�ւ��L�[���͂����m
        /// </summary>
        public void SwitchingGuns()
        {
            int gunCount = Enum.GetValues(typeof(GunType)).Length;

            // �}�E�X�z�C�[���ł̏e�̐؂�ւ�
            float mouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");
            if (mouseScroll != 0f)
                UpdateSelectedGunType((int)Mathf.Sign(mouseScroll), gunCount);

            // ���l�L�[�ł̏e�̐؂�ւ�
            for (int i = 0; i < guns.Count; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    SetGunTypeAndNotify((GunType)i);
                    break; // �L�[�������ꂽ�烋�[�v�𔲂���
                }
            }
        }

        /// <summary>
        /// �e�̃^�C�v���X�V���A�ύX��ʒm
        /// </summary>
        void UpdateSelectedGunType(int direction, int gunCount)
        {
            // �e�̃^�C�v���X�V
            selectedGunType += direction;

            // �͈͊O�ɂȂ�Ȃ��悤�ɒ���
            if (selectedGunType < 0)
            {
                selectedGunType = (GunType)(gunCount - 1);
            }
            else if ((int)selectedGunType >= gunCount)
            {
                selectedGunType = GunType.HandGun;
            }

            // �X�V��̏e�̃^�C�v��ݒ肵�A�ʒm
            SetGunTypeAndNotify(selectedGunType);
        }

        /// <summary>
        /// �e�̃^�C�v��ݒ肵�A�ύX��ʒm����
        /// </summary>
        void SetGunTypeAndNotify(GunType gunType)
        {
            selectedGunType = gunType;
            gunAnimator.SetTrigger("WeaponChange");
            photonView.RPC("SetGun", RpcTarget.All, (int)selectedGunType);
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

                // �w�莞�Ԍ�؂�ւ�
                StartCoroutine(DelayedSwitchGun(1f));
            }
        }

        /// <summary>
        /// Photon�ŌĂяo������ύX����
        /// </summary>
        /// <param name="waitTime">�҂�����</param>
        IEnumerator DelayedSwitchGun(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            ChangeActiveGun();
        }

        /// <summary>
        /// �e�̕\���؂�ւ�
        /// </summary>
        void ChangeActiveGun()
        {
            // �S�Ă̏e���\����
            foreach (GameObject gun in guns)
                gun.gameObject.SetActive(false);

            // �I�𒆂̏e�̂ݕ\������
            guns[(int)selectedGunType].SetActive(true);
        }

        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        //�@�Y�[������
        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

        /// <summary>
        /// �e�̃Y�[����ԕύX���̃C�x���g�n���h��
        /// </summary>
        public static Action<bool> OnGunZoomStateChanged;

        /// <summary>
        /// �Y�[���J�n
        /// </summary>
        void ZoomIn(InputAction.CallbackContext context)
        {
            OnGunZoomStateChanged?.Invoke(true);
            CameraZoom.OnZoomStateChanged?.Invoke(gunDates[(int)selectedGunType].AdsZoom, gunDates[(int)selectedGunType].AdsSpeed);
        }

        /// <summary>
        /// �Y�[���I��
        /// </summary>
        void ZoomOut(InputAction.CallbackContext context)
        {
            OnGunZoomStateChanged?.Invoke(false);
            CameraZoom.OnZoomStateChanged?.Invoke(60f, gunDates[(int)selectedGunType].AdsSpeed);
        }


        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
        //�@���ˏ����ƃ����[�h
        //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

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
            int gunTypeIndex = (int)selectedGunType;

            // �����[�h��[���̒e���v�Z
            int ammoToReload = Math.Min(gunDates[gunTypeIndex].MaxAmmoClip - ammoClip[gunTypeIndex], ammunition[gunTypeIndex]);

            // �e�򂪂���ꍇ�̂݃����[�h
            if (ammoToReload > 0)
            {
                //�@TODO: ���Ԃ�����ۂɂ����𒲐�����
                // �A�j���[�V����
                // gunAnimator.SetTrigger("Reload");

                // �����e����X�V�ƒe�򑕓U
                ammunition[gunTypeIndex] -= ammoToReload;
                ammoClip[gunTypeIndex] += ammoToReload;
            }
        }
    }
}