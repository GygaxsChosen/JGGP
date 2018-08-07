using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour 
{
    public GameObject gameManager;

	// This is to Load the game
	void Awake () 
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
	}
	
}
