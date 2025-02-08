using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    [field: SerializeField] public Vector3 BottomPoint { get; private set; }

    private int _collidersCount = 0;

    public bool IsCollided => _collidersCount > 0;

    private void OnCollisionEnter(Collision collision)
    {
        _collidersCount++;
        Debug.Log("CollisionEnter");
    }

    private void OnCollisionExit(Collision collision)
    {
        _collidersCount--;
        Debug.Log("CollisionExit");
    }
}
