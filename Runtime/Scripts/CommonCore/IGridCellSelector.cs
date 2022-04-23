using UnityEngine;

namespace ATH.GBS
{
    public interface IGridCellSelector
    {
        Vector2Int? GetSelectedCoordinate();
    }
}
