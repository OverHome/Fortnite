using FishNet.Object;
using Player.Weapons;
using UnityEngine;

public class Tester : NetworkBehaviour
{
    [SerializeField] private Player.Player _player;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _isBuildingCollidedTester;
    [SerializeField] private BuildingObject _buildingObject;

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
        }
        _isBuildingCollidedTester.SetActive(_buildingObject.IsCollided);
    }
}
