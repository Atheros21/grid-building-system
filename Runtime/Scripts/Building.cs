using UnityEngine;

namespace ATH.HouseBuilding
{
	public class Building : IBuilding
	{
		private string _type;
		private string _id;

		public string Type { get => _type; }
		public string Id { get => _id; }

		public Building(string type, string id)
        {
			_type = type;
			_id = id;
        }
	}
}