using System.Collections.Generic;
using UnityEngine;

public class BuildingObjectTemplate : MonoBehaviour
{
    [field: SerializeField] public Material Material { get; private set; }
    [field: SerializeField] public Color CanPlaceColor { get; private set; }
    [field: SerializeField] public Color CantPlaceColor { get; private set; }
    [field: SerializeField] public int BuildingObjectLayer { get; private set; }

    private int _collidersCount = 0;
    private bool _lastCanPlace = true;

    public bool IsCollided => _collidersCount > 0;
    public bool IsOnFloor { get; set; }
    public BuildedObject ActiveTemplate { get; private set; }

    private void Awake()
    {
        Material.color = IsCollided ? CantPlaceColor : CanPlaceColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collidersCount++;
    }

    private void OnCollisionExit(Collision collision)
    {
        _collidersCount--;
    }

    public bool CanPlace()
    {
        return IsOnFloor && !IsCollided;
    }

    public Color GetPlaceColor()
    {
        return CanPlace() ? CanPlaceColor : CantPlaceColor;
    }

    private void Update()
    {
        var canPlace = CanPlace();
        if(_lastCanPlace != canPlace)
        {
            Material.color = GetPlaceColor();
        }
        _lastCanPlace = canPlace;
    }

    public void SetNewTemplate(BuildedObject template)
    {
        if(ActiveTemplate != null)
        {
            Destroy(ActiveTemplate.gameObject);
        }
        _collidersCount = 0;
        ActiveTemplate = Instantiate(template, transform.position, Quaternion.identity, transform);
        ActiveTemplate.transform.localPosition = -ActiveTemplate.BottomPoint;

        foreach(var collider in ActiveTemplate.GetComponentsInChildren<Collider>())
        {
            collider.gameObject.layer = BuildingObjectLayer;
        }
        foreach(var renderer in ActiveTemplate.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = Material;
        }
    }
}
