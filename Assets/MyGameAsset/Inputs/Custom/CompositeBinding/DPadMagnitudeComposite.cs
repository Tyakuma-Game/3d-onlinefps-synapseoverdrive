using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

internal class DPadMagnitudeComposite : InputBindingComposite<float>
{
    // 4�����{�^������
    [InputControl(layout = "Button")] public int up = 0;
    [InputControl(layout = "Button")] public int down = 0;
    [InputControl(layout = "Button")] public int left = 0;
    [InputControl(layout = "Button")] public int right = 0;

    /// <summary>
    /// ������
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
#else
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
    private static void Initialize()
    {
        // �����CompositeBinding��o�^����K�v������
        InputSystem.RegisterBindingComposite(typeof(DPadMagnitudeComposite), "2DVectorMagnitude");
    }

    /// <summary>
    /// 4�������͂���x�N�g���̑傫���ɕϊ����ĕԂ�
    /// </summary>
    public override float ReadValue(ref InputBindingCompositeContext context)
    {
        var upValue = context.ReadValue<float>(up);
        var downValue = context.ReadValue<float>(down);
        var leftValue = context.ReadValue<float>(left);
        var rightValue = context.ReadValue<float>(right);

        return DpadControl.MakeDpadVector(upValue, downValue, leftValue, rightValue).magnitude;
    }

    /// <summary>
    /// �l�̑傫����Ԃ�
    /// </summary>
    public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
    {
        return ReadValue(ref context);
    }
}