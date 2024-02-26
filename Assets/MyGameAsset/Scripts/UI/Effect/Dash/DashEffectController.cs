using UnityEngine;

public class DashEffectController : MonoBehaviour
{
    [SerializeField] GameObject dashEffect;

    void Start()
    {
        PlayerMove.OnStateChanged += SetDashEffectActive;
    }

    void OnDestroy()
    {
        PlayerMove.OnStateChanged -= SetDashEffectActive;
    }

    /// <summary>
    /// �_�b�V���G�t�F�N�g�̕\���ؑ�
    /// </summary>
    /// <param name="isActive">�\�����</param>
    void SetDashEffectActive(bool isActive) =>
        dashEffect.SetActive(isActive);
}