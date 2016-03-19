using UnityEngine;
using System.Collections;

public class ObjBind : MonoBehaviour {

    public XML_Parsing xmlParsing;
    public string ID;
    public string WeaponState;
    public string ObjectCode;
    public string MixResult;
    // Use this for initialization
    void Awake()
    {
        xmlParsing = GameObject.Find("MonsterGenerator").GetComponent<XML_Parsing>();
        Debug.Log(this.gameObject.name);
        
    }
    void Start ()
    {
        var temp = xmlParsing.BindDBRead(Application.streamingAssetsPath + XmlConstancts.OBJBINDXML, this.name);
        Insert(temp);
        Debug.Log("나는" + ID + "다!");
        Debug.Log("나는" + WeaponState + "다!");
        Debug.Log("나는" + ObjectCode + "다!");
        Debug.Log("나는" + MixResult + "다!");
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    void Insert(Bind_Info temp)
    {
        ID = temp.ID;
        WeaponState = temp.WeaponState;
        ObjectCode = temp.ObjectCode;
        MixResult = temp.MixResult;
    }
}
