using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject roadStraight;
    
    [SerializeField] private RoadPrefabsData roadPrefabsData;
    
    [SerializeField] private PlacementManager placementManager;
    
    private List<Vector3Int> _roadPositionsToRecheck = new();
    private readonly List<Vector3Int> _temporaryPlacementPositions = new();
    
    private RoadFixer _roadFixer;

    private void Start()
    {
        _roadFixer = new RoadFixer(roadPrefabsData);
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

        _temporaryPlacementPositions.Clear();
        _temporaryPlacementPositions.Add(position);
        placementManager.PlaceTemporaryStructure(position, roadStraight, CellType.Road);
        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach (var temporaryPosition in _temporaryPlacementPositions)
        {
            _roadFixer.FixRoadAtPosition(temporaryPosition);
            _roadPositionsToRecheck = placementManager.GetNeighbourOfTypeFor(temporaryPosition, CellType.Road);
        }

        foreach (var roadPosition in _roadPositionsToRecheck)
        {
            _roadFixer.FixRoadAtPosition(roadPosition);    
        }
    }
}
