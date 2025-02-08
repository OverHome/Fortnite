using Player.Weapons;
using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerShooter
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _distance;

        [field: SerializeField] public Transform WeaponParent { get; private set; }
        [field: SerializeField] public LayerMask RayMask { get; private set; }
        public Weapon HoldenWeapon { get; private set; }

        private Player _player;

        public const int Damage = 10;

        public void Initialize(Player player)
        {
            _player = player;
        }

        //Server
        public void Shoot()
        {
            if (HoldenWeapon == null)
                return;

            HoldenWeapon.ServerFire();
        }

        //Server
        public void ServerTakeWeapon(Weapon weapon)
        {
            SetWeaponPos(weapon);
            weapon.OnServerTakeWeapon(_player);
        }

        //Client
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

