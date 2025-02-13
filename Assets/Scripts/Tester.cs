using FishNet.Object;
using Player.Weapons;
using UnityEngine;

public class Tester : NetworkBehaviour
{
    [SerializeField] private Player.Player _player;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private BuildingsPlacer _buildingsPlacer;
    [SerializeField] private Transform _arm;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private BuildedObject _buildedObject;

    private void Start()
    {
        if(IsServer)
        {
            _player.Shooter.ServerTakeWeapon(_weapon);
        }
    }

    private void Update()
    {
        //FOR TEST AND DEBUG
        if (IsClient)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _player.ServerRpcFire();
            }
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var point = ray.origin + ray.direction * 5f;
            _arm.LookAt(point);
            Debug.DrawLine(ray.origin, point, Color.red);

            if(Input.GetMouseButtonDown(1))
            {
                _buildingsPlacer.StartPlacing(_buildedObject, _arm);
            }
            if(Input.GetMouseButtonUp(1))
            {
                _buildingsPlacer.TryPlace();
                _buildingsPlacer.EndPlacing();
            }
        }
    }
}
