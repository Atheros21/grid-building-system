using UnityEngine;

namespace ATH.GBS
{
    public class Building
    {
        private string _id;
        private Quaternion _rotation;

        public string Id { get => _id; }
        public Quaternion Rotation { get => _rotation; }

        public Building(string id, Quaternion rotation)
        {
            _id = id;
            _rotation = rotation;
        }
    }
}