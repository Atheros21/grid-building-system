using UnityEngine;

namespace ATH.GBS
{
    public interface IBuildPreviewView
    {
        void Draw(Vector3 position, Quaternion rotation, IBuildingView view, bool state);
    }
}