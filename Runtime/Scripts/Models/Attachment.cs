using System.Collections.Generic;
using UnityEngine;

namespace ATH.HouseBuilding
{
    [CreateAssetMenu]
    public class Attachment : ScriptableObject
    {
        [SerializeField] private string _objectName;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private AttachmentSlot _ocupiedSlot;
        [SerializeField] private List<AttachmentSlot> _enabledSlots;
        [SerializeField] private bool[,] _shape;
        [SerializeField] private Vector2Int _shapeOrigin;

        public string ObjectName => _objectName;
        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;
        public AttachmentSlot OcupiedSlot => _ocupiedSlot;
        public List<AttachmentSlot> EnabledSlots => _enabledSlots;
        public bool[,] Shape => _shape;
        public Vector2Int ShapeOrigin => _shapeOrigin;
    }
}
