using UnityEngine;
using UnityEngine.InputSystem;

namespace ATH.GBS
{
    public class RaycastCellSelector : IGridCellSelector
    {
        private Camera _camera;
        private InputAction _positionInput;
        private LayerMask _layerMask;
        private float _rayDistance;
        private IGridView _gridView;

        public RaycastCellSelector(IGridView gridView ,LayerMask rayLayerMask, float rayDistance, Camera camera, InputAction positionInput)
        {
            _gridView = gridView;
            _layerMask = rayLayerMask;
            _rayDistance = rayDistance;
            _camera = camera;
            _positionInput = positionInput;
        }

        public Vector2Int? GetSelectedCoordinate()
        {
            var ray = _camera.ScreenPointToRay(_positionInput.ReadValue<Vector2>());

            if(Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _layerMask))
            {
                if(hit.transform.TryGetComponent(out ICellView cellView))
                {
                    return _gridView.GetCellCoordinate(cellView);
                }
            }

            return null;
        }
    }
}
