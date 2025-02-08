using FishNet.Object;
using UnityEngine;

namespace Player.Weapons
{
    public class Weapon : NetworkBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; }
        public Player OwnerPlayer { get; private set; }

        public const float MaxRayDistance = 500f;

        [Server]
        public virtual void OnServerTakeWeapon(Player player)
        {
            OwnerPlayer = player;
        }

        [Client]
        public virtual void OnClientTakeWeapon(Player player)
        {
            OwnerPlayer = player;
        }

        [Server]
        public virtual void ServerFire()
        {
            DefaultRayDamage();
        }

        public void DefaultRayDamage()
        {
            var ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out var hit, MaxRayDistance, OwnerPlayer.Shooter.RayMask))
            {
                if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.ServerGetDamage(Damage);
                }
            }
            Debug.DrawRay(transform.position, transform.forward, Color.red, 3f);
        }
    }
}

