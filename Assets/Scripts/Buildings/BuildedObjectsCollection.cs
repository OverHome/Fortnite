using System;
using UnityEngine;

namespace Buildings
{
    [Serializable]
    public class BuildedObjectsCollection
    {
        [SerializeField] private BuildedObject[] _objects;

        private int _index = 0;
        
        public BuildedObject ChosenObject => _objects[_index];

        public void ChooseNext()
        {
            _index = (_index + 1) % _objects.Length;
        }

        public void ChoosePrevious()
        {
            if (_index > 0)
            {
                _index--;
            }
            else
            {
                _index = _objects.Length - 1;
            }
        }
    }
}