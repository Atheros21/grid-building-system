using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ATH.GBS
{
    public class BaseBuildingView : MonoBehaviour, IBuildingView
    {
        [SerializeField] private float _activationInterval;
        [SerializeField] private List<GameObject> _elements;

        public GameObject GameObject => gameObject;

        public async Task Construct()
        {
            foreach (var item in _elements)
            {
                await Task.Delay(_activationInterval.SecondsToMils());
            }
        }

        public void Init()
        {
            _elements.ForEach(itm => itm.SetActive(false));
        }
    }
}