
namespace OnDamageEvent
{
    /// <summary>
    /// �_���[�W�C�x���g�Ŏg�p����f�[�^�N���X
    /// </summary>
    public class DamageEventData
    {
        public int DamageAmount { get; private set; }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="damageAmount">�_���[�W��</param>
        public DamageEventData(int damageAmount)
        {
            DamageAmount = damageAmount;
        }
    }
}