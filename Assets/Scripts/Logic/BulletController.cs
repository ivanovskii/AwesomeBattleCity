using Assets.Scripts.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

internal class BulletController : MonoBehaviour
{
    private Vector2Int direction;
    public float speed = 10;
    private Cell[,] cells;
    private Tank parent;

    [SerializeField]
    private GameObject explosion;

    public void Initialize(Cell[,] cells, Tank parent)
    {
        this.cells = cells;
        this.parent = parent;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;

        var ourCell = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        if (cells[ourCell.x, ourCell.y].Space != CellSpace.Empty)
        {
            var fx = Instantiate(explosion, transform.position, Quaternion.identity);

            if (cells[ourCell.x, ourCell.y].Space == CellSpace.Destructable)
            {
                var wall = cells[ourCell.x, ourCell.y].UpperCell.GetComponent<DestructableCell>();
                wall.Die();
                cells[ourCell.x, ourCell.y].Space = CellSpace.Empty;
            }

            Destroy(gameObject);
            Destroy(fx, 3);
        }
        
        if (cells[ourCell.x, ourCell.y].Occupant != null)
        {
            var tank = cells[ourCell.x, ourCell.y].Occupant.GetComponent<Tank>();
            if (tank != null && tank != this.parent)
            {
                var fx = Instantiate(explosion, transform.position, Quaternion.identity);
                tank.Die();
                Destroy(gameObject);
                Destroy(fx, 3);
            }
        }
        
    }

    public void Fire(Vector2Int direction)
    {
        this.direction = direction;
    }
}
