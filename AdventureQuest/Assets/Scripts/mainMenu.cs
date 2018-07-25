using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {
    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;


    public void playGame()
    {
        myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/Scenes");
        SceneManager.LoadScene("Level1");
    }
}
