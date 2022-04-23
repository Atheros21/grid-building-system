using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ATH.GBS
{
    public class BuildingController : MonoBehaviour, IBuildingController
    {
        [SerializeField] private GridView _gridView;
        [SerializeField] private LayerMask _cellsLayerMask;
        [SerializeField] private BaseInput _input;

        private CoreGrid _grid;
        private IGridCellSelector _cellSelector;
        private Vector2Int? _currentlySelectedCell;

        public CoreGrid Grid => _grid;
        public IGridView GridView => _gridView;
        public IBuildPreviewView PreviewView => throw new System.NotImplementedException();
        public IGridCellSelector CellSelector => _cellSelector;

        private void Start()
        {
            _grid = GridUtilityTest.GetHGridShaped();
            _gridView.Render(Grid);
            _input = new BaseInput();
            _input.Enable();
            _cellSelector = new RaycastCellSelector(_gridView, _cellsLayerMask, 15, Camera.main, _input.BuildingEditing.MousePosition);
        }

        private void Update()
        {
            var selectedCoord = _cellSelector.GetSelectedCoordinate();
            if(selectedCoord.HasValue)
            {
                _gridView.ChangeCellsState(new List<Vector2Int>() { selectedCoord.Value }, CellViewState.Highlighted);
            }

            if (selectedCoord.HasValue && _currentlySelectedCell.HasValue && selectedCoord.Value != _currentlySelectedCell.Value)
            {
                _gridView.ChangeCellsState(new List<Vector2Int>() { _currentlySelectedCell.Value }, CellViewState.Normal);
            }

            _currentlySelectedCell = selectedCoord;
        }

        public bool CanConstruct(Vector2Int corrdinates, Quaternion rotation, string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Construct(Vector2Int coordinates, Quaternion rotation, string id)
        {
            throw new System.NotImplementedException();
        }

    }
}
