using UnityEngine;

namespace ATH.GBS
{
    public class CellView : MonoBehaviour, ICellView
    {
        private const string kColorKey = "_Color";

        [SerializeField] private MeshRenderer _meshRenderer;
        
        private Material _meshMaterial;

        [SerializeField] private Color _hiddenColor;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _highLightColor;
        [SerializeField] private Color _blockedColor;

        public ICellView Create(CellViewState startState, Vector3 position, Quaternion rotation, Transform parent)
        {
            var go = Instantiate(this, position, rotation, parent);
            var view = go.GetComponent<ICellView>();
            view.SetState(startState);
            return view;
        }

        public void SetState(CellViewState state)
        {
            _meshMaterial = _meshRenderer.material;
            switch (state)
            {
                case CellViewState.Hidden:
                    _meshMaterial.SetColor(kColorKey, _hiddenColor);
                    break;
                case CellViewState.Normal:
                    _meshMaterial.SetColor(kColorKey, _normalColor);
                    break;
                case CellViewState.Highlighted:
                    _meshMaterial.SetColor(kColorKey, _highLightColor);
                    break;
                case CellViewState.Blocked:
                    _meshMaterial.SetColor(kColorKey, _blockedColor);
                    break;
            }
        }
    }
}