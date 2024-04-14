using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LobbyPanelController : MonoBehaviour
{
    void Start()
    {
        TitleUIEvent.OnCloseAllMenusRequested += CloseMenu;
    }

    void OnDestroy()
    {
        TitleUIEvent.OnCloseAllMenusRequested -= CloseMenu;
    }

    /// <summary>
    /// ���g�̔�\������
    /// </summary>
    void CloseMenu() =>
        gameObject.SetActive(false);

    public void OpenMenu() =>
        gameObject.SetActive(true);
}