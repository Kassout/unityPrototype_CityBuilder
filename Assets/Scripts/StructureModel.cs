using UnityEngine;

public class StructureModel : MonoBehaviour
{
    private float _yHeight = 0f;

    public void CreateModel(GameObject model)
    {
        var structure = Instantiate(model, transform);
        _yHeight = structure.transform.position.y;
    }

    public void SwapModel(GameObject model, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var structure = Instantiate(model, transform);
        structure.transform.localPosition = new Vector3(0, _yHeight, 0);
        structure.transform.localRotation = rotation;
    }
}
