using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCPathing : MonoBehaviour
{
    private int gridHeight = 200;
    private int gridWidth = 200;
    private float cellHeight = 1f;
    private float cellWidth = 1f;

    private bool generatePath;
    private bool visualizeGrid;
    
    private bool pathGenerated;

    private Dictionary<Vector2, Cell> cells;

    private List<Vector2> cellsToSearch;
    private List<Vector2> searchedCells;
    private List<Vector2> finalPath;

    GameObject[] objects;
    List<Vector2> dests;
    Rigidbody rb;
    [SerializeField] GameObject point;
    Vector2 destination;
    Vector3 pointPos;
    [SerializeField] float speed = 1f;
    int destIndex;
    Vector3 currentDest;
    bool destinationReached = false;
//before i forget im finna tag everything as an obstacle and use the coordinates and scale for pathfinding
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        destination = new Vector2((int)(point.transform.position.x + 0.5f),(int)(point.transform.position.z + 0.5f));
        pointPos = point.transform.position;
        GenerateGrid();
        objects = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject block in objects)
        {
            BoxCollider box = block.GetComponent<BoxCollider>();
        }
    }
    void Update()
    {
        if (Vector3.Distance(pointPos,point.transform.position) >= 1)
        {
            pointPos = point.transform.position;
            destination = new Vector2((int)(point.transform.position.x + 0.5f),(int)(point.transform.position.z + 0.5f));
            pathGenerated = false;
            destinationReached = false;
        }
        PathToPoint(destination);
    }

    private Vector2 RoundToVector2(Vector2 subject, Vector2 target, float step)
    {
        Vector2 direction = target - subject;
        if (direction.x != 0)
        {
            direction.x = direction.x / Mathf.Abs(direction.x);
        }
        if (direction.y != 0)
        {
            direction.y = direction.y / Mathf.Abs(direction.y);
        }
        direction *= step;
        return new Vector2((int)(subject.x + direction.x),(int)(subject.y+direction.y));
    }
    public void PathToPoint(Vector2 destination)
    {
        if (!pathGenerated)
        {
            finalPath = new List<Vector2>();
            Pathfind(RoundToVector2(new Vector2(transform.position.x,transform.position.z),destination,cellHeight), destination);
            pathGenerated = true;
            destIndex = finalPath.Count-1;
            currentDest = new Vector3(finalPath[destIndex].x, 0, finalPath[destIndex].y);
            rb.velocity = (currentDest - transform.position).normalized * speed;
        }
        if (Vector3.Distance(currentDest, transform.position) <= 0.1 && !destinationReached)
        {
            if(destIndex > 0)
            {
                destIndex--;
                currentDest = new Vector3(finalPath[destIndex].x, 0, finalPath[destIndex].y);
                rb.velocity = (currentDest - transform.position).normalized * speed;
            }
            else
            {
                rb.velocity = new Vector3(0,0,0);
                destinationReached = true;
            }
        }
    }
    public void Pathfind(Vector2 startPos, Vector2 endPos)
    {
        GenerateGrid();
        FindPath(startPos,endPos);
    }
    private void GenerateGrid()
    {
        cells = new Dictionary<Vector2, Cell>();

        for (float x = 0; x < Math.Abs(gridWidth); x += 1)
        {
            for (float y = 0; y < Math.Abs(gridHeight); y += 1)
            {
                Vector2 pos = new Vector2(x - gridWidth/2, y - gridHeight/2);
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