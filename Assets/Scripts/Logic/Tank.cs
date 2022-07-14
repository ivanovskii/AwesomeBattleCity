using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Logic
{
	internal class Tank : MonoBehaviour
	{
	    public bool isMoving { get; private set; }

        public BulletController bulletPrefab;

        private float moveSpeed;
        private Cell[,] cells;

        private Vector2Int direction = Vector2Int.up;

		public void Initialize(float moveSpeed, Cell[,] cells)
		{
            this.moveSpeed = moveSpeed;
            this.cells = cells;
		}

        public IEnumerator TryMove(Vector2Int delta)
        {
            if (isMoving)
            {
                yield break;
            }

            isMoving = true;
            direction = delta;

            var rotationY = Vector2.SignedAngle(Vector2.up, delta * new Vector2Int(-1, 1));
            var from = GetCoords();
            var tc = from + delta;

            var targetCell = cells[tc.x, tc.y];
            if (targetCell.Occupant == null && targetCell.Space == CellSpace.Empty)
            {
                cells[tc.x, tc.y].Occupy(this);
                cells[from.x, from.y].Occupy(null);
                var currentPosition = new Vector3(from.x, 1, from.y);
                var targetPosition = new Vector3(tc.x, 1, tc.y);

                var currentRotation = gameObject.transform.eulerAngles;
                var targetRotation = new Vector3(0, rotationY, 0);

                if (currentRotation.y == 270 && targetRotation.y == -90)
                {
                    targetRotation = new Vector3(0, 270, 0);
                }

                var moveTime = 1f / moveSpeed;
                float t = 0;
                while (t < moveTime)
                {
                    t += Time.deltaTime;
                    gameObject.transform.position = currentPosition + (t / moveTime) * (targetPosition - currentPosition);

                    var f = Mathf.Min(1, 2 * t / moveTime);
                    gameObject.transform.eulerAngles = currentRotation + f * (targetRotation - currentRotation);
                    yield return null;
                }
                gameObject.transform.position = targetPosition;
                gameObject.transform.eulerAngles = targetRotation;
            }
            else
            {
                Rotate(delta);
            }

            isMoving = false;
        }

        public void Rotate(Vector2Int delta)
        {
            direction = delta;

            var rotationY = Vector2.SignedAngle(Vector2.up, delta * new Vector2Int(-1, 1));
            
            var currentRotation = gameObject.transform.eulerAngles;
            var targetRotation = new Vector3(0, rotationY, 0);

            var moveTime = 1f / moveSpeed;
            float t = 0;
            while (t < moveTime)
            {
                t += Time.deltaTime;

                var f = Mathf.Min(1, 2 * t / moveTime);
                gameObject.transform.eulerAngles = currentRotation + f * (targetRotation - currentRotation);
            }
            gameObject.transform.eulerAngles = targetRotation;
        }

        public Vector2Int GetCoords()
        {
            Vector2Int p = default;
            for (var x = 0; x < cells.GetLength(0); x++)
            {
                for (var y = 0; y < cells.GetLength(1); y++)
                {
                    if (cells[x, y].Occupant == this)
                    {
                        p = new Vector2Int(x, y);
                    }
                }
            }
            return p;
        }

        public void Stop()
        {
            isMoving = false;
        }

        public void Fire()
        {
            
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.Initialize(cells, gameObject.GetComponent<Tank>());
            bullet.Fire(direction);
            

            /*
            if (!isMoving)
            {
                var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.Initialize(cells, gameObject.GetComponent<Tank>());
                bullet.Fire(direction);
            }
            */
            
        }

        public void Die()
        { 
            StopAllCoroutines();
            var p = GetCoords();
            cells[p.x, p.y].Occupy(null);
            Destroy(gameObject);
        }
    }
}
