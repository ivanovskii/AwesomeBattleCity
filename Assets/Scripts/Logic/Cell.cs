using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic
{
	internal enum CellSpace
	{
		Empty,
		Bedrock,
		Destructable,
	}

	internal class Cell
	{
		public CellSpace Space { get; set; }
		public Tank Occupant;
		public DestructableCell UpperCell;

		public Cell(CellSpace space)
		{
			Space = space;
		}

		public void SetUpperCell(DestructableCell upperCell)
        {
			UpperCell = upperCell;
        }

        public void Occupy(Tank occupant)
		{
			Occupant = occupant;

		}
	}
}
