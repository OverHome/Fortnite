using Player.Weapons;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [Serializable]
    public class PlayerShooter
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _distance;

        [field: SerializeField] public Transform WeaponParent { get; private set; }
        [field: SerializeField] public LayerMask RayMask { get; private set; }
        [field: SerializeField] public Weapon HoldenWeapon { get; private set; }

        private Player _player;
        
        public void Initialize(Player player)
        {
            _player = player;
        }
        
        public void Shoot()
        {
            if (HoldenWeapon == null)
                return;

            HoldenWeapon.ServerFire();
        }
        
        public void ServerTakeWeapon(Weapon weapon)
        {
            SetWeaponPos(weapon);
            weapon.OnServerTakeWeapon(_player);
        }
        
        public void ClientTakeWeapon(Weapon weapon)
        {
            SetWeaponPos(weapon);
            weapon.OnClientTakeWeapon(_player);
        }

        private void SetWeaponPos(Weapon weapon)
        {
            weapon.transform.SetParent(WeaponParent);
            weapon.transform.localPosition = Vector3.zero;
        }
    }
}

