using SVS;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private RoadManager roadManager;
    [SerializeField] private InputManager inputManager;

    private void Start()
    {
        inputManager.MouseClick += roadManager.PlaceRoad;
        inputManager.MouseHold += roadManager.PlaceRoad
        inputManager.MouseUp += roadManager.FinishPlacingRoad;
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovement.x, 0, inputManager.CameraMovement.y)); 
    }
}
