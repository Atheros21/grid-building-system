using System.Threading.Tasks;
using UnityEngine;

namespace ATH.GBS
{
    public interface IBuildingController
    {
        CoreGrid Grid { get; }
        IGridView GridView { get; }
        IBuildPreviewView PreviewView{ get; }

        Task Construct(Vector2Int coordinates, Quaternion rotation, string id);
        bool CanConstruct(Vector2Int corrdinates, Quaternion rotation, string id);
    }
}