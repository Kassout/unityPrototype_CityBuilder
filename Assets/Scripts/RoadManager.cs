using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;

    public List<Vector3Int> temporaryPlacementPositions = new();

    public GameObject roadStraight;

    public void PlaceRoad(Vector3Int position)
    {
        if (!placementManager.CheckIfPositionInBound(position))
        {
            return;
        }

        if (!placementManager.CheckIfPositionIsFree(position))
        {
            return;
        }

        placementManager.PlaceTemporaryStructure(position, roadStraight, CellType.Road);
    }
}
