using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] tiles;
    public GameObject wall;
    public List <Vector3> createdTiles;

    public int gridSize;
    public float tileSize;

    public float chanceGrass;
    public float chanceFlowers;
    //public float chanceRock;

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
            
                //int tile = Random.Range(0, tiles.Length);
                //if (tile != 0 && tile != 1 && tile != 2)
                //{
                //    tile = Random.Range(0, tiles.Length);
                //}

                //CreateTile(tile);

                yield return new WaitForSeconds(waitTime);
            }
            
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

    //void TileSelector(int type)
    //{
    //    switch (type)
    //    {
    //        case 0:
    //            transform.position = new Vector3(transform.position.x, transform.position.y + tileSize, 0);
    //            break;
    //        case 1:
    //            transform.position = new Vector3(transform.position.x + tileSize, transform.position.y, 0);
    //            break;
    //        case 2:
    //            transform.position = new Vector3(transform.position.x, transform.position.y - tileSize, 0);
    //            break;
    //        case 3:
    //            transform.position = new Vector3(transform.position.x - tileSize, transform.position.y, 0);
    //            break;
    //    }
    //}
	
    void CreateTile(int tileIndex)
    {
        GameObject tileObject;
        tileObject = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

        createdTiles.Add(tileObject.transform.position);
    }
}
