using System.Collections.Generic;
using UnityEngine;

namespace ATH.GBS
{
    public interface IGridView
    {
        ICellView CellView { get; }

        Vector2Int? GetCellCoordinate(ICellView cellView);
        void Render(CoreGrid grid);
        void ChangeCellsState(List<Vector2Int> coordinates, CellViewState state);
    }
}