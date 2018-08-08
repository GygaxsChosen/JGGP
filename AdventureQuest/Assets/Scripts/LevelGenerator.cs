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
    public List<Vector3> createdCoins;
    public List<Vector3> createdWalls;
    public List<Vector3> createdRiver;
    public List<Vector3> createdDirt;

    public float tileSize;
    public float dirtTileSize;
    public float chanceGrass;
    public float chanceFlowers;
    public float chanceTrees;
    public float chanceCoins;
    public float chanceWater;
    public float waitTime;
    #endregion

    #region Private Properties
    private int Width = 60;
    private int Height = 60;
    #endregion

    void Start () {
        StartCoroutine(GenerateLevel());
	}

    #region Generate Level
    IEnumerator GenerateLevel()
    {
        #region Generate Grass Biome
        for (int j=0; j < 10; j++)
        {
            for (int i = 0; i < Width - 30; i++)
            {
                transform.position = new Vector3(tileSize * i, tileSize * j, 0);
                float typeOfTile = Random.Range(0f, 1f);
                CallCreateGrass(typeOfTile);
            }
        }
        for (int j = 10; j < Height; j++)
        {
            for (int i = 0; i < Width - 30; i++)
            {
                transform.position = new Vector3(tileSize * i, tileSize * j, 0);
                float typeOfTile = Random.Range(0f, 1f);
                CallCreateGrassWithRocks(typeOfTile);
            }
        }
        for (int j = 20; j < Height; j++)
        {
            for (int i = 0; i < Width - 30; i++)
            {
                transform.position = new Vector3(tileSize * i, tileSize * j, 0);
                float typeOfTile = Random.Range(0f, 1f);
                CallCreateTrees(typeOfTile);
            }
        }
        #endregion

        #region Generate Objects
        for (int j = 10; j < Height; j++)
        {
            for (int i = 10; i < Width; i++)
            {
                transform.position = new Vector3(tileSize * i, tileSize * j, 0);
                float typeOfTile = Random.Range(0f, 1f);
                CallCreateCoins(typeOfTile);
            }
        }
        #endregion

        #region Generate River
        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                transform.position = new Vector3(tileSize * i + 5.63f, tileSize * j, 0);
                float typeOfTile = Random.Range(0f, 1f);
                CallCreateWater(typeOfTile);
            }
        }
        #endregion

        #region Generate Dirt Biome
        for (int j = 0; j < Height; j++)
        {
            for (int i = 33; i < Width; i++)
            {
                transform.position = new Vector3(tileSize * i, tileSize * j, 0);
                float typeOfTile = Random.Range(0f, 1f);
                CallCreateDirt(typeOfTile);
            }
        }
        #endregion

        #region Generate Boundary Walls
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
        #endregion

        yield return 0;
    }
    #endregion

    #region Call Create Tiles
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

    void CallCreateCoins(float type)
    {
        if (type < chanceCoins)
        {
            CreateCoins(7);
        }
    }

    void CallCreateWater(float type)
    {
        if (type < chanceWater)
        {
            CreateRiver(8);
        }
    }

    void CallCreateDirt(float type)
    {
        int variation = Random.Range(9, 11);
        CreateDirt(variation);
    }
    #endregion

    #region Create Tiles
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

    void CreateCoins(int tileIndex)
    {
        GameObject coinObject;
        coinObject = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

        createdTrees.Add(coinObject.transform.position);
    }

    void CreateRiver(int tileIndex)
    {
        GameObject waterObject;
        waterObject = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

        createdRiver.Add(waterObject.transform.position);
    }

    void CreateDirt(int tileIndex)
    {
        GameObject dirtObject;
        dirtObject = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

        createdDirt.Add(dirtObject.transform.position);
    }

    void CreateWall(int wallIndex)
    {
        GameObject wallObject;
        wallObject = Instantiate(walls[wallIndex], transform.position, transform.rotation) as GameObject;

        createdWalls.Add(wallObject.transform.position);
    }
    #endregion
}
