using UnityEngine.InputSystem;

internal class MultiTapAndHoldInteraction : IInputInteraction
{
    public float tapTime;       // �ő�̃^�b�v����[s]
    public float tapDelay;      // ���̃^�b�v�܂ł̍ő�ҋ@����[s]
    public int tapCount = 2;    // �K�v�ȃ^�b�v��
    public float pressPoint;    // ���͔����臒l(0�Ńf�t�H���g�l)
    public float releasePoint;  // �����[�X�����臒l(0�Ńf�t�H���g�l)
    public float endDelay;      // �}���`�^�b�v���z�[���h��A���͂��Ȃ��Ȃ��Ă���I������܂ł̎���

    /// <summary>
    /// �^�b�v��Ԃ̓����t�F�[�Y
    /// </summary>
    enum TapPhase
    {
        None,
        WaitingForNextRelease,
        WaitingForNextPress,
        WaitingForRelease,
        WaitingForEnd,
    }

    // �ݒ�l���f�t�H���g�l�̒l���i�[����t�B�[���h
    float tapTimeOrDefault => tapTime > 0.0 ? tapTime : InputSystem.settings.defaultTapTime;
    float tapDelayOrDefault => tapDelay > 0.0 ? tapDelay : InputSystem.settings.multiTapDelayTime;
    float pressPointOrDefault => pressPoint > 0 ? pressPoint : InputSystem.settings.defaultButtonPressPoint;
    float releasePointOrDefault => pressPointOrDefault * InputSystem.settings.buttonReleaseThreshold;

    // Interaction�̓������
    TapPhase _currentTapPhase = TapPhase.None;
    double _currentTapStartTime;
    double _lastTapReleaseTime;
    int _currentTapCount;

    /// <summary>
    /// ������
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
#else
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
    public static void Initialize()
    {
        // �����Interaction��o�^����K�v������
        InputSystem.RegisterInteraction<MultiTapAndHoldInteraction>();
    }

    /// <summary>
    /// Interaction�̓�������
    /// </summary>
    public void Process(ref InputInteractionContext context)
    {
        // �^�C���A�E�g����
        if (context.timerHasExpired)
        {
            // �ő勖�e���Ԃ𒴂��ă^�C���A�E�g�ɂȂ����ꍇ�̓L�����Z��
            context.Canceled();
            return;
        }

        switch (_currentTapPhase)
        {
            case TapPhase.None:
                // �������

                // �^�b�v���ꂽ���`�F�b�N
                if (context.ControlIsActuated(pressPointOrDefault))
                {
                    _currentTapStartTime = context.time;

                    if (++_currentTapCount >= tapCount)
                    {
                        // �K�v�ȃ^�b�v���ɒB������Performed�R�[���o�b�N���s
                        _currentTapPhase = TapPhase.WaitingForRelease;
                        context.Started();
                        context.PerformedAndStayPerformed();
                    }
                    else
                    {
                        // ���͂��Ȃ��Ȃ�܂őҋ@
                        _currentTapPhase = TapPhase.WaitingForNextRelease;
                        context.Started();
                        context.SetTimeout(tapTimeOrDefault);
                    }
                }

                break;

            case TapPhase.WaitingForNextRelease:
                // ���͂��Ȃ��Ȃ�܂őҋ@���Ă�����
                if (!context.ControlIsActuated(releasePointOrDefault))
                {
                    if (context.time - _currentTapStartTime > tapTimeOrDefault)
                    {
                        // �ő勖�e���Ԃ𒴂����̂ŃL�����Z��
                        context.Canceled();
                        break;
                    }

                    // ���̓��͑҂���ԂɑJ��
                    _lastTapReleaseTime = context.time;
                    _currentTapPhase = TapPhase.WaitingForNextPress;
                    context.SetTimeout(tapDelayOrDefault);
                }

                break;

            case TapPhase.WaitingForNextPress:
                // ���̓��͑҂��̏��
                if (context.ControlIsActuated(pressPointOrDefault))
                {
                    if (context.time - _lastTapReleaseTime > tapDelayOrDefault)
                    {
                        // �ő勖�e���Ԃ𒴂����̂ŃL�����Z��
                        context.Canceled();
                        break;
                    }

                    ++_currentTapCount;
                    _currentTapStartTime = context.time;

                    if (_currentTapCount >= tapCount)
                    {
                        // �K�v�ȃ^�b�v���ɒB�����̂ŁAPerformed�R�[���o�b�N�ʒm
                        // �I���܂őҋ@�����ԂɑJ��
                        _currentTapPhase = TapPhase.WaitingForRelease;
                        context.PerformedAndStayPerformed();
                    }
                    else
                    {
                        // �K�v�^�b�v���ɒB���Ă��Ȃ��̂ŁA���͂��Ȃ��Ȃ�܂őҋ@
                        _currentTapPhase = TapPhase.WaitingForNextRelease;
                        context.SetTimeout(tapTimeOrDefault);
                    }

                    _currentTapStartTime = context.time;
                }

                break;

            case TapPhase.WaitingForRelease:
                // �}���`�^�b�v�����A���͂��Ȃ��Ȃ�܂őҋ@���Ă�����

                // ���̓`�F�b�N
                if (!context.ControlIsActuated(releasePointOrDefault))
                {
                    // ���͂��Ȃ��Ȃ����̂ŏI��
                    _currentTapPhase = TapPhase.WaitingForEnd;
                    _lastTapReleaseTime = context.time;
                    context.SetTimeout(endDelay);
                }

                break;

            case TapPhase.WaitingForEnd:
                // ���͂��Ȃ��Ȃ��Ă���Interaction���I������܂őҋ@���Ă�����
                if (context.time - _lastTapReleaseTime >= endDelay)
                {
                    // ��莞�Ԍo�߂����̂ŏI������
                    context.Canceled();
                }
                else if (context.ControlIsActuated(pressPointOrDefault))
                {
                    // �Ăѓ��͂�������
                    // ��莞�Ԍo�߂��Ă��Ȃ��̂ŁA�p���Ƃ݂Ȃ�
                    _currentTapPhase = TapPhase.WaitingForRelease;
                    context.PerformedAndStayPerformed();
                }

                break;
        }
    }

    /// <summary>
    /// Interaction�̏�ԃ��Z�b�g
    /// </summary>
    public void Reset()
    {
        _currentTapPhase = TapPhase.None;
        _currentTapStartTime = 0;
        _lastTapReleaseTime = 0;
        _currentTapCount = 0;
    }
}