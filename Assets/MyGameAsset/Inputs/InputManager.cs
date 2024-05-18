using UnityEngine;

/// <summary>
/// �Q�[�����̓��͏����Ǘ��N���X
/// ���̓V�X�e�������������L�����E����������Ӗ�������
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary>
    /// �Q�[���̓��͐ݒ���i�[����ÓI�ϐ�
    /// ���̕ϐ���ʂ��āA�Q�[���S�̂ň�т������͏�����񋟂���
    /// </summary>
    public static GameInputs Controls { get; private set; }

    void Awake()
    {
        // ����
        Controls = new GameInputs();

        if (Controls != null)
        {
            Controls.Enable(); // ���̓V�X�e���L����
            Debug.Log("Input system enabled.");
        }
        else
        {
            Debug.LogError("Failed to create input system.");
        }
    }

    void OnDestroy()
    {
        if (Controls != null)
        {
            Controls.Disable(); // ���̓V�X�e��������
            Debug.Log("Input system disabled.");
        }
    }
}