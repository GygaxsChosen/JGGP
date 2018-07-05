using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] tiles;
    public GameObject wall;
    public List <Vector3> createdTiles;

    public int tileAmount;
    public float tileSize;

    public float chanceUp;
    public float chanceRight;
    public float chanceDown;

    public float waitTime;

	void Start () {
        StartCoroutine(GenerateLevel());
	}

    IEnumerator GenerateLevel()
    {
        for (int i=0; i < tileAmount; i++)
        {
            float dir = Random.Range(0f, 1f);
            int tile = Random.Range(0, tiles.Length);
            if (tile != 0 && tile != 1 && tile !=2)
            {
                tile = Random.Range(0, tiles.Length);
            }

            CreateTile(tile);
            CallMoveGen(dir);

            yield return new WaitForSeconds(waitTime);
        }
        yield return 0;
    }

    void CallMoveGen(float ranDir)
    {
        if (ranDir < chanceUp)
        {
            MoveGen(0);
        } else if (ranDir < chanceRight)
        {
            MoveGen(1);
        } else if (ranDir < chanceDown)
        {
            MoveGen(2);
        } else
        {
            MoveGen(3);
        }
    }

    void MoveGen(int dir)
    {
        switch (dir)
        {
            case 0:
                transform.position = new Vector3(transform.position.x, transform.position.y + tileSize, 0);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x + tileSize, transform.position.y, 0);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x, transform.position.y - tileSize, 0);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x - tileSize, transform.position.y, 0);
                break;
        }
    }
	
    void CreateTile(int tileIndex)
    {
        GameObject tileObject;
        tileObject = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

        createdTiles.Add(tileObject.transform.position);
    }
}
