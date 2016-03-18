using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    public GameObject bBoard;
    public Camera camera1;

    void Start()
    {
    }
    void Update()
    {
        //bBoard.transform.position = camera1.WorldToViewportPoint(new Vector3(0.5f,0.5f,0.8f));
        //bBoard.transform.position = camera1.WorldToViewportPoint(new Vector3(0.8f,0.8f,7.7f));
        //Vector3 one = camera1.ScreenToWorldPoint(bBoard.transform.position);
        //Debug.Log(one);
        bBoard.transform.position = new Vector3(Screen.width*0.5f, Screen.height*0.5f, 0);
        //bBoard.transform.localScale = new Vector3(Screen.width, Screen.height, 0);
        //bBoard.transform.position = new Vector3(Screen.width * 0.5, Screen.height * 0.5);
    }

}
