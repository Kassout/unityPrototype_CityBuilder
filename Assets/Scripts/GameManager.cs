using System;
using SVS;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public InputManager inputManager;

    private void Start()
    {
        inputManager.MouseClick += HandleMouseClick;
    }

    private void HandleMouseClick(Vector3Int position)
    {
        //throw new NotImplementedException();
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovement.x, 0, inputManager.CameraMovement.y)); 
    }
}
