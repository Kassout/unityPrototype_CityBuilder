using System.Collections.Generic;
using UnityEngine;

public class RoadFixer
{
    private static RoadPrefabsData _roadsData;
    
    public RoadFixer(RoadPrefabsData roadsData)
    {
        _roadsData = roadsData;
        
    }
    
    public void FixRoadAtPosition(Vector3Int temporaryPosition)
    {
        // [right, up, left, down]
        var cellTypes = PlacementManager.GetNeighbourTypesFor(temporaryPosition);
        int roadCount = 0;

        foreach (var type in cellTypes)
        {
            if (type == CellType.Road)
            {
                roadCount++;
            }
        }

        if (roadCount is 0 or 1)
        {
            CreateDeadEnd(cellTypes, temporaryPosition);
        }
        else if (roadCount == 2)
        {
            if (CreateStraightRoad(cellTypes, temporaryPosition))
            {
                return;
            }

            CreateCorner(cellTypes, temporaryPosition);
        }
        else if (roadCount == 3)
        {
            Create3Way(cellTypes, temporaryPosition);
        }
        else
        {
            Create4Way(temporaryPosition);
        }
    }

    private static void Create4Way(Vector3Int temporaryPosition)
    {
        PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.fourWay, Quaternion.identity);
    }

    private static void Create3Way(IReadOnlyList<CellType> results, Vector3Int temporaryPosition)
    {
        if (results[1] == CellType.Road && results[2] == CellType.Road && results[3] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.threeWay, Quaternion.identity);
        }
        else if (results[2] == CellType.Road && results[3] == CellType.Road && results[0] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.threeWay, Quaternion.Euler(0f, 90f, 0f));
        }
        else if (results[3] == CellType.Road && results[0] == CellType.Road && results[1] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.threeWay, Quaternion.Euler(0f, 180f, 0f));
        }
        else
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.threeWay, Quaternion.Euler(0f, 270f, 0f));
        }
    }

    private void CreateCorner(IReadOnlyList<CellType> results, Vector3Int temporaryPosition)
    {
        if (results[1] == CellType.Road && results[2] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.corner, Quaternion.Euler(0f, 90f, 0f));
        }
        else if (results[2] == CellType.Road && results[3] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.corner, Quaternion.Euler(0f, 180f, 0f));
        }
        else if (results[3] == CellType.Road && results[0] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.corner, Quaternion.Euler(0f, 270f, 0f));
        }
        else
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.corner, Quaternion.identity);
        }
    }

    private bool CreateStraightRoad(IReadOnlyList<CellType> results, Vector3Int temporaryPosition)
    {
        if (results[0] == CellType.Road && results[2] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.roadStraight, Quaternion.identity);
            return true;
        }
        
        if (results[1] == CellType.Road && results[3] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.roadStraight, Quaternion.Euler(0f, 90f, 0f));
            return true;
        }

        return false;
    }

    private static void CreateDeadEnd(IReadOnlyList<CellType> results, Vector3Int temporaryPosition)
    {
        if (results[1] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.deadEnd, Quaternion.Euler(0f, 270f, 0f));
        }
        else if (results[2] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.deadEnd, Quaternion.identity);
        }
        else if (results[3] == CellType.Road)
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.deadEnd, Quaternion.Euler(0f, 90f, 0f));
        }
        else
        {
            PlacementManager.ModifyStructureModel(temporaryPosition, _roadsData.deadEnd, Quaternion.Euler(0f, 180f, 0f));
        }
    }
}
