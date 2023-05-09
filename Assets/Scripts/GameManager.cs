using SVS;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private RoadManager roadManager;
    [SerializeField] private StructureManager structureManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private UIController uiController;
    
    private void Start()
    {
        uiController.RoadPlacement += OnRoadPlacement;
        uiController.HousePlacement += OnHousePlacement;
        uiController.SpecialPlacement += OnSpecialPlacement;
        uiController.BigStructurePlacement += OnBigStructurePlacement;
    }

    private void OnBigStructurePlacement()
    {
        inputManager.ClearInputActions();

        inputManager.MouseClick += structureManager.PlaceBigStructure;
    }

    private void OnSpecialPlacement()
    {
        inputManager.ClearInputActions();
        
        inputManager.MouseClick += structureManager.PlaceSpecial;
    }

    private void OnHousePlacement()
    {
        inputManager.ClearInputActions();
        
        inputManager.MouseClick += structureManager.PlaceHouse;
    }

    private void OnRoadPlacement()
    {
        inputManager.ClearInputActions();
        
        inputManager.MouseClick += roadManager.PlaceRoad;
        inputManager.MouseHold += roadManager.PlaceRoad;
        inputManager.MouseUp += roadManager.FinishPlacingRoad;
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovement.x, 0, inputManager.CameraMovement.y)); 
    }
}
