using UnityEngine;

/// <summary>
/// �e�̃p�����[�^�N���X
/// </summary>
[CreateAssetMenu(fileName = "NewGunData", menuName = "MyFPSGameDate/Gun Data", order = 0)]
public class GunData : ScriptableObject
{
    [Header(" �e��֘A ")]
    [SerializeField] int maxAmmunition; // �����e��ő吔
    [SerializeField] int maxAmmoClip;   // �P�}�K�W��������̍ő�e��

    [Header(" �ˌ��֘A ")]
    [SerializeField] int shotDamage = 10;           // �ꔭ������̃_���[�W��
    [SerializeField] float shootInterval = 0.1f;    // �e�̔��ˊԊu
    [SerializeField] float adsZoom = 2.0f;          // �Y�[���{��
    [SerializeField] float adsSpeed = 0.5f;         // �Y�[�����x

    [Header(" �G�t�F�N�g�֘A ")]
    [SerializeField] GameObject playerHitEffect;    // �v���C���[�ɓ������ۂɐ�������I�u�W�F�N�g
    [SerializeField] GameObject nonPlayerHitEffect; // �v���C���[�ł͂Ȃ��I�u�W�F�N�g�ɔ�e���ɐ�������I�u�W�F�N�g

    /// <summary>
    /// �����e�򐔂̍ő吔
    /// </summary>
    public int MaxAmmunition => maxAmmunition;

    /// <summary>
    /// �}�K�W��1������̍ő�e��
    /// </summary>
    public int MaxAmmoClip => maxAmmoClip;

    /// <summary>
    /// 1���̃_���[�W��
    /// </summary>
    public int ShotDamage => shotDamage;

    /// <summary>
    /// �ˌ��Ԋu�i�b�j
    /// </summary>
    public float ShootInterval => shootInterval;

    /// <summary>
    /// �Ə����̃Y�[���{��
    /// </summary>
    public float AdsZoom => adsZoom;

    /// <summary>
    /// �Ə��̓��쑬�x
    /// </summary>
    public float AdsSpeed => adsSpeed;

    /// <summary>
    /// �v���C���[�ɒe���������ۂɐ�������G�t�F�N�g
    /// </summary>
    public GameObject PlayerHitEffect => playerHitEffect;

    /// <summary>
    /// �v���C���[�ł͂Ȃ��I�u�W�F�N�g�ɒe���������ۂɐ�������G�t�F�N�g
    /// </summary>
    public GameObject NonPlayerHitEffect => nonPlayerHitEffect;
}