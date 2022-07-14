using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic
{
	internal class Player : Tank
	{
		public void TryFire()
        {
            if (!isMoving)
            {
                Fire();
            }
        }
	}
}
