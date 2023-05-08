using SVS;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private RoadManager roadManager;
    [SerializeField] private InputManager inputManager;

    private void Start()
    {
        inputManager.MouseClick += HandleMouseClick;
    }

    private void HandleMouseClick(Vector3Int position)
    {
        roadManager.PlaceRoad(position);
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovement.x, 0, inputManager.CameraMovement.y)); 
    }
}
