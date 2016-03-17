﻿using UnityEngine;
using System.Collections;

static class Constancts//#define대신 ㅋ
{
    //전처리할 내용을 넣어주세요 -> public const 형태 이름;
    public const string body01 = "shop_img02";
    public const string body02 = "stake-";
}
public class Monster : MonoBehaviour
{

    public enum MonsterState { IDLE, TRACE, ATK, BEATEN, DIE }; //상태 선언
    public MonsterState monsterState = MonsterState.IDLE;//기본상태로 초기화

    public SkeletonAnimation monsterAnimation;//spine 애니메이션
    private string curAnimation = "";//현재 실행중인 애니메이션
    public Sprite[] sprite;//이미지 교체를 위해
    [SpineSlot]
    public string slot;//교체될 이미지가들어갈 슬롯
    [SpineSkin]
    public string skin;//애니 본에 씌움

    public Collider atkColl;//공격 콜리더
    public Collider defColl;//쳐맞 콜리더
    public GameObject monsterGen;//몹 생성기
    private NavMeshAgent nvAgent;//추적을 위한 네비
    private Transform monsterTr;//몹 자신의 위치
    public Transform playerTr;//플레이어 위치
                    
    public float traceDist = 10f; // 추적 거리
    public float attackDist = 2f; // 공격 거리

    //몬스터의 정보(외부 입력 가능)
    public string id;//몬스터 관리를 위한 id (과연 id가 쓸모 있는건지 생각좀 해봐야됨)
    public string m_name;
    public float hp;
    public float atk;
    private float moveSpeed = 0.05f;//몹 이속
    private float tPosition;//목표 방향
    private bool isDie = false;//몬스터 행동여부
    private bool isFly = false;//몬스터 비행여부
    public bool isAtk = false;//몬스터 공격판정
    // Use this for initialization
    void Start()
    {
        var SkeletonRender = GetComponent<SkeletonRenderer>();
        var attachMent = SkeletonRender.skeleton.Data.AddUnitySprite(slot, sprite[0], skin);
        SkeletonRender.skeleton.SetAttachment(slot, sprite[0].name);
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();//플레이어 위치 가져옴
        monsterTr = GetComponent<Transform>();//내 위치정보 가져옴
        monsterGen = GameObject.Find("MonsterGenerator");
        nvAgent = GetComponent<NavMeshAgent>();//네비는 몬스터의 매시에이전트
        nvAgent.destination = playerTr.position;//네비가 가리키는 목표는 플레이어
        atkColl = GetComponentInChildren<Collider>();//자식오브젝트에 있는 공격 콜리더 가져옴
        //atkColl.enabled = false;

        if (this.transform.position.x > playerTr.position.x)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (this.transform.position.x < playerTr.position.x)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        StartCoroutine(MonsterAction());
        StartCoroutine(MonsterStateCheck());
        //nvAgent.

    }
    // Update is called once per frame
    void Update()
    {
        /*if (atkColl.enabled == true)
        {
            atkColl.enabled = false;
        }*/

    }
    //몬스터 정보를 외부에서 초기화 하기위한 함수입니다.
    public void Insert(string i_id, string i_name, float i_hp, float i_atk)
    {
        id = i_id;
        m_name = i_name;
        hp = i_hp;
        atk = i_atk;
    }
    //몬스터의 행동을 위한 코루틴 함수입니다.
    IEnumerator MonsterAction()
    {//NavMeshAgent는 2d에서 몬써먹나?
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.IDLE:
                    if (this.transform.position.x > playerTr.position.x)
                    {
                        this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (this.transform.position.x < playerTr.position.x)
                    {
                        this.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    break;
                case MonsterState.DIE:
                    nvAgent.Stop();
                    break;
                case MonsterState.TRACE: 
                    if (this.transform.position.x > playerTr.position.x)
                    {
                        this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (this.transform.position.x < playerTr.position.x)
                    {
                        this.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    nvAgent.destination = playerTr.position;
                    nvAgent.Resume();
                    break;
                case MonsterState.BEATEN:
                    nvAgent.Stop();
                    break;
                case MonsterState.ATK:
                    atkColl.enabled = false;
                    if (this.transform.position.x > playerTr.position.x)
                    {
                        this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (this.transform.position.x < playerTr.position.x)
                    {
                        this.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    break;
            }
            yield return null;
        }
    }
    IEnumerator MonsterStateCheck()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.5f);
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);
            //if()쳐맞을때
            if(dist <= attackDist)
            {
                monsterState = MonsterState.ATK;
                //공격 에니 set할것
                //박스 알아서 만들어
                Beated();
                SetAnimation("test", true, 1.0f);
            }
            else if (dist <= traceDist)
            {
                monsterState = MonsterState.TRACE;
                SetAnimation("run", true, 1.0f);
            }
            else
            {
                monsterState = MonsterState.IDLE;
                SetAnimation("stay", true, 1.0f);
            }
        }
    }
    public IEnumerator MonsterAtk()
    {
        atkColl.enabled = true;
        yield return new WaitForSeconds(1);
        atkColl.enabled = false;
    }
    public void SetAnimation(string name, bool loop, float speed)//스켈레톤 애니 세팅 이름,루프여부,재생속도
    {
        if (name == curAnimation)
        {
            return;
        }
        else if(name=="test")
        {
            atkColl.enabled = false;
            StartCoroutine(MonsterAtk());
            monsterAnimation.state.SetAnimation(0, name, loop).timeScale = speed;
            curAnimation = name;
        }
        else
        {
            monsterAnimation.state.SetAnimation(0, name, loop).timeScale = speed;
            curAnimation = name;
        }
    }
    public void DestroyMonster()//몬스터 사망시 실행하는 함수
    {
        Destroy(this.gameObject, 0.1f);
    }
    public void Beated()//몬스터가 맞을때마다 실행하는 경직 애니 함수
    {
        var SkeletonRender = GetComponent<SkeletonRenderer>();
        var attachMent = SkeletonRender.skeleton.Data.AddUnitySprite(slot, sprite[1], skin);
        SkeletonRender.skeleton.SetAttachment(slot, sprite[1].name);
    }
  
}