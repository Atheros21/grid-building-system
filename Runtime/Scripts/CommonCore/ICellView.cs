using UnityEngine;

namespace ATH.GBS
{
    public interface ICellView
    {
        ICellView Create(CellViewState startState, Vector3 position, Quaternion rotation, Transform parent);
        void SetState(CellViewState state);
    }
}