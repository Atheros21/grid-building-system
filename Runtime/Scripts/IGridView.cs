using System.Collections.Generic;
using UnityEngine;

namespace ATH.HouseBuilding
{
	public interface IGridView
	{
		void Render(CoreGrid grid);
		void Highlight(List<Vector2Int> coordinates, CoreGrid grid, object style);
	}
}