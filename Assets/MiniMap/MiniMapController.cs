using System.Collections.Generic;
using UnityEngine;

namespace MiniMap
{
    // TODO
    // �����I�ɂ��̑��v���C���[�̃g�����X�t�H�[�����Ǘ����A�G�̃A�C�R���Ǝ��E��������悤�ɉ��ǂ���
    // ��肠�����A�g�����X�t�H�[���̓��X�g�^�ŊǗ����A�����ȊO�̍��W�Ȃǂ𑗐M���銴���ɂ��邩
    // �������̂���Player���ԂɃ~�j�}�b�v�Ǘ��̃v���O������g�ݍ��ށH�@

    /// <summary>
    /// �~�j�}�b�v�̊Ǘ����s���N���X
    /// </summary>
    public class MiniMapController : MonoBehaviour
    {
        public static MiniMapController instance { get; private set; }

        [Header("Settings")]
        [SerializeField] float yPositionConstant = 0.0f;
        [SerializeField] float cameraIconDistance = 0.0f;
        [SerializeField] float iconRotation = 90f;

        [Header("Elements")]
        [SerializeField] Transform cameraTransform;
        [SerializeField] Transform iconTransform;

        Transform targetTransform;
        Vector3 miniMapPos;
        Vector3 targetRotation;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        // TODO:
        // OnMove�Ȃǂ̈ړ��C�x���g�����������ۂɂ��̊֐���o�^���銴���Őݒ肷��悤�ɂ���
        // ���݂̓��t�@�N�^�����O���̈�Update�ōX�V���Ă��邪�����I�ɂ͍X�V�C�x���g�ŌĂяo���悤�ɂ��A����������I
        void Update()
        {
            if (targetTransform == null)
                Debug.LogWarning("�~�j�}�b�v��Target��NULL�I");
            else
                UpdateMiniMapPosition();
        }

        /// <summary>
        /// �~�j�}�b�v��̈ʒu�ƃJ�����ʒu���X�V
        /// </summary>
        void UpdateMiniMapPosition()
        {
            // �~�j�}�b�v��ł̈ʒu���v�Z
            miniMapPos = new Vector3(targetTransform.position.x, yPositionConstant, targetTransform.position.z);

            // �J�����ʒu�X�V
            cameraTransform.position = miniMapPos;

            // �J�����ƃA�C�R���̋������l�����ăA�C�R���̍��W�X�V
            miniMapPos.y -= cameraIconDistance;
            iconTransform.position = miniMapPos;

            // �A�C�R���̉�]���X�V
            targetRotation = targetTransform.eulerAngles;
            iconTransform.eulerAngles = new Vector3(iconRotation, targetRotation.y, targetRotation.z);
        }

        // TODO:
        // �v���C���[�̃X�|�[���C�x���g������Ăяo����悤�����I�ɂ���

        /// <summary>
        /// �Ǐ]������I�u�W�F�N�g�̃g�����X�t�H�[�����Z�b�g
        /// </summary>
        /// <param name="target">�Ǐ]������I�u�W�F�N�g�̃g�����X�t�H�[��</param>
        public void SetMiniMapTarget(Transform target)
        {
            targetTransform = target;
        }
    }
}