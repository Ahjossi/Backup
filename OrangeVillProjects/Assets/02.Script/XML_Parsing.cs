using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

static class XmlConstancts
{
    //전처리할 내용을 넣어주세요 -> public const 형태 이름;
    //xml 관련해서 사용할 전처리입니다.
    public const string MOBDBXML =      "/MonsterDB.xml";
    public const string MOBXMLNODE =    "MonsterInfo/Monster";
    public const string playerDbXml =   "/Player_db_Test.xml";
    public const string OBJBINDXML =    "/ObjBindDB.xml";
    public const string BINDXMLNODE =   "BindInfo/Bind";
}

[System.Serializable]
[XmlRoot(ElementName = "XMLSerializer")]
public class PlayerInfo //xml로 불러올 몬스터의 정보를 저장할 클라스
{
    [XmlElement("name")]
    public string name;
    [XmlElement("hp")]
    public float hp;
    [XmlElement("atk")]
    public float atk;
    [XmlElement("def")]
    public float def;
}

public class Monster_Info//데이터 로드 리스트 정의
{
    public string ID;
    public string Name;
    public string kName;
    public int Etype;
    public int type;
    public int Hp;
    public int Atk;
    public float Spd;
    public float Acc;
    public float AtkSpd;
}
public class Bind_Info
{
    public string ID;
    public string WeaponState;
    public string ObjectCode;
    public string MixResult;
}
public class XML_Parsing : MonoBehaviour
{
    static string path;//xml파일 경로


    [XmlArray("Player"), XmlArrayItem("PlayerInfo")]
    public List<PlayerInfo> m_playerlist = new List<PlayerInfo>();
    public List<Monster_Info> monsters = new List<Monster_Info>();
    // Use this for initialization
    void Awake()
    {
        path = Application.streamingAssetsPath;//xml파일 경로
        monsters = Read(path + XmlConstancts.MOBDBXML);//시작시 리스트에 xml데이터를 넣는다.
    }

    // Update is called once per frame
    void Start()
    {
        /*if (monsters.Count > 0)//재대로 들어갔는지 확인
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                Debug.Log("id " + monsters[i].ID);
                Debug.Log("이름 " + monsters[i].kName);
                Debug.Log("영문 이름 " + monsters[i].Name);
                Debug.Log("hp " + monsters[i].Hp);
                Debug.Log("속성" + monsters[i].Etype);
                Debug.Log("공중형" + monsters[i].type);
                Debug.Log("spd " + monsters[i].Spd);
                Debug.Log("atkspd " + monsters[i].AtkSpd);
                Debug.Log("가속도 " + monsters[i].Acc);
                Debug.Log("atk " + monsters[i].Atk);
            }
        }*/
    }
    public List<Monster_Info> Read(string Path)//추후 재정의 해서 쓸 수 있도록 작업 할 것
    {
        XmlDocument Document = new XmlDocument();
        Document.Load(Path);
        XmlElement KeyList = Document.DocumentElement;//키 리스트를 문서의 항목을 사용한다?
        XmlNodeList Nodes = Document.SelectNodes(XmlConstancts.MOBXMLNODE);//monsterinfo아래 Monster항목을 노드로 설정하여 하위항목을 불러오자
        List<Monster_Info> tempList = new List<Monster_Info>();//반환을 위한 임시 리스트

        foreach (XmlNode xn in Nodes)
        {
            Monster_Info m_Info = new Monster_Info();
            m_Info.ID = xn["Monster_Code"].InnerText;
            m_Info.kName = xn["KName"].InnerText;
            m_Info.Name = xn["Name"].InnerText;
            m_Info.Etype = int.Parse(xn["EType"].InnerText);
            m_Info.type = int.Parse(xn["Type"].InnerText);
            m_Info.Hp = int.Parse(xn["HP"].InnerText);
            m_Info.Atk = int.Parse(xn["ATK"].InnerText);
            m_Info.Spd = float.Parse(xn["Speed"].InnerText);
            m_Info.Acc = float.Parse(xn["accel"].InnerText);
            m_Info.AtkSpd = float.Parse(xn["AtkSpeed"].InnerText);
            tempList.Add(m_Info);
        }
        return tempList;        
    }
    public Bind_Info BindDBRead(string path, string id)
    {
        Bind_Info bind_info = new Bind_Info();
        XmlDocument Document = new XmlDocument();
        Document.Load(path);
        XmlElement KeyList = Document.DocumentElement;
        XmlNodeList Nodes = Document.SelectNodes(XmlConstancts.BINDXMLNODE);
        foreach (XmlNode xn in Nodes)
        {
            Bind_Info temp_info = new Bind_Info();
            temp_info.ID = xn["MixCode"].InnerText;
            temp_info.WeaponState = xn["Weapon_State"].InnerText;
            temp_info.ObjectCode = xn["Object_Code"].InnerText;
            temp_info.MixResult = xn["Mix_Result"].InnerText;
            if (temp_info.ID == id)
                bind_info = temp_info;
            else
                Debug.Log("아직 못찾음");
        }
        return bind_info;
    }
    public List<Bind_Info> BindDBListRead(string path)
    {
        List<Bind_Info> bind_list = new List<Bind_Info>();
        XmlDocument Document = new XmlDocument();
        Document.Load(path);
        XmlElement KeyList = Document.DocumentElement;
        XmlNodeList Nodes = Document.SelectNodes(XmlConstancts.BINDXMLNODE);
        foreach (XmlNode xn in Nodes)
        {
            Bind_Info temp_info = new Bind_Info();
            temp_info.ID = xn["MixCode"].InnerText;
            temp_info.WeaponState = xn["Weapon_State"].InnerText;
            temp_info.ObjectCode = xn["Object_Code"].InnerText;
            temp_info.MixResult = xn["Mix_Result"].InnerText;
            bind_list.Add(temp_info);
        }
        return bind_list;
    }
    public Monster_Info MonsterDbRead(string Path)//list사용 안하도록 재정의 하는 중
    {
        Monster_Info m_Info = new Monster_Info();
        return m_Info;
    }
    /// <summary>
    /// Xml파일을 XmlSerializer로 저장할때 윈도우에서 파일 인코딩이 자동으로 UTF-8로 되지 않기 때문에
    /// 저장, 불러오기 할때 인코딩 형식을 utf-8로 고정했습니다.
    /// </summary>
    void XmlSaveUTF8(string path)
    {
        var serializer = new XmlSerializer(typeof(List<PlayerInfo>));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
            serializer.Serialize(streamWriter, this.m_playerlist);
        }
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
        Debug.Log("저장 완료");
    }
    public List<PlayerInfo> XmlLoadUTF8(string path)
    {
        var serializer = new XmlSerializer(typeof(List<PlayerInfo>));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);
            return (List<PlayerInfo>)serializer.Deserialize(streamReader);
        }
    }
}
