using UnityEngine;

public class StructureModel : MonoBehaviour
{
    private float _yHeight = 0f;

    public void CreateModel(GameObject structure)
    {
        var model = Instantiate(structure, transform);
        _yHeight = model.transform.position.y;
    }

    public void SwapModel(GameObject currentModel, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var newModel = Instantiate(currentModel, transform);
        newModel.transform.localPosition = new Vector3(0, _yHeight, 0);
        newModel.transform.localRotation = rotation;
    }
}
