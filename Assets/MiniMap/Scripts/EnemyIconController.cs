using UnityEngine;

namespace MiniMap
{
    /// <summary>
    /// �~�j�}�b�v��ł̓G�A�C�R�����Ǘ�����N���X
    /// </summary>
    public class EnemyIconController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float iconElevation = 0.0f;
        [SerializeField] GameObject icon;
        [SerializeField] Transform enemyTransform;
        bool isIconVisible = true;

        // TODO: ����������
        // Update�Ŗ��ʂɊ֐��𖈉�Ăяo���Ă��邩��
        // ���Move�A�N�V�����̊֐��ɓo�^����`�ō��W�X�V�����ɍ��킹�ČĂяo���悤�ɍ�蒼���B
        void Update()
        {
            if (isIconVisible)
                UpdateIconPosition();
        }

        /// <summary>
        /// �~�j�}�b�v��ł̃A�C�R���̈ʒu�X�V
        /// </summary>
        void UpdateIconPosition()
        {
            Vector3 iconPosition = new Vector3(enemyTransform.position.x, iconElevation, enemyTransform.position.z);
            icon.transform.position = iconPosition;
        }

        /// <summary>
        /// �A�C�R���̕\���؂�ւ�
        /// </summary>
        /// <param name="isVisible">�\�� = true / ��\�� = false</param>
        public void SetIconVisibility(bool isVisible)
        {
            if (isIconVisible != isVisible)
            {
                isIconVisible = isVisible;
                icon.SetActive(isVisible);

                // �A�C�R����\������ۂɈʒu���X�V
                if (isVisible)
                    UpdateIconPosition();
            }
        }
    }
}