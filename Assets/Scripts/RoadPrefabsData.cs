using UnityEngine;

[CreateAssetMenu(fileName = "newRoadPrefabsData", menuName = "Data/Road/Road Prefabs Data", order = 0)]
public class RoadPrefabsData : ScriptableObject
{ 
    public GameObject deadEnd;
    public GameObject roadStraight;
    public GameObject corner;
    public GameObject threeWay;
    public GameObject fourWay;
}