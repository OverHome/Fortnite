using System;
using FishNet.Object;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace Player
{
    public class Player : NetworkBehaviour, IDamageable
    {
        [field: SerializeField] public PlayerHealth Health { get; private set; }
        [field: SerializeField] public PlayerShooter Shooter { get; private set; }
        
        public override void OnStartClient()
        {
            base.OnStartClient();
            if (base.IsOwner)
            {
                GetComponent<XROrigin>().enabled = true;
                GetComponent<InputActionManager>().enabled = true;
                GetComponent<LocomotionSystem>().enabled = true;
                GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                GetComponent<ActionBasedSnapTurnProvider>().enabled = true;
                GetComponent<XROrigin>().CameraFloorOffsetObject.SetActive(true);
            }
            else
            {
                this.enabled = false;
            }
        }
        private void Awake()
        {
            Health.Initialize(this);
            Shooter.Initialize(this);
        }

        private void Update()
        {
            transform.GetChild(1).transform.rotation = GetComponent<XROrigin>().Camera.transform.rotation;
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
