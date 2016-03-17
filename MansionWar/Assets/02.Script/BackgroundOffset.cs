using UnityEngine;
using System.Collections;

public class BackgroundOffset : MonoBehaviour {
    public Renderer rend;
    public Transform pTr;
    public float h = 0.0f;
    public float speed = 20.0f;
    public float ind;
    public float test=0.0f;
    public float test1 = 1.0f;
    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        ind = Mathf.Clamp01(1.0f);

        if (h > 0)
        {
            //rend.material.mainTextureOffset = new Vector2(ofs, 0);
            transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time*0.5f, 0);

        }

        if(h<0)
        {
            //rend.material.mainTextureOffset = new Vector2(ofs, 0);
            transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(-Time.time*0.5f, 0);

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
