using Unity.VisualScripting;
using UnityEngine;

public class RoadFixer : MonoBehaviour
{
    public GameObject deadEnd;
    public GameObject roadStraight;
    public GameObject corner;
    public GameObject threeWay;
    public GameObject fourWay;

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
        throw new System.NotImplementedException();
    }

    private void Create3Way(PlacementManager placementManager, CellType[] results, Vector3Int temporaryPosition)
    {
        throw new System.NotImplementedException();
    }

    private void CreateCorner(PlacementManager placementManager, CellType[] results, Vector3Int temporaryPosition)
    {
        throw new System.NotImplementedException();
    }

    private bool CreateStraightRoad(PlacementManager placementManager, CellType[] results, Vector3Int temporaryPosition)
    {
        throw new System.NotImplementedException();
    }

    private void CreateDeadEnd(PlacementManager placementManager, CellType[] results, Vector3Int temporaryPosition)
    {
        throw new System.NotImplementedException();
    }
}
