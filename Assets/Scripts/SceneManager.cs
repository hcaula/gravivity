using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour {

    public void LoadSceneByName(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
    public void LoadNextScene()
    {
        Scene current = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        Debug.Log("Current Scene: "+ current.buildIndex + " Next Scene: "+ (current.buildIndex+1));

        UnityEngine.SceneManagement.SceneManager.LoadScene(current.buildIndex + 1);
    }
}
