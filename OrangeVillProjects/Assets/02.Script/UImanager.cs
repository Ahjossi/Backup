using UnityEngine;
using System.Collections;

public class UImanager : MonoBehaviour {
    public GameObject InputBox;
    UIInput uiinput;
    string text;
    float zMove;
    public void getMessage()
    {
        uiinput = InputBox.GetComponent<UIInput>();
        text = uiinput.label.text;
        print(text);
        zMove = float.Parse(text);
        FollowCamera.instance.z = zMove;
    }
}

/*
 public GameObject InputBox;
    UIInput uiinput;
    string text;
    float xMove;
    public void getMessage()
    {
        uiinput = InputBox.GetComponent<UIInput>();
        text = uiinput.label.text;
        print(text);
        xMove = float.Parse(text);
        FollowCamera.instance.x = xMove;
    }
*/
