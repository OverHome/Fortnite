using FishNet.Object;
using UnityEngine;

namespace Player
{
    public class Player : NetworkBehaviour, IDamageable
    {
        [field: SerializeField] public PlayerHealth Health { get; private set; }
        [field: SerializeField] public PlayerShooter Shooter { get; private set; }

        private void Awake()
        {
            Health.Initialize(this);
            Shooter.Initialize(this);
        }

        [ServerRpc]
        public void ServerRpcFire()
        {
            Shooter.Shoot();
        }

        [ObserversRpc]
        internal void ClientRpcGetDamage(int damage, int health)
        {
            Health.ClientGetDamage(damage, health);
        }

        [Server]
        public void ServerGetDamage(int damage)
        {
            Health.ServerGetDamage(damage);
            ClientRpcGetDamage(damage, Health.Health);
        }
    }
}
