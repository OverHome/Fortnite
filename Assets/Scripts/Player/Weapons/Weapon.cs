using FishNet.Object;
using UnityEngine;

namespace Player.Weapons
{
    public class Weapon : NetworkBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        public Player OwnerPlayer { get; private set; }

        public const float MaxRayDistance = 500f;


        public virtual void OnServerTakeWeapon(Player player)
        {
            OwnerPlayer = player;
        }

        public virtual void OnClientTakeWeapon(Player player)
        {
            OwnerPlayer = player;
        }

        public void PlayShoot()
        {
            AudioSource.PlayOneShot(AudioSource.clip);
        }

        [ServerRpc(RequireOwnership = false)]
        public virtual void ServerFire()
        {
            DefaultRayDamage();
        }
        
        [ObserversRpc]
        public void DefaultRayDamage()
        {
            var ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out var hit, MaxRayDistance)) //OwnerPlayer.Shooter.RayMask
            {
                
                if (hit.collider.TryGetComponent<Player>(out var damageable))
                {
                    print(hit.collider.name);
                    damageable.ServerGetDamage(Damage);
                }
            }
            Debug.DrawRay(transform.position, transform.forward, Color.red, 3f);
        }
    }
}

