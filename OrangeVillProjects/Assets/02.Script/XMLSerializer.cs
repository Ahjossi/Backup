using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*[System.Serializable]
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
}*/

public class XMLSerializer : MonoBehaviour
{
    string path = "Assets/streamingAssetsPath/Player_db_Test.xml";//xml을 불러올 경로

    [XmlArray("Player"), XmlArrayItem("PlayerInfo")]
    public List<PlayerInfo> m_playerlist = new List<PlayerInfo>();

    void Start()
    {
       for (int i = 0; i < 5; i++)
        {
            PlayerInfo m_info = new PlayerInfo();
            m_info.name = "a";
            m_info.hp = (i*10)+10;
            m_info.atk = i+1;
            m_info.def = i;
            m_playerlist.Add(m_info);
        }
        Debug.Log("초기화 완료.");

        XmlSaveUTF8(path);
        #if UNITY_EDITOR
        AssetDatabase.Refresh();
        #endif
        Debug.Log("직렬화 완료");

        var m_player = XmlLoadUTF8(path);

        Debug.Log("데이터를 불러옵니다.");
        if (m_player.Count > 0)
        { 
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("name : " + m_player[i].name);
                Debug.Log("hp : " + m_player[i].hp);
                Debug.Log("atk : " + m_player[i].atk);
                Debug.Log("def : " + m_player[i].def);
            }
        }
    }
    void XmlSaveUTF8(string path)
    {
        var serializer = new XmlSerializer(typeof(List<PlayerInfo>));
        using(var stream = new FileStream(path, FileMode.Create))
        {
            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
            serializer.Serialize(streamWriter, this.m_playerlist);
        }
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