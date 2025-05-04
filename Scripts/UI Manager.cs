using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Changes the current scene to the specified scene ID
    // Parameters:
    //   scene_id: The build index of the scene to load (as defined in Build Settings)
    public void changeScene(int scene_id)
    {
        SceneManager.LoadScene(scene_id); // Load the scene with the given build index
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
