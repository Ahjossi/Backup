using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BindControl : MonoBehaviour {
    /// <summary>
    /// 추후 Forbolg라는 케릭터 스크립트와 병합 할 것
    /// </summary>
    XML_Parsing xmlParsing;
    public GameObject Player;
    public Transform PlayerHand;
    public GameObject MyWeapon;
    public GameObject temp;
    public Collider Coll;
    public string bindName;
    public int atk;
    public bool isatk;
    List<Bind_Info> BindList = new List<Bind_Info>();
    // Use this for initialization
    void Start () {
        Coll = GetComponent<Collider>();
        Coll.enabled = false;
        xmlParsing = GameObject.Find("MonsterGenerator").GetComponent<XML_Parsing>();
        BindList = xmlParsing.BindDBListRead(Application.streamingAssetsPath + XmlConstancts.OBJBINDXML);
        PlayerHand = GameObject.Find("Hand").GetComponent<Transform>();
        MyWeapon = GetComponent<GameObject>();
        for (int i = 0; i < BindList.Count; i++)
        {
            Debug.Log("리스트  " + BindList[i].ID);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))//조합키
        {
            StartCoroutine(CollAction());
            Debug.Log("뭐지");
        }
        if (Input.GetKeyDown(KeyCode.Q))//공격
        {
            Debug.Log("공격");
            isatk = true;
            StartCoroutine(CollAction());   
        }
            MyWeapon.transform.position = PlayerHand.transform.position;//무기 위치
	}
    void OnTriggerStay(Collider other)
    {
        //나중에 거리별로 우선순위 매겨서 저장할 것
        bindName = other.name;
        if (other.tag != "Monster" && isatk==false)
        {
            BindSerch();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Monster" && MyWeapon.name == "Weapon_003(Clone)" && isatk == true)
        {
            Debug.Log("잡았다");
            Destroy(other.gameObject);
        }
        
    }
    void BindSerch()//무슨옵젝인지검색
    {
        for (int i = 0; i < BindList.Count; i++)
        {
            if (BindList[i].ID == bindName)
            {
                temp = GameObject.Find(BindList[i].MixResult);
                Debug.Log("앗 무기닷");
                Debug.Log(bindName);
                if (MyWeapon!=null) { DestroyObject(MyWeapon.gameObject); }
                MyWeapon = Instantiate(temp, PlayerHand.position, Quaternion.identity) as GameObject;
                atk = 99;
                bindName = null;
            }
        }
    }
    IEnumerator CollAction()//콜리더 온오프
    {
        Coll.enabled = true;
        yield return new WaitForSeconds(0.05f);
        if (isatk == true)
            isatk = false;
        Coll.enabled = false;
    }
}

