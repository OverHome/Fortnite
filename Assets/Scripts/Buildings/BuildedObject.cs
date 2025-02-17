using FishNet.Object;
using UnityEngine;

public class BuildedObject : NetworkBehaviour
{
    [field: SerializeField] public Vector3 BottomPoint { get; private set; }
}
