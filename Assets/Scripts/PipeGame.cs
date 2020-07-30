using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PipeTypes
{
    Horizontal,
    Vertical,
    LeftUp,
    LeftDown,
    RightUp,
    RightDown
}

public enum PipeColors
{
    Red,
    Green,
    Blue
}

public class PipeGame : MonoBehaviour
{
    private static PipeGame s_instance;

    public static PipeGame Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType<PipeGame>();

                if (s_instance == null)
                {
                    GameObject container = new GameObject("PipeGame");
                    s_instance = container.AddComponent<PipeGame>();
                }
            }

            return s_instance;
        }
    }

    [SerializeField]
    private UIManager m_uiManager;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (s_instance != null && s_instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        s_instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowEnd(bool pWin)
    {
        m_uiManager.ShowEndScreen(pWin);
    }

    [SerializeField]
    private Sprite m_horizontalPipe;
    [SerializeField]
    private Sprite m_verticalPipe;
    [SerializeField]
    private Sprite m_leftDownPipe;
    [SerializeField]
    private Sprite m_leftUpPipe;
    [SerializeField]
    private Sprite m_rightDownPipe;
    [SerializeField]
    private Sprite m_rightUpPipe;

    public Sprite HorizontalPipe { get => m_horizontalPipe; }
    public Sprite VerticalPipe { get => m_verticalPipe; }
    public Sprite LeftDownPipe { get => m_leftDownPipe; }
    public Sprite LeftUpPipe { get => m_leftUpPipe; }
    public Sprite RightDownPipe { get => m_rightDownPipe; }
    public Sprite RightUpPipe { get => m_rightUpPipe; }
}
