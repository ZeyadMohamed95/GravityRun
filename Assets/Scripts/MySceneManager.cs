using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    void Start()
    {
    }

    public void ReloadScene()
    {
       var scene = SceneManager.GetActiveScene();

       SceneManager.LoadScene(scene.name);
    }

        
}
