using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Transform cTransform;
    public GameObject _Player;

    //플레이어의 좌표
    float cx;
    float cy;
    float cz;

	// Use this for initialization
	void Start () {
        cTransform = _Player.GetComponent<Transform>().transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
