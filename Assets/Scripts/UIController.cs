using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public event Action RoadPlacement;
    public event Action HousePlacement;
    public event Action SpecialPlacement;

    [SerializeField] private Color outlineColor;

    [SerializeField] private Button placeRoadButton;
    [SerializeField] private Button placeHouseButton;
    [SerializeField] private Button placeSpecialButton;
    
    private List<Button> _buttons;

    private void Start()
    {
        _buttons = new() { placeHouseButton, placeRoadButton, placeSpecialButton };
        
        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            RoadPlacement?.Invoke();
        });
        
        placeHouseButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeHouseButton);
            HousePlacement?.Invoke();
        });
        
        placeSpecialButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeSpecialButton);
            SpecialPlacement?.Invoke();
        });
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        foreach (var button in _buttons)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }
}
