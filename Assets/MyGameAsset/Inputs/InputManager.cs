using UnityEngine;

/// <summary>
/// �Q�[�����̓��͏����Ǘ��N���X
/// �Q�[���̓��̓V�X�e�������������A�L�����E����������Ӗ�������
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary>
    /// �Q�[���̓��͐ݒ���i�[����ÓI�ϐ�
    /// ���̕ϐ���ʂ��āA�Q�[���S�̂ň�т������͏�����񋟂���
    /// </summary>
    public static GameInputs Controls;

    void Awake()
    {
        Controls = new GameInputs();    // GameInputs�C���X�^���X����
        Controls.Enable();              // ���̓V�X�e����L����
    }

    void OnDestroy()
    {
        Controls?.Disable();// ���̓V�X�e�������݂���ꍇ�ɖ�����
    }
}