using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public Action<Vector3Int> MouseClick;
    public Action<Vector3Int> MouseHold;
    public Action MouseUp;

    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private LayerMask groundMask;
    
    private Vector2 _cameraMovement;

    public Vector2 CameraMovement => _cameraMovement;
    public LayerMask GroundMask => groundMask;

    private void Update()
    {
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
        CheckArrowInput();
    }

    private Vector3Int? RaycastGround()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
        {
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }

        return null;
    }

    private void CheckClickDownEvent()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if (position != null)
            {
                MouseClick?.Invoke(position.Value);
            }
        }
    }

    private void CheckClickUpEvent()
    {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            MouseUp?.Invoke();
        }
    }

    private void CheckClickHoldEvent()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if (position != null)
            {
                MouseHold?.Invoke(position.Value);
            }
        }
    }

    private void CheckArrowInput()
    {
        _cameraMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
