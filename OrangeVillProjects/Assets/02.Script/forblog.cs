using UnityEngine;
using System.Collections;

public class forblog : MonoBehaviour {
    //스켈레톤 애니메이션
    public SkeletonAnimation Player;
    //현재 실행 중인 애니메이션 이름
    private string cur_animation = "";
    //움직임을 제한하는 변수 선언
    public bool limit_move = false;
    //캐릭터의 Transform 컴포넌트 추가를 위한 변수 선언
    public Transform tr;
    //캐릭터의 움직임을 넣을 변수 선언
    public Vector3 movement;
    //캐릭터의 Horizontal 방향 값 변수 선언
    public float h = 0.0f;
    public float v = 0.0f;
    //캐릭터의 속도
    public float walkSpeed = 1.0f;
    public float jumpSpeed = 0.1f;
    public bool isJump = false;

    void Awake()
    {
        //Player의 컴포넌트
        tr = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        
        //움직임이 제한되지 않을 경우
        if(!limit_move)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = false;
                TransformLimit();
                //Jumping();
            }
            // 키보드 A,D 값을 변수에 넣어준다.
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            //만약 h 값이 0보다 클 경우
            if (h>0)
            {
                /*Vector2 tiling = transform.localScale;
                tiling.x = -Mathf.Abs(tiling.x);
                transform.localScale = tiling;*/
                transform.localRotation = Quaternion.Euler(22.9f, 0, 0);
                //Player.Skeleton.FlipX = true;
                TransformLimit();
                //애니메이션
                //SetAnimation("run", true, 1.0f);
            }
            else if(h<0)
            {
                /* Vector2 tiling = transform.localScale;
                 tiling.x = Mathf.Abs(tiling.x);
                 transform.localScale = tiling;*/
                transform.localRotation = Quaternion.Euler(-22.9f, 180, 0);
                //Player.Skeleton.FlipX = false;
                TransformLimit();
                //애니메이션
                //SetAnimation("run", true, 1.0f);
            }
            else if (v > 0)
            {
                TransformLimit();
            }
            else if (v < 0)
            {
                TransformLimit();
            }
            else
            {
                //SetAnimation("idle", true, 1.0f);
            }
        }
    }

    public void TransformLimit()
    {
        movement.Set(h, 0, v);
        tr.Translate(movement.normalized * walkSpeed * Time.deltaTime, Space.World);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -25.0f, 25.0f), Mathf.Clamp(transform.position.y, -1.0f, 0.0f), Mathf.Clamp(transform.position.z, 20.0f, 25.0f));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -6.6f, 7.0f), 
            Mathf.Clamp(transform.position.y, 0.0f, 3.0f), 
            Mathf.Clamp(transform.position.z, -19.5f, -12.5f));
    }

    void SetAnimation(string name, bool loop, float speed)
    {
        if(name == cur_animation)
        {
            return;
        }
        else
        {
            Player.state.SetAnimation(0, name, loop).TimeScale = speed;
            cur_animation = name;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DoorUp"))
        {
            if(GameManager.Instance.doorHitUp == true)
            {
                Debug.Log("위로 충돌");
                GameManager.Instance.Floor_M.transform.position +=  new Vector3(0, 0, -40.0f);
                transform.position = new Vector3(0, 2.32f, -8.0f);
            }
        }
        if (other.CompareTag("DoorDown"))
        {
            if (GameManager.Instance.doorHitDown == true)
            {
                Debug.Log("아래로 충돌");
                GameManager.Instance.Floor_M.transform.position += new Vector3(0, 0, 40.0f);
                transform.position = new Vector3(0, 2.32f, 8.0f);
            }
        }
        if (other.CompareTag("DoorRight"))
        {
            if (GameManager.Instance.doorHitRight == true)
            {
                Debug.Log("오른쪽으로 충돌");
                GameManager.Instance.Floor_M.transform.position += new Vector3(-70.0f, 0, 0);
                transform.position = new Vector3(-20.0f, 2.32f, 0);
            }
        }
        if (other.CompareTag("DoorLeft"))
        {
            if (GameManager.Instance.doorHitLeft == true)
            {
                Debug.Log("왼쪽으로 충돌");
                GameManager.Instance.Floor_M.transform.position += new Vector3(70.0f, 0, 0);
                transform.position = new Vector3(20.0f, 2.32f, 0);
            }
        }
    }
}
