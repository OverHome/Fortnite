using System;
using FishNet.Transporting.Tugboat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UpdateIP : MonoBehaviour
    {
        public TMP_InputField ipInputField;
        public Tugboat tugboat;

        private void Start()
        {
            ipInputField.onValueChanged.AddListener(SetIp); 
        }

        private void SetIp(string arg0)
        {
            tugboat.SetClientAddress(arg0);
        }
    }
}