using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

namespace Player.Weapons
{
    public class Weapon : NetworkBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public  LineRenderer LineRenderer { get; private set; }
        public Player OwnerPlayer { get; private set; }

        public const float MaxRayDistance = 5000f;


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
            var start = transform.position;
            var forward = transform.forward;
            var ray = new Ray(start, forward);
            if (Physics.Raycast(ray, out var hit, MaxRayDistance)) //OwnerPlayer.Shooter.RayMask
            {
                print(hit.collider.name);
                if (hit.collider.TryGetComponent<Player>(out var damageable))
                {
                    damageable.ServerGetDamage(Damage);
                }
            }
            
            DrawLine(start, start+forward*MaxRayDistance);
        }

        public void DrawLine(Vector3 start, Vector3 end)
        {
            LineRenderer.startWidth = 0.1f; // Ширина линии
            LineRenderer.endWidth = 0.1f;
            LineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Используем материал для рисования
            LineRenderer.startColor = Color.red;
            LineRenderer.endColor = Color.red;
            LineRenderer.enabled = false; // Отключаем LineRenderer по умолчанию
            LineRenderer.positionCount = 2; // Два сегмента линии
            LineRenderer.SetPosition(0, start); // Начальная точка
            LineRenderer.SetPosition(1, end); // Конечная точка
            LineRenderer.enabled = true;
        }
    }
}

