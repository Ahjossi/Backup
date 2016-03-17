using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneTest : MonoBehaviour {

    public void OnClick()
    {
        SceneManager.LoadSceneAsync("Intro");
        SceneManager.LoadScene("Main");
        
    }
    
}
