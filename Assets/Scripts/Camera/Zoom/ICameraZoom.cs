using UnityEngine;

/// <summary>
/// �J�����̃Y�[���Ɋւ���C���^�[�t�F�[�X
/// </summary>
public interface ICameraZoom
{
    /// <summary>
    /// �J�n�n�_���珙�X�ɃY�[������
    /// </summary>
    /// <param name="camera">�Ώۂ̃J����</param>
    /// <param name="adsZoom">�Y�[���{��</param>
    /// <param name="adsSpeed">�Y�[�����x</param>
    public void GunZoomIn(Camera camera, float adsZoom, float adsSpeed);

    /// <summary>
    /// ���̒n�_�ɏ��X�ɖ߂�
    /// </summary>
    /// <param name="camera">�Ώۂ̃J����</param>
    /// <param name="adsZoom">�Y�[���O�J�����{��</param>
    /// <param name="adsSpeed">�Y�[�����x</param>
    public void GunZoomOut(Camera camera, float CameraBaseFactor, float adsSpeed);
}