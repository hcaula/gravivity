using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleControler : MonoBehaviour {

    private SceneManager sm;
    public bool loadNextLevel = true;
    public string sceneToLoad;
	// Use this for initialization
	void Start () {
        sm = FindObjectOfType<SceneManager>();
     
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (sm == null)
        {
            Debug.Log("Scene Manager not found.");
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                if (loadNextLevel)
                {
                    sm.LoadNextScene();
                }
                else
                {
                    sm.LoadSceneByName(sceneToLoad);
                }
            }
        }
    }


}


