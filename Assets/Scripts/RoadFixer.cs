using UnityEngine;

public class RoadFixer : MonoBehaviour
{
    [SerializeField] private GameObject deadEnd;
    [SerializeField] private GameObject roadStraight;
    [SerializeField] private GameObject corner;
    [SerializeField] private GameObject threeWay;
    [SerializeField] private GameObject fourWay;

    public void FixRoadAtPosition(PlacementManager placementManager, Vector3Int temporaryPosition)
    {
        // [right, up, left, down]
        var results = placementManager.GetNeighboursTypesFor(temporaryPosition);
        int roadCount = 0;

        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] == CellType.Road)
            {
                roadCount++;
            }
        }

        if (roadCount is 0 or 1)
        {
            CreateDeadEnd(placementManager, results, temporaryPosition);
        }
        else if (roadCount == 2)
        {
            if (CreateStraightRoad(placementManager, results, temporaryPosition))
            {
                return;
            }

            CreateCorner(placementManager, results, temporaryPosition);
        }
        else if (roadCount == 3)
        {
            Create3Way(placementManager, results, temporaryPosition);
        }
        else
        {
            Create4Way(placementManager, results, temporaryPosition);
        }
    }

    private void Create4Way(PlacementManager placementManager, CellType[] results, Vector3Int temporaryPosition)
    {
        placementManager.ModifyStructureModel(temporaryPosition, fourWay, Quaternion.identity);
    }

    private void Create3Way(PlacementManager placementManager, CellType[] results, Vector3Int temporaryPosition)
    {
        if (results[1] == CellType.Road && results[2] == CellType.Road && results[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.identity);
        }
        else if (results[2] == CellType.Road && results[3] == CellType.Road && results[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0f, 90f, 0f));
        }
        else if (results[3] == CellType.Road && results[0] == CellType.Road && results[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0f, 180f, 0f));
        }
        else
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0f, 270f, 0f));
        }
    }

    private void CreateCorner(PlacementManager placementManager, CellType[] results, Vector3Int temporaryPosition)
    {
        if (results[1] == CellType.Road && results[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0f, 90f, 0f));
        }
        else if (results[2] == CellType.Road && results[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0f, 180f, 0f));
        }
        else if (results[3] == CellType.Road && results[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0f, 270f, 0f));
        }
        else
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.identity);
        }
    }

    private bool CreateStraightRoad(PlacementManager placementManager, CellType[] results, Vector3Int temporaryPosition)
    {
        if (results[0] == CellType.Road && results[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, roadStraight, Quaternion.identity);
            return true;
        }
        
        if (results[1] == CellType.Road && results[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, roadStraight, Quaternion.Euler(0f, 90f, 0f));
            return true;
        }

        return false;
    }

    private void CreateDeadEnd(PlacementManager placementManager, CellType[] results, Vector3Int temporaryPosition)
    {
        if (results[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0f, 90f, 0f));
        }
        else if (results[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.identity);
        }
        else if (results[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0f, 270f, 0f));
        }
        else
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0f, 180f, 0f));
        }
    }
}
