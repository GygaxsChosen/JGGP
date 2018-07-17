using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] tiles;
    public GameObject[] walls;
    public List <Vector3> createdTiles;

    public int gridSize;
    public float tileSize;

    public float chanceGrass;
    public float chanceFlowers;

    public float waitTime;

	void Start () {
        StartCoroutine(GenerateLevel());
	}

    IEnumerator GenerateLevel()
    {
        for (int i=0; i < gridSize; i++)
        {
            for (int j=0; j < gridSize; j++)
            {
                transform.position = new Vector3(tileSize * i, tileSize * j, 0);
                float typeOfTile = Random.Range(0f, 1f);
                CallCreateTile(typeOfTile);

                //only necessary if we want to see individual tiles generated
                //yield return new WaitForSeconds(waitTime);
            }
            
        }

        transform.position = new Vector3(0, 0, 0);
        for (int i = 0; i < gridSize; i++)
        {
            transform.position = new Vector3((tileSize * i), -.15f, 0);
            CreateWall(0);
        }

        transform.position = new Vector3(0, gridSize * tileSize, 0);
        for (int i = 0; i < gridSize; i++)
        {
            transform.position = new Vector3((tileSize * i), gridSize * tileSize - .15f, 0);
            CreateWall(0);
        }

        transform.position = new Vector3(0, 0, 0);
        for (int j = 0; j < gridSize; j++)
        {
            transform.position = new Vector3(-.08f, j * tileSize -0.065f, 0);
            CreateWall(1);
        }

        transform.position = new Vector3(gridSize * tileSize, 0, 0);
        for (int j = 0; j < gridSize; j++)
        {
            transform.position = new Vector3(tileSize * gridSize, j * tileSize - 0.065f, 0);
            CreateWall(1);
        }

        yield return 0;
    }

    void CallCreateTile(float type)
    {
        if (type < chanceGrass)
        {
            int variation = 0;
            CreateTile(variation);
        } else if (type < chanceFlowers)
        {
            int variation = Random.Range(1, 3);
            CreateTile(variation);
        } else
        {
            int variation = Random.Range(3, 6);
            CreateTile(variation);
        }
    }
	
    void CreateTile(int tileIndex)
    {
        GameObject tileObject;
        tileObject = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

        createdTiles.Add(tileObject.transform.position);
    }

    void CreateWall(int wallIndex)
    {
        GameObject wallObject;
        wallObject = Instantiate(walls[wallIndex], transform.position, transform.rotation) as GameObject;

        createdTiles.Add(wallObject.transform.position);
    }
}
