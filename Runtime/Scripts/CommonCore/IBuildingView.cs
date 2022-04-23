using System.Threading.Tasks;
using UnityEngine;

namespace ATH.GBS
{
    public interface IBuildingView
    {
        GameObject GameObject { get; }
        void Init();
        Task Construct();
    }
}