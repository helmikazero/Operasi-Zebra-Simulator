using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class preScene : MonoBehaviour
{

    public float delay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(delay > 0)
        {
            delay -= 1f * Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
