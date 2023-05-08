using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;

    private Grid _placementGrid;

    private void Start()
    {
        _placementGrid = new Grid(width, height);
    }

    public bool CheckIfPositionInBound(Vector3Int position)
    {
        Debug.Log(position);
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

    public void PlaceTemporaryStructure(Vector3Int position, GameObject structure, CellType type)
    {
        _placementGrid[position.x, position.z] = type;
        GameObject newStructure = Instantiate(structure, position, Quaternion.identity);
    }
}
