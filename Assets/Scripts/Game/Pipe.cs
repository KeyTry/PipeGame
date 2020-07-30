using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_sprite;

    private PipeCell m_cell;

    private PipeTypes m_type;
    private PipeColors m_color;

    public PipeTypes Type { get => m_type; set => m_type = value; }
    public PipeColors Color { get => m_color; set => m_color = value; }
    public PipeCell Cell { get => m_cell; set => m_cell = value; }

    public void SetPipe()
    {
        switch (m_color)
        {
            case PipeColors.Blue:
                m_sprite.color = UnityEngine.Color.blue;
                break;

            case PipeColors.Green:
                m_sprite.color = UnityEngine.Color.green;
                break;

            case PipeColors.Red:
                m_sprite.color = UnityEngine.Color.red;
                break;
        }

        switch (m_type)
        {
            case PipeTypes.Horizontal:
                m_sprite.sprite = PipeGame.Instance.HorizontalPipe;
                break;

            case PipeTypes.Vertical:
                m_sprite.sprite = PipeGame.Instance.VerticalPipe;
                break;

            case PipeTypes.LeftDown:
                m_sprite.sprite = PipeGame.Instance.LeftDownPipe;
                break;

            case PipeTypes.LeftUp:
                m_sprite.sprite = PipeGame.Instance.LeftUpPipe;
                break;

            case PipeTypes.RightDown:
                m_sprite.sprite = PipeGame.Instance.RightDownPipe;
                break;

            case PipeTypes.RightUp:
                m_sprite.sprite = PipeGame.Instance.RightUpPipe;
                break;
        }
    }

    public void SetOnTop()
    {
        m_sprite.sortingOrder = 1;
    }

    public void SetBelow()
    {
        m_sprite.sortingOrder = 0;
    }

    PipeCell m_collidingPipeCell = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PipeCell cell = collision.GetComponent<PipeCell>();

        if(cell != null)
        {
            m_collidingPipeCell = cell;
        }
    }

    public void TradeCell()
    {
        if (m_collidingPipeCell != null && m_collidingPipeCell != m_cell)
        {
            Pipe pipe = m_collidingPipeCell.Pipe;

            pipe.SetNewCell(m_cell);

            SetNewCell(m_collidingPipeCell);
        }
        else
        {
            transform.localPosition = Vector2.zero;
        }
    }

    public void SetNewCell(PipeCell pCell)
    {
        pCell.Pipe = this;
        m_cell = pCell;

        transform.SetParent(m_cell.transform);

        transform.localPosition = Vector2.zero;
    }
}
