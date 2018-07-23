using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderAtoB : MonoBehaviour {

    private float Timer = 0.0f;
    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;

    // Use this for initialization
    void Start () {
        myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/Scenes");
        //scenePaths = myLoadedAssetBundle.GetAllScenePaths();
    }
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;

        if (Timer > 3)
        {
            SceneManager.LoadScene("Enemy with new Manages");

        }
        else if (Timer > 6)
        {
            Timer = 0;
            SceneManager.LoadScene("July16_Collisions");
        }
    }
}
