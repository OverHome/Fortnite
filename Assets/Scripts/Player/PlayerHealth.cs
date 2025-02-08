using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerHealth : IDamageable
    {
        [field: SerializeField, Min(1)] public int MaxHealth { get; private set; }
        public int Health { get; private set; }

        public event Action<Player> Died;

        private Player _player;

        internal void Initialize(Player player)
        {
            _player = player;
            Health = MaxHealth;
        }

        public void ServerGetDamage(int damage)
        {
            Health -= damage;
            if(Health <= 0)
            {
                Health = 0;
                Die();
            }
            _player.ClientRpcGetDamage(damage, Health);
        }

        internal void ClientGetDamage(int damage, int health)
        {
            if(!_player.IsServer)
            {
                Health = health;
            }
            //Hit animations later
        }

        public void Die()
        {
            Died?.Invoke(_player);
        }
    }
}
