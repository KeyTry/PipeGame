using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_pipesMask;

    private Pipe m_selectedPipe = null;
    
    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;

        if(mouse.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouse.position.ReadValue()), Vector2.zero, Mathf.Infinity, m_pipesMask);

            if(hit.collider != null)
            {
                Pipe pipe = hit.collider.GetComponent<Pipe>();

                if(pipe != null)
                {
                    m_selectedPipe = pipe;
                    m_selectedPipe.SetOnTop();
                }
            }
        }

        if(mouse.leftButton.isPressed)
        {
            if(m_selectedPipe != null)
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());

                m_selectedPipe.transform.position = position;
            }
        }

        if(mouse.leftButton.wasReleasedThisFrame)
        {
            if(m_selectedPipe != null)
            {
                m_selectedPipe.SetBelow();
                m_selectedPipe.TradeCell();
                m_selectedPipe = null;
            }
        }
    }
}
