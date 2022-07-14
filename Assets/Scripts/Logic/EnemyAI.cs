using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic
{
	internal class EnemyAI : Tank
	{
		public float nextActionTime = 0.0f;
		public float period = 10f;

		public bool allowedToFire = true;

		public IEnumerator Think()
		{
			var whereToMove = new List<Vector2Int>()
			{
				Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down
			};

			yield return TryMove(whereToMove[UnityEngine.Random.Range(0, whereToMove.Count)]);
		}

		public void TryFire(Vector2Int target_coords)
        {
            if (allowedToFire)
            {
				Aim(target_coords);
				Fire();
				allowedToFire = false;
            }
        }

		public void Aim(Vector2Int target_coords)
        {
			var our_coords = GetCoords();

			if (target_coords.x < our_coords.x)
            {
				Rotate(Vector2Int.left);
            }
			else if(target_coords.x > our_coords.x)
            {
				Rotate(Vector2Int.right);
			}
			else if(target_coords.y < our_coords.y)
            {
				Rotate(Vector2Int.down);
			}
			else if(target_coords.y > our_coords.y)
            {
				Rotate(Vector2Int.up);
			}
        }
	}
}
