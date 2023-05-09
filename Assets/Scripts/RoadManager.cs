using System.Collections.Generic;
using SVS;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private RoadPrefabsData roadPrefabsData;
    
    [SerializeField] private PlacementManager placementManager;
    
    private bool _placementMode = false;
    
    private Vector3Int _startPosition;

    private List<Vector3Int> _roadPositionsToRecheck = new();
    private List<Vector3Int> _temporaryPlacementPositions = new();

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
        
        if (!_placementMode)
        {
            _temporaryPlacementPositions.Clear();
            _roadPositionsToRecheck.Clear();

            _placementMode = true;
            _startPosition = position;
            
            _temporaryPlacementPositions.Add(position);
            placementManager.PlaceTemporaryStructure(position, roadPrefabsData.deadEnd, CellType.Road);
        }
        else
        {
            placementManager.RemoveAllTemporaryStructures();
            _temporaryPlacementPositions.Clear();

            foreach (var positionToFix in _roadPositionsToRecheck)
            {
                _roadFixer.FixRoadAtPosition(positionToFix);
            }
            
            _roadPositionsToRecheck.Clear();

            _temporaryPlacementPositions = placementManager.GetPathBetween(_startPosition, position);

            foreach (var temporaryPosition in _temporaryPlacementPositions)
            {
                if (!placementManager.CheckIfPositionIsFree(temporaryPosition))
                {
                    continue;
                }
                
                placementManager.PlaceTemporaryStructure(temporaryPosition, roadPrefabsData.deadEnd, CellType.Road);
            }
        }
        
        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach (var temporaryPosition in _temporaryPlacementPositions)
        {
            _roadFixer.FixRoadAtPosition(temporaryPosition);
            var neighbours = placementManager.GetNeighbourOfTypeFor(temporaryPosition, CellType.Road);
            foreach (var neighbour in neighbours)
            {
                if (_roadPositionsToRecheck.Contains(neighbour) == false)
                {
                    _roadPositionsToRecheck.Add(neighbour);
                }
            }
        }

        foreach (var roadPosition in _roadPositionsToRecheck)
        {
            _roadFixer.FixRoadAtPosition(roadPosition);    
        }
    }

    public void FinishPlacingRoad()
    {
        _placementMode = false;
        placementManager.AddToStructures();

        if (_temporaryPlacementPositions.Count > 0)
        {
            AudioPlayer.instance.PlayPlacementSound();
        }
        
        _temporaryPlacementPositions.Clear();
        _startPosition = Vector3Int.zero;
    }
}
