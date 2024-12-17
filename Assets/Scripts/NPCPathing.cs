using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCPathing : MonoBehaviour
{
    private int gridHeight = 201;
    private int gridWidth = 201;
    private float cellHeight = 1f;
    private float cellWidth = 1f;

    private bool generatePath;
    private bool visualizeGrid;
    
    private bool pathGenerated;

    private Dictionary<Vector2, Cell> cells;

    private List<Vector2> cellsToSearch;
    private List<Vector2> searchedCells;
    private List<Vector2> finalPath;


    List<Vector2> dests;
    Rigidbody rb;
    [SerializeField] Vector2 destination = new Vector2(0,0);
    int destIndex = 0;
    Vector3 currentDest;
    Boolean isMoving = false;

    void Start()
    {
        Vector2 playerPos = new Vector2((int)Math.Abs(transform.position.x), (int)Math.Abs(transform.position.z));
        Vector2 startPos = new Vector2 (0,0);
        Vector2 endPos = playerPos-destination;
        Pathfinder pathfinder = new Pathfinder();
        rb = this.GetComponent<Rigidbody>();
        dests = pathfinder.Pathfind(startPos,endPos);
    }
    void Update()
    {
        currentDest = new Vector3(dests[destIndex].x, 0, dests[destIndex].y);
        if (currentDest != transform.position && !isMoving)
        {
            rb.velocity = (currentDest-transform.position).normalized;
            isMoving = true;
        }
        else
        {
            destIndex++;
            isMoving = false;
        }
    }
    public List<Vector2> Pathfind(Vector2 startPos, Vector2 endPos)
    {
        GenerateGrid();
        FindPath(startPos,endPos);
        return finalPath;
    }
    private void GenerateGrid(Vector2 endPos)
    {
        cells = new Dictionary<Vector2, Cell>();
        float xDirection = Math.Abs(endPos.x) / endPos.x;
        float yDirection = Math.Abs(endPos.y) / endPos.y;

        for (float x = 0; Math.Abs(x) < Math.Abs(gridWidth); x += xDirection)
        {
            for (float y = 0; Math.Abs(y) < gridHeight; y += yDirection)
            {
                Vector2 pos = new Vector2(x,y);
                cells.Add(pos, new Cell(pos));
            }
        }
    }

    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        searchedCells = new List<Vector2>();
        cellsToSearch = new List<Vector2> {startPos};
        finalPath = new List<Vector2>();
        Cell startCell = cells[startPos];
        startCell.gCost = 0;
        startCell.hCost = GetDistance(startPos, endPos);
        startCell.fCost = GetDistance(startPos, endPos);

        while (cellsToSearch.Count > 0)
        {
            Vector2 cellToSearch = cellsToSearch[0];
            foreach (Vector2 pos in cellsToSearch)
            {
                Cell c = cells[pos];
                if (c.fCost < cells[cellToSearch].fCost || c.fCost == cells[cellToSearch].fCost && c.hCost == cells[cellToSearch].hCost)
                {
                    cellToSearch = pos;
                }
            }
            
            cellsToSearch.Remove(cellToSearch);
            searchedCells.Add(cellToSearch);

            if (cellToSearch == endPos)
            {

                Cell pathCell = cells[endPos];

                while (pathCell.position != startPos)
                {
                    finalPath.Add(pathCell.position);
                    pathCell = cells[pathCell.connection];
                }

                finalPath.Add(startPos);
                return;
            }

            SearchCellNeighbors(cellToSearch,endPos);
        }
    }

    private void SearchCellNeighbors(Vector2 cellPos, Vector2 endPos)
    {
        for (float x = cellPos.x - cellWidth; x <= cellWidth + cellPos.x; x += cellWidth)
        {
            for (float y = cellPos.y - cellHeight; y <= cellHeight + cellPos.y; y += cellHeight)
            {
                Vector2 neighborPos = new Vector2(x, y);

                if (cells.TryGetValue(neighborPos, out Cell c) && !searchedCells.Contains(neighborPos) && !cells[neighborPos].isWall)
                {
                    int GcostToNeighbor = cells[cellPos].gCost + GetDistance(cellPos, neighborPos);

                    if (GcostToNeighbor < cells[neighborPos].gCost)
                    {
                        Cell neighborNode = cells[neighborPos];

                        neighborNode.connection = cellPos;
                        neighborNode.gCost = GcostToNeighbor;
                        neighborNode.hCost = GetDistance(neighborPos, endPos);
                        neighborNode.fCost = neighborNode.gCost + neighborNode.hCost;

                        if (!cellsToSearch.Contains(neighborPos))
                        {
                            cellsToSearch.Add(neighborPos);
                        }
                    }
                }
            }
        }
    }

    private int GetDistance(Vector2 pos1, Vector2 pos2)
    {
        Vector2Int dist = new Vector2Int(Mathf.Abs((int)pos1.x - (int)pos2.x), Mathf.Abs((int)pos1.y - (int)pos2.y));

        int lowest = Mathf.Min(dist.x, dist.y);
        int highest = Mathf.Max(dist.x, dist.y);

        int horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10;
    }

    private class Cell
    {
        public Vector2 position;
        public int fCost = int.MaxValue;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public Vector2 connection;
        public bool isWall;

        public Cell(Vector2 pos)
        {
            position = pos;
        }
    }
}