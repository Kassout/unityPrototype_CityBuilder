using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    private static Dictionary<Vector3Int, StructureModel> _temporaryStructures = new();
    private static Dictionary<Vector3Int, StructureModel> _structures = new();
    
    private static Grid _placementGrid;

    private void Start()
    {
        _placementGrid = new Grid(width, height);
    }

    public bool CheckIfPositionInBound(Vector3Int position)
    {
        return position.x >= 0 && position.x < width && position.z >= 0 && position.z < height;
    }

    public bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    private bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return _placementGrid[position.x, position.z] == type;
    }

    public void PlaceTemporaryStructure(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        _placementGrid[position.x, position.z] = type;
        StructureModel structure = CreateNewStructureModel(position, structurePrefab, type);
        _temporaryStructures.Add(position, structure);
    }

    private StructureModel CreateNewStructureModel(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = position;
        var model = structure.AddComponent<StructureModel>();
        model.CreateModel(structurePrefab);
        return model;
    }

    public static void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
    {
        if (_temporaryStructures.TryGetValue(position, out var model))
        {
            model.SwapModel(newModel, rotation);
        }
        else if (_structures.TryGetValue(position, out model))
        {
            model.SwapModel(newModel, rotation);
        }
    }

    internal static CellType[] GetNeighbourTypesFor(Vector3Int position)
    {
        return _placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
    }

    public List<Vector3Int> GetNeighbourOfTypeFor(Vector3Int position, CellType type)
    {
        var neighbourVertices = _placementGrid.GetAdjacentCellsOfType(position.x, position.z, type);
        var neighbours = new List<Vector3Int>();
        foreach (var vertex in neighbourVertices)
        {
            neighbours.Add(new Vector3Int(vertex.X, 0, vertex.Y));
        }

        return neighbours;
    }

    public void RemoveAllTemporaryStructures()
    {
        foreach (var structure in _temporaryStructures.Values)
        {
            var position = Vector3Int.RoundToInt(structure.transform.position);
            _placementGrid[position.x, position.z] = CellType.Empty;
            Destroy(structure.gameObject);
        }
        
        _temporaryStructures.Clear();
    }

    public List<Vector3Int> GetPathBetween(Vector3Int startPosition, Vector3Int endPosition)
    {
        var resultPath = GridSearch.AStarSearch(_placementGrid, 
            new Point(startPosition.x, startPosition.z), 
            new Point(endPosition.x, endPosition.z));

        var path = new List<Vector3Int>();
        foreach (var point in resultPath)
        {
            path.Add(new Vector3Int(point.X, 0, point.Y));
        }

        return path;
    }

    public void AddToStructures()
    {
        foreach (var structure in _temporaryStructures)
        {
            _structures.Add(structure.Key, structure.Value);
        }
        _temporaryStructures.Clear();
    }
}
