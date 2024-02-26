using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class RotationSpeedController : MonoBehaviour
{
    [SerializeField] RotationSettings rotationSettings; // �X�N���v�^�u���I�u�W�F�N�g�̎Q��
    [SerializeField] Slider rotationSpeedSlider; // UI�X���C�_�[�̎Q��
    [SerializeField] Text Text;
    void Start()
    {
        rotationSpeedSlider.minValue = 0.1f; // �X���C�_�[�̍ŏ��l��ݒ�
        rotationSpeedSlider.maxValue = 5f; // �X���C�_�[�̍ő�l��ݒ�
        rotationSpeedSlider.value = Mathf.Clamp(rotationSettings.rotationSpeed, 0.1f, 5f); // �����l��ݒ肵�A�͈͓��Ɏ��߂�
        rotationSpeedSlider.onValueChanged.AddListener(UpdateRotationSpeed); // �X���C�_�[�̒l���ύX���ꂽ���̃��X�i�[��ǉ�
        Text.text = rotationSpeedSlider.value.ToString() + 'f';
    }

    void UpdateRotationSpeed(float value)
    {
        rotationSettings.rotationSpeed = Mathf.Clamp(value, 0.1f, 5f); // �X���C�_�[�̒l��rotationSpeed�ɔ��f���A�͈͓��Ɏ��߂�
        Text.text = value.ToString() + 'f';
    }
}