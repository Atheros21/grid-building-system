using UnityEngine;

namespace ATH.GBS
{
    [CreateAssetMenu()]
    public class BuildingData : ScriptableObject
    {
        [SerializeField] private Vector2Int _origin;
        [SerializeReference] private IBuildingView _buildingView;

        public Vector2Int Origin => _origin;
        public IBuildingView View => _buildingView;

        public bool[,] GetShape()
        {
            return new bool[2, 2]
          {
                {true,true,},
                {true,true,},
          };
        }
    }
}