using Assets.Scripts.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public GroundChessBoard bedrockVoxel;
    public GameObject destructableVoxel, playerPrefab, enemyPrefab, mainCamera;

    private Cell[,] cells;
    private int width, height;
    private Player player;

    private float playerSpeed = 3, enemySpeed = 2;

    private string[] map = new[] {
        "..E.......",
        "#######...",
        "......E...",
        "..........",
        "..........",
        "**********",
        ".E.**.....",
        "...**.....",
        "...**..P..",
        "...**....."
    };

    private FieldController CreateWalls()
    {
        for (int i = 0; i < width + 2; i++)
        {
            cells[i, 0] = new Cell(CellSpace.Bedrock);
            cells[i, height + 1] = new Cell(CellSpace.Bedrock);
        }

        for (int i = 0; i < height + 2; i++)
        {
            cells[0, i] = new Cell(CellSpace.Bedrock);
            cells[width + 1, i] = new Cell(CellSpace.Bedrock);
        }

        return this;
    }

    private FieldController GenerateGameObjects()
    {
        for (int i = 0; i < height; i++)
        {
            if (map[i].Length != width)
            {
                throw new System.Exception("Invalid map");
            }
            for (int j = 0; j < width; j++)
            {
                switch (map[i][j])
                {
                    case '#':
                        cells[j + 1, i + 1] = new Cell(CellSpace.Bedrock);
                        break;
                    case '*':
                        cells[j + 1, i + 1] = new Cell(CellSpace.Destructable);
                        break;
                    default:
                        cells[j + 1, i + 1] = new Cell(CellSpace.Empty);
                        break;
                }

                switch (map[i][j])
                {
                    case 'P':
                        var playerGO = Instantiate(playerPrefab, new Vector3(j + 1, 1, i + 1), Quaternion.identity, transform);
                        player = playerGO.GetComponent<Player>();
                        player.Initialize(playerSpeed, cells);
                        cells[j + 1, i + 1].Occupy(player);
                        break;

                    case 'E':
                        var enemyGO = Instantiate(enemyPrefab, new Vector3(j + 1, 1, i + 1), Quaternion.identity, transform);
                        var e = enemyGO.GetComponent<EnemyAI>();
                        e.Initialize(enemySpeed, cells);
                        cells[j + 1, i + 1].Occupy(e);
                        break;
                }
            }
        }
        return this;
    }

    private FieldController InstantiateGameObjects()
    {
        for (var x = 0; x < width + 2; x++)
        {
            for (var y = 0; y < height + 2; y++)
            {
                var c = Instantiate(bedrockVoxel, new Vector3(x, 0, y), Quaternion.identity, transform);
                c.SetColor((x + y) % 2 == 0);

                if (cells[x, y].Space == CellSpace.Bedrock)
                {
                    Instantiate(bedrockVoxel, new Vector3(x, 1, y), Quaternion.identity, transform);
                }

                if (cells[x, y].Space == CellSpace.Destructable)
                {
                    var upperCell = Instantiate(destructableVoxel, new Vector3(x, 1, y), Quaternion.identity, transform).GetComponent<DestructableCell>();
                    cells[x, y].SetUpperCell(upperCell);
                }
            }
        }
        return this;
    }

    private FieldController SetCamera()
    {
        mainCamera.transform.position = new Vector3((width + 2) / 2, 12, ((height + 2) / 2) - (float)0.5);
        mainCamera.transform.eulerAngles = new Vector3(90, 0, 0);
        return this;
    }

    void Start()
    {

        height = map.Length;
        width = map[0].Length;

        cells = new Cell[width + 2, height + 2];

        CreateWalls().GenerateGameObjects().InstantiateGameObjects().SetCamera();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(player.TryMove(Vector2Int.right));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(player.TryMove(Vector2Int.left));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(player.TryMove(Vector2Int.up));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(player.TryMove(Vector2Int.down));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Fire();
        }

        for (var x = 0; x < cells.GetLength(0); x++)
        {
            for (var y = 0; y < cells.GetLength(1); y++)
            {
                if (cells[x, y].Occupant is EnemyAI enemy)
                {
                    enemy.StartCoroutine(enemy.Think());
                }
            }
        }
    }
}
