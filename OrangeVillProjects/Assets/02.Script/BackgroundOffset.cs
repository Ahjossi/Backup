using UnityEngine;
using System.Collections;

public class BackgroundOffset : MonoBehaviour {
    public Renderer rend;
    public Transform pTr;
    public float h = 0.0f;
    public float speed = 1.0f;
    public float offset = 0.0f;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");

        if (h > 0)
        {
            offset += Time.deltaTime * speed;
            //rend.material.mainTextureOffset = new Vector2(ofs, 0);
            transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 0);
          

        }
        if (h<0)
        {
            offset += Time.deltaTime * speed;
            //rend.material.mainTextureOffset = new Vector2(ofs, 0);
            transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(-offset, 0);
            
        }
        
    }

    /*public Renderer rendMe;
    public Renderer rendPlayer;
    public Renderer rendFloor;
    public Vector3 backgroundBoundsMin;
    public Vector3 backgroundBoundsMax;
    public Vector3 floorBoundsMin;
    public Vector3 floorBoundsMax;
    public Transform player;


    void Awake()
    {
        rendMe = GetComponent<Renderer>();
        backgroundBoundsMin = rendMe.bounds.min;
        backgroundBoundsMax = rendMe.bounds.max;
        floorBoundsMin = player.GetComponent<Renderer>().bounds.min;
        floorBoundsMax = player.GetComponent<Renderer>().bounds.max;

        Vector3 backgroundNm = (backgroundBoundsMax - backgroundBoundsMin).normalized;
        Vector3 playerNm = floorBoundsMin - (floorBoundsMin - player.transform.position)
    }

    void FixedUpdate()
    {

    }*/

}
