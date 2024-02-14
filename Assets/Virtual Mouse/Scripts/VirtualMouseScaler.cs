using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif

/// <summary>
/// ���z�}�E�X�̍��W�C�����s���N���X
/// </summary>
public class VirtualMouseScaler : InputProcessor<Vector2>
{
    public float scale = 1;

    const string ProcessorName = nameof(VirtualMouseScaler);
    const string VirtualMouseDeviceName = "VirtualMouse";

#if UNITY_EDITOR
    static VirtualMouseScaler() => Initialize();
#endif

    /// <summary>
    /// Processor�̓o�^
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        // �d���o�^�̏ꍇ�AInput Action��Processor�ꗗ�ɐ������\������Ȃ��������邽�߁A
        // �d���`�F�b�N���s���Ă���
        if (InputSystem.TryGetProcessor(ProcessorName) == null)
            InputSystem.RegisterProcessor<VirtualMouseScaler>(ProcessorName);
    }

    /// <summary>
    /// �Ǝ���Processor�̏���
    /// </summary>
    /// <param name="value">���͒l</param>
    /// <param name="control">���̓R���g���[��</param>
    /// <returns>�������ꂽ���͒l</returns>
    public override Vector2 Process(Vector2 value, InputControl control)
    {
        // VirtualMouse�̏ꍇ�A���W�n��肪�������邽��Processor�K�p
        if (control.device.name == VirtualMouseDeviceName)
            value *= scale;

        return value;
    }
}