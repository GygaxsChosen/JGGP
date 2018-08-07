using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    #region Public Properties
    public GameObject[] tiles;
    public GameObject[] walls;
    public List <Vector3> createdGrass;
    public List<Vector3> createdRockGrass;
    public List<Vector3> createdTrees;
    public List<Vector3> createdWalls;
    public float tileSize;
    public float chanceGrass;
    public float chanceFlowers;
    public float chanceTrees;
    public float waitTime;
    #endregion

    #region Public Properties
    private int Width = 30;
    private int Height = 30;





	void Start () {
        StartCoroutine(GenerateLevel());
	}

    IEnumerator GenerateLevel()
    {
        for (int j=0; j < 10; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                transform.position = new Vector3(tileSize * i, tileSize * j, 0);
                float typeOfTile = Random.Range(0f, 1f);
                CallCreateGrass(typeOfTile);
            }
        }
        for (int j = 10; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                transform.position = new Vector3(tileSize * i, tileSize * j, 0);
                float typeOfTile = Random.Range(0f, 1f);
                CallCreateGrassWithRocks(typeOfTile);
            }
        }
        for (int j = 0; j < 10; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                transform.position = new Vector3(tileSize * i, tileSize * j, 0);
                float typeOfTile = Random.Range(0f, 1f);
                CallCreateTrees(typeOfTile);
            }
        }

        transform.position = new Vector3(0, 0, 0);
        for (int i = 0; i < Width; i++)
        {
            transform.position = new Vector3((tileSize * i), -.15f, 0);
            CreateWall(0);
        }

        transform.position = new Vector3(0, Width * tileSize, 0);
        for (int i = 0; i < Width; i++)
        {
            transform.position = new Vector3((tileSize * i), Width * tileSize - .15f, 0);
            CreateWall(0);
        }

        transform.position = new Vector3(0, 0, 0);
        for (int j = 0; j < Height; j++)
        {
            transform.position = new Vector3(-.08f, j * tileSize - 0.065f, 0);
            CreateWall(1);
        }

        transform.position = new Vector3(Height * tileSize, 0, 0);
        for (int j = 0; j < Height; j++)
        {
            transform.position = new Vector3(tileSize * Height - .05f, j * tileSize - 0.065f, 0);
            CreateWall(1);
        }

        yield return 0;
    }

    void CallCreateGrass(float type)
    {
        if (type < chanceGrass)
        {
            int variation = 0;
            CreateGrass(variation);
        }
        else
        {
            int variation = Random.Range(1, 3);
            CreateGrass(variation);
        }
    }

    void CallCreateGrassWithRocks(float type)
    {
        if (type < chanceGrass)
        {
            int variation = 0;
            CreateGrassWithRocks(variation);
        }
        else if (type < chanceFlowers)
        {
            int variation = Random.Range(1, 3);
            CreateGrassWithRocks(variation);
        }
        else
        {
            int variation = Random.Range(3, 6);
            CreateGrassWithRocks(variation);
        }
    }

    void CallCreateTrees(float type)
    {
        if (type < chanceTrees)
        {
            CreateTrees(6);
        }
    }

    void CreateGrass(int tileIndex)
    {
        GameObject grass;
        grass = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

        createdGrass.Add(grass.transform.position);
    }

    void CreateGrassWithRocks(int tileIndex)
    {
        GameObject rockGrass;
        rockGrass = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

        createdRockGrass.Add(rockGrass.transform.position);
    }

    void CreateTrees(int tileIndex)
    {
        GameObject treeObject;
        treeObject = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

        createdTrees.Add(treeObject.transform.position);
    }

    void CreateWall(int wallIndex)
    {
        GameObject wallObject;
        wallObject = Instantiate(walls[wallIndex], transform.position, transform.rotation) as GameObject;

        createdWalls.Add(wallObject.transform.position);
    }
}
