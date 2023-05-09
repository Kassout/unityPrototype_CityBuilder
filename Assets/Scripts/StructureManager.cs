using System;
using SVS;
using UnityEngine;
using Random = UnityEngine.Random;

public class StructureManager : MonoBehaviour
{
    public StructurePrefabWeighted[] housePrefabs;
    public StructurePrefabWeighted[] specialPrefabs;

    public PlacementManager _placementManager;

    private float[] _houseWeights;
    private float[] _specialWeights;

    private void Start()
    {
        _houseWeights = new float[housePrefabs.Length];
        for (int i = 0; i < housePrefabs.Length; i++)
        {
            _houseWeights[i] = housePrefabs[i].weight;
        }
        
        _specialWeights = new float[specialPrefabs.Length];
        for (int i = 0; i < specialPrefabs.Length; i++)
        {
            _specialWeights[i] = specialPrefabs[i].weight;
        }
    }

    public void PlaceHouse(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            int randomIndex = GetRandomWeightedIndex(_houseWeights);
            _placementManager.PlaceObjectOnTheMap(position, housePrefabs[randomIndex].prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }
    
    public void PlaceSpecial(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            int randomIndex = GetRandomWeightedIndex(_specialWeights);
            _placementManager.PlaceObjectOnTheMap(position, specialPrefabs[randomIndex].prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }

    private int GetRandomWeightedIndex(float[] weights)
    {
        float sum = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
        }

        float randomValue = Random.Range(0, sum);
        float tempSum = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            // 0->weight[0] weight[0]->weight[1]
            if (randomValue >= tempSum && randomValue < tempSum + weights[i])
            {
                return i;
            }
            tempSum += weights[i];
        }

        return 0;
    }

    private bool CheckPositionBeforePlacement(Vector3Int position)
    {
        if (!_placementManager.CheckIfPositionInBound(position))
        {
            Debug.Log("This position is out of bounds.");
            return false;
        }

        if (!_placementManager.CheckIfPositionIsFree(position))
        {
            Debug.Log("This position is not EMPTY.");
            return false;
        }

        if (_placementManager.GetNeighbourOfTypeFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Must be placed near a road.");
            return false;
        }

        return true;
    }
}

[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    [Range(0, 1)]
    public float weight;
    
}
