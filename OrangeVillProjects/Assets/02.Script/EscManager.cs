using UnityEngine;
using System.Collections;

public class EscManager : MonoBehaviour {

	
	void Update () {
	if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
