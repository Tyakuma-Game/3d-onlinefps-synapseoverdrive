/// <summary>
/// Player�̃A�j���[�V�����C���^�[�t�F�[�X
/// </summary>
public interface IPlayerAnimator
{
    /// <summary>
    /// �A�j���[�V�����̍X�V����
    /// </summary>
    /// <param name="playerAnimationState">���݂̃A�j���[�V�������</param>
    void AnimationUpdate(PlayerAnimationState playerAnimationState);
}