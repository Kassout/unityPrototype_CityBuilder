using System;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;

    public List<Vector3Int> temporaryPlacementPositions = new();
    public List<Vector3Int> roadPositionsToRecheck = new();

    public GameObject roadStraight;

    public RoadFixer roadFixer;

    private void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }

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

        temporaryPlacementPositions.Clear();
        temporaryPlacementPositions.Add(position);
        placementManager.PlaceTemporaryStructure(position, roadStraight, CellType.Road);
        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach (var temporaryPosition in temporaryPlacementPositions)
        {
            roadFixer.FixRoadAtPosition(placementManager, temporaryPosition);
            roadPositionsToRecheck = placementManager.GetNeighbourOfTypeFor(temporaryPosition, CellType.Road);
        }

        foreach (var roadPosition in roadPositionsToRecheck)
        {
            roadFixer.FixRoadAtPosition(placementManager, roadPosition);    
        }
    }
}
