using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_winScreen;
    [SerializeField]
    private GameObject m_loseScreen;

    public void CloseScreens()
    {
        m_winScreen.SetActive(false);
        m_loseScreen.SetActive(false);
    }

    public void ShowEndScreen(bool pWin)
    {
        if(pWin)
        {
            m_winScreen.SetActive(true);
        }
        else
        {
            m_loseScreen.SetActive(true);
        }
    }
}
