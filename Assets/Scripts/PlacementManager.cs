using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    private static Dictionary<Vector3Int, StructureModel> _temporaryRoadObjects = new();
    
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
        _temporaryRoadObjects.Add(position, structure);
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
        if (_temporaryRoadObjects.TryGetValue(position, out StructureModel model))
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
}
