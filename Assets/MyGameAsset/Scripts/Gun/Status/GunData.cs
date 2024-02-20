using UnityEngine;

/// <summary>
/// �e�̃p�����[�^�N���X
/// </summary>
[CreateAssetMenu(fileName = "NewGunData", menuName = "MyFPSGameDate/Gun Data", order = 0)]
public class GunData : ScriptableObject
{
    [Header(" �e��֘A ")]
    [SerializeField] int maxAmmunition;
    [SerializeField] int maxAmmoClip;
    int ammunition;
    int ammoClip;

    [Header(" �ˌ��֘A ")]
    [SerializeField] int shotDamage         = 10;
    [SerializeField] float shootInterval    = 0.1f;
    [SerializeField] float adsZoom          = 2.0f;
    [SerializeField] float adsSpeed         = 0.5f;

    /// <summary>
    /// �����e�򐔂̍ő吔
    /// </summary>
    public int MaxAmmunition => maxAmmunition;

    /// <summary>
    /// �}�K�W��1������̍ő�e��
    /// </summary>
    public int MaxAmmoClip => maxAmmoClip;

    /// <summary>
    /// ���݂̏����e��
    /// </summary>
    public int Ammunition
    {
        get { return ammunition; }
        set { ammunition = Mathf.Clamp(value, 0, maxAmmunition); }
    }

    /// <summary>
    /// ���݂̃}�K�W�����e��
    /// </summary>
    public int AmmoClip
    {
        get { return ammoClip; }
        set { ammoClip = Mathf.Clamp(value, 0, maxAmmoClip); }
    }

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
}