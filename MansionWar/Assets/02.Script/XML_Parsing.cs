using UnityEngine;
using System.Collections.Generic;
using System.Xml;//xml을 다루기 위해 불러온다.
using System.IO;



public class Monster_Info//데이터 로드 리스트 정의
{
    public string id;
    public string name;
    public float hp;
    public float atk;
}

public class XML_Parsing : MonoBehaviour
{
    string path;
    public List<Monster_Info> monsters = new List<Monster_Info>();

    // Use this for initialization
    void Awake()
    {
        path = Application.streamingAssetsPath + "/Monsters_db_Test.xml";//xml파일 경로
        //monsters = Read(path);//시작시 리스트에 xml데이터를 넣는다.
    }

    // Update is called once per frame
    void Start()
    {
        /*if (monsters.Count > 0)//재대로 들어갔는지 확인
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                Debug.Log("id " + monsters[i].id);
                Debug.Log("name " + monsters[i].name);
                Debug.Log("hp " + monsters[i].hp);
                Debug.Log("atk " + monsters[i].atk);
            }
        }*/
    }
    public List<Monster_Info> Read(string Path)//추후 재정의 해서 쓸 수 있도록 작업 할 것
    {
        XmlDocument Document = new XmlDocument();
        Document.Load(Path);
        XmlElement KeyList = Document.DocumentElement;//키 리스트를 문서의 항목을 사용한다?
        XmlNodeList Nodes = Document.SelectNodes("MonsterInfo/Monster");//monsterinfo아래 Monster항목을 노드로 설정하여 하위항목을 불러오자
        List<Monster_Info> tempList = new List<Monster_Info>();//반환을 위한 임시 리스트

        foreach (XmlNode xn in Nodes)
        {
            Monster_Info m_Info = new Monster_Info();
            m_Info.id = xn["id"].InnerText;
            m_Info.name = xn["Name"].InnerText;
            m_Info.hp = float.Parse(xn["HP"].InnerText);
            m_Info.atk = float.Parse(xn["ATK"].InnerText);
            tempList.Add(m_Info);
        }
        return tempList;        
    }
}
