using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OffLineRoomManager : MonoBehaviour
{

    [SerializeField] string OnLineConnectTitlescene;
    [SerializeField] string ToPracticeGroundScene;


    /// <summary>
    /// ���K���ǂݍ���
    /// </summary>
    public void TransitionToPracticeGround()
    {
        // �V�[����񓯊��œǂݍ���
        SceneManager.LoadSceneAsync(ToPracticeGroundScene);
    }


    /// <summary>
    /// Internet�ڑ��̃^�C�g����ǂݍ��ފ֐�
    /// </summary>
    public void TransitionToOnlineMode()
    {
        // �V�[����񓯊��œǂݍ���
        SceneManager.LoadSceneAsync(OnLineConnectTitlescene);
    }


    /// <summary>
    /// �Q�[�����I������֐�
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        // �G�f�B�^�[���[�h�Ŏ��s���Ă���ꍇ�A�G�f�B�^�[���~
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // �G�f�B�^�[���[�h�ȊO�ł̓A�v���P�[�V�������I��
        Application.Quit();
#endif
    }
}