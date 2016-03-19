using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static class MonsterGenratiorConstancts//#define대신 ㅋ
{
    //전처리할 내용을 넣어주세요 -> public const 형태 이름;
    public const int MAXMONSTER = 5;
}
public class Monster_Genratior : MonoBehaviour
{
    XML_Parsing xmlParsing;
    List<Monster_Info> monstersList = new List<Monster_Info>();//외부에서 몬스터 정보를 읽어오기위해 선언
    public GameObject[] monsterPrefabs;//몬스터 종류
    public Transform[] points;//몬스터 스폰 위치
    int nowMonster=0;
    // Use this for initialization
	void Awake()
    {
        xmlParsing = GetComponent<XML_Parsing>();
        monstersList = xmlParsing.Read(Application.streamingAssetsPath + XmlConstancts.MOBDBXML);
        for (int i = 0; i < 2; i++)//프리팹 id순서대로 초기화, 추후 더 나은방법 찾을것!
        {
            monsterPrefabs[i].GetComponent<Monster>().Insert(monstersList[i].ID,
                monstersList[i].Name, monstersList[i].kName, monstersList[i].Etype, monstersList[i].type,
                monstersList[i].Hp, monstersList[i].Atk,monstersList[i].Spd,monstersList[i].Acc,monstersList[i].AtkSpd);
        }
    }
    void Start()
    {
        StartCoroutine(MonsterGen());
    }
    //1.5초 간격으로 몬스터를 자동 생성
    IEnumerator MonsterGen()
    {
        while (true) { 
            int idx = Random.Range(0, 1);
            if(nowMonster < MonsterGenratiorConstancts.MAXMONSTER)
            {
                Instantiate(monsterPrefabs[idx], points[idx].position, Quaternion.identity);
                nowMonster++;
            }
                
        yield return new WaitForSeconds(2.5f);
        }
    }
}
