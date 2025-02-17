using FishNet.Object;
using UnityEngine;

public class BuildingsPlacer : NetworkBehaviour
{
    [SerializeField] private BuildingObjectTemplate _template;

    public const float YOffset = .05f;

    [field: SerializeField] public float RayDistance { get; private set; }
    [field: SerializeField] public LayerMask RayMask { get; private set; }

    public BuildedObject PlaceObjectPrefab { get; private set; }
    public Transform Arm { get; private set; }

    public void StartPlacing(BuildedObject placeObjectPrefab, Vector3 pos)
    {
        PlaceObjectPrefab = placeObjectPrefab;
        _template.SetNewTemplate(placeObjectPrefab);
        SetObjectPos(pos);
        _template.gameObject.SetActive(true);
    }


    public bool TryPlace()
    {
        if (PlaceObjectPrefab == null || !_template.CanPlace())
            return false;
        PlaceObjectServer(PlaceObjectPrefab,
            _template.transform.position - Vector3.up * YOffset - PlaceObjectPrefab.BottomPoint);
        return true;
    }

    [ServerRpc(RequireOwnership = false)]
    public void PlaceObjectServer(BuildedObject buildedObject, Vector3 pos)
    {
        buildedObject.GetComponent<NetworkObject>().enabled = true;
        var obj = Instantiate(
            buildedObject, 
            pos,
            Quaternion.identity);
        ;
        ServerManager.Spawn(obj.gameObject);
        buildedObject.GetComponent<NetworkObject>().enabled = false;
    }

    public void EndPlacing()
    {
        // PlaceObjectPrefab = null;
        _template.gameObject.SetActive(false);
    }

    public void SetObjectPos(Vector3 pos)
    {
        _template.transform.position = pos + Vector3.up * YOffset;
    }

    public void StartPlacing(BuildedObject buildingObject, Transform arm)
    {
        Arm = arm;
        var pos = GetRayCastPos(arm);
        StartPlacing(buildingObject, pos);
    }

    public void SetObjectPos(Transform arm)
    {
        var pos = GetRayCastPos(arm, out var isCollide);
        SetObjectPos(pos);
        _template.IsOnFloor = isCollide;
    }

    public bool Raycast(Transform arm, out RaycastHit hit)
    {
        var ray = new Ray(arm.transform.position, arm.transform.forward);
        return Physics.Raycast(ray, out hit, RayDistance, RayMask);
    }

    public Vector3 GetRayCastPos(Transform arm)
    {
        if (Raycast(arm, out var hit))
        {
            return hit.point;
        }
        else
        {
            return arm.transform.position + arm.transform.forward * RayDistance;
        }
    }

    public Vector3 GetRayCastPos(Transform arm, out bool isCollide)
    {
        if (Raycast(arm, out var hit))
        {
            isCollide = true;
            return hit.point;
        }
        else
        {
            isCollide = false;
            return arm.transform.position + arm.transform.forward * RayDistance;
        }
    }

    private void Update()
    {
        if (Arm != null && PlaceObjectPrefab != null)
        {
            SetObjectPos(Arm);
        }
    }
}