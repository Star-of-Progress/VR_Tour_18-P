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

    public void changeScene(int scene_id)
    {
        SceneManager.LoadScene(scene_id);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
