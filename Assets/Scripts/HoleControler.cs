using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleControler : MonoBehaviour
{

    #region Public attributes
    public bool loadNextLevel = true;
    public string sceneToLoad;
    #endregion

    #region Private attributes
    private SceneManager sm;
    #endregion

    void Start()
    {
        sm = FindObjectOfType<SceneManager>();
        if (!sm) Debug.Log("Scene Manager not found.");
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (loadNextLevel) sm.LoadNextScene();
            else sm.LoadSceneByName(sceneToLoad);
        }
    }
}


