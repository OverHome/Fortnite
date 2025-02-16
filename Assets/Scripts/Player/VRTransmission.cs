using System;
using UnityEngine;

namespace Player
{
    public class VRTransmission : MonoBehaviour
    {
        [SerializeField] private Transform xrOrigin;
        [SerializeField] private Transform mainCamera;
        [SerializeField] private Transform rightController;
        [SerializeField] private Transform leftController;
        [SerializeField] private Transform mainBody;
        [SerializeField] private Transform gunHolder;

        private void Update()
        {
            mainBody.GetChild(1).rotation = mainCamera.transform.rotation;
            gunHolder.position = rightController.transform.position;
            gunHolder.rotation = rightController.transform.rotation;
        }
    }
}