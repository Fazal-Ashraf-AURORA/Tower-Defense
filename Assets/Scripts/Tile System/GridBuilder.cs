using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    private NavMeshSurface myNavMesh => GetComponent<NavMeshSurface>();
    [SerializeField] private GameObject mainPrefab;

    [SerializeField] private int gridLength = 10;
    [SerializeField] private int gridWidth = 10;

    [SerializeField] private List<GameObject> createdTiles;

    public List<GameObject> GetTileSetup() => createdTiles;
    public void UpdateNavMesh() => myNavMesh.BuildNavMesh();

    [ContextMenu("Build Grid")]
    private void BuildGrid()
    {
        ClearGrid();
        createdTiles = new List<GameObject>();

        for (int x = 0; x < gridLength; x++)
        {
            for(int z = 0; z < gridWidth; z++)
            {
                CreateTile(x,z);
            }
        }
    }
     
    [ContextMenu("Clear Grid")]
    private void ClearGrid()
    {
        foreach (var tile in createdTiles)
        {
            DestroyImmediate(tile);
        }

        createdTiles.Clear();
    }

    private void CreateTile(float xPosition, float zPosition)
    {
        Vector3 newPosition = new Vector3(xPosition, 0, zPosition);
        GameObject newTile = Instantiate(mainPrefab, newPosition, Quaternion.identity, transform);

        createdTiles.Add(newTile);
    }
}
