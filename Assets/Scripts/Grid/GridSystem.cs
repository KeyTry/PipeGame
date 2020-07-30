using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    // Grid Size 5 * 9;

    [SerializeField]
    private PipeCell[,] m_cellGrid = new PipeCell[Constants.gridWidth, Constants.gridLength];

    [SerializeField]
    private Grid m_grid;

    [SerializeField]
    private GameObject m_pipePrefab;

    [SerializeField]
    private GameObject m_pipeCellPrefab;

    [SerializeField]
    private PipeDefinition[] m_defaultLayout = new PipeDefinition[Constants.gridWidth * Constants.gridLength];

    public PipeCell[,] CellGrid { get => m_cellGrid; set => m_cellGrid = value; }

    // Start is called before the first frame update
    void Start()
    {
        int width = Constants.gridWidth;
        int length = Constants.gridLength;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < length; y++)
            {
                GameObject pipeCell = Instantiate(m_pipeCellPrefab, m_grid.transform);

                PipeCell pipeCellScript = pipeCell.GetComponent<PipeCell>();

                GameObject pipe = Instantiate(m_pipePrefab, pipeCell.transform);

                Pipe pipeScript = pipe.GetComponentInChildren<Pipe>();

                pipeCellScript.Pipe = pipeScript;

                pipeScript.Cell = pipeCellScript;

                pipeScript.Type = m_defaultLayout[y + (x * length)].PipeType;
                pipeScript.Color = m_defaultLayout[y + (x * length)].PipeColor;

                pipeScript.SetPipe();

                pipeCell.transform.position = m_grid.CellToWorld(new Vector3Int(x, y, 0));

                m_cellGrid[x, y] = pipeCellScript;
            }
        }
    }

    [System.Serializable]
    public struct PipeDefinition
    {
        [SerializeField]
        private PipeTypes m_pipeType;
        [SerializeField]
        private PipeColors m_pipeColor;

        public PipeTypes PipeType { get => m_pipeType; set => m_pipeType = value; }
        public PipeColors PipeColor { get => m_pipeColor; set => m_pipeColor = value; }
    }
}
