using System;
using UnityEngine;

namespace Player
{
    public class VRTransmission : MonoBehaviour
    {
        [SerializeField] public Transform xrOrigin;
        [SerializeField] public Transform mainCamera;
        [SerializeField] public Transform rightController;
        [SerializeField] public Transform leftController;
        [SerializeField] public Transform mainBody;
        [SerializeField] public Transform gunHolder;

        private void Update()
        {
            mainBody.GetChild(1).rotation = mainCamera.transform.rotation;
            gunHolder.position = rightController.transform.position;
            gunHolder.rotation = rightController.transform.rotation;
        }
    }
}