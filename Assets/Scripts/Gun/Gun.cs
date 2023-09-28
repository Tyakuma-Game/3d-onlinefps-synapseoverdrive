using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("�萔")]
    [Tooltip("�ˌ��Ԋu")]
    public float shootInterval = .1f;
    
    [Tooltip("�З�")]
    public int shotDamage;
    
    [Tooltip("�`�����ݎ��̃Y�[��")]
    public float adsZoom;
    
    [Tooltip("�`�����ݎ��̑��x")]
    public float adsSpeed;


    [Header("�Q��")]
    [Tooltip("�e���̃G�t�F�N�g")]
    public GameObject bulletImpact;


    [Header("�T�E���h�֘A")]

    [Tooltip("�e�̔��ˉ�")]
    public AudioSource shoutSound;
    
    [Tooltip("��䰗�����")]
    public AudioSource cartridgeCaseSound;
    
    [Tooltip("�e��؂�̉�")]
    public AudioSource outOfAmmoSound;

    [Tooltip("�����[�h�̉�")]
    public AudioSource reloadingSound;//������


    /// <summary>
    /// �e�����P���炷
    /// </summary>
    public void SoundGunShot()
    {
        shoutSound.Play();                      //��x�炷
        Invoke("SoundGunCartridgeCase", 0.3F);  //��䰂̗���������莞�Ԍ�ɖ炷
    }


    /// <summary>
    /// �T�u�}�V���K���̃��[�vON
    /// </summary>
    public void LoopON_SubmachineGun()
    {
        //�����Ȃ��Ă��Ȃ��Ȃ�
        if (!shoutSound.isPlaying)
        {
            shoutSound.loop = true;                 //���[�vON
            shoutSound.Play();                      //����炵�n�߂�
            Invoke("SoundGunCartridgeCase", 0.3F);  //��䰂̗���������莞�Ԍ�ɖ炷
        }
    }


    /// <summary>
    /// �T�u�}�V���K���̃��[�vOFF
    /// </summary>
    public void LoopOFF_SubmachineGun()
    {
        shoutSound.loop = false;    //���[�vOFF
        shoutSound.Stop();          //�����~�߂�
    }


    /// <summary>
    /// �e�؂�̉���炷
    /// </summary>
    public void SoundGunOutOfBullets()
    {
        //�������ĂȂ��Ȃ�
        if (!outOfAmmoSound.isPlaying)
        {
            outOfAmmoSound.Play();  //��x�炷
        }
    }


    /// <summary>
    /// �s�X�g���̃����[�h��
    /// </summary>
    public void SoundPistolReloading()
    {
        reloadingSound.Play();//��x�炷
    }


    /// <summary>
    /// �V���b�g�K���̃����[�h��
    /// </summary>
    public void SoundShotgunReloading()
    {
        reloadingSound.Play();//��x�炷
    }


    /// <summary>
    /// �A�T���g�̃����[�h��
    /// </summary>
    public void SoundAssaultReloading()
    {
        reloadingSound.Play();//��x�炷
    }


    /// <summary>
    /// ��䰂̗�������炷
    /// </summary>
    public void SoundGunCartridgeCase()
    {
        //�������ĂȂ��Ȃ�
        if (!cartridgeCaseSound.isPlaying)
        {
            cartridgeCaseSound.Play();  //��x�炷
        }
    }
}