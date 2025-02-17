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
        [field: SerializeField] public BuildingsPlacer BuildingsPlacer { get; private set; }
        [field: SerializeField] public BuildedObject BuildingsObject { get; private set; }
        
        public InputActionReference _fireInputAction;
        public InputActionReference _buildInputAction;
        
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
                GetComponent<VRTransmission>().enabled = true;
                
                if (_fireInputAction != null)
                {
                    _fireInputAction.action.Enable();
                    _fireInputAction.action.performed += PlayerFire;

                }
                
                if (_buildInputAction != null)
                {
                    _buildInputAction.action.Enable();
                    _buildInputAction.action.started += PlayerBuildStart;
                    _buildInputAction.action.canceled += PlayerBuildCancle;
                }
                
                Health.Died += PlayerDie;
            }
            else
            {
                // this.enabled = false;
            }

            gameObject.name = "Player" + ObjectId;
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            if (base.IsOwner)
            {
                if (_fireInputAction != null)
                {
                    _fireInputAction.action.Disable();
                    _fireInputAction.action.performed -= PlayerFire;
                }
                
                if (_buildInputAction != null)
                {
                    _buildInputAction.action.Disable();
                    _buildInputAction.action.started -= PlayerBuildStart;
                    _buildInputAction.action.canceled -= PlayerBuildCancle;;
                }

                Health.Died -= PlayerDie;
            }
        }

        private void Awake()
        {
            Health.Initialize(this);
            Shooter.Initialize(this);
            
        }
        
        public void PlayerFire(InputAction.CallbackContext context)
        {
            Shooter.HoldenWeapon.PlayShoot();
            Shooter.Shoot();
        }
        
        public void PlayerBuildStart(InputAction.CallbackContext context)
        {
            BuildingsPlacer.StartPlacing(BuildingsObject, GetComponent<VRTransmission>().leftController.gameObject.transform);
        }
        
        public void PlayerBuildCancle(InputAction.CallbackContext context)
        {
            BuildingsPlacer.TryPlace();
            BuildingsPlacer.EndPlacing();
        }

        [ObserversRpc]
        internal void ClientRpcGetDamage(int damage)
        {
            Health.ClientGetDamage(damage);
            print(gameObject.name + " " + Health.Health);
        }

        [ServerRpc(RequireOwnership = false)]
        public void ServerGetDamage(int damage)
        {
            ClientRpcGetDamage(damage);
        }

        private void PlayerDie(Player player)
        {
            Destroy(player.gameObject);
            NetworkManager.ClientManager.StopConnection();
        }
    }
}
