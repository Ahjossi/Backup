using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
    // 카메라가 바라볼 타겟
    public Transform target;
    //[Range(0,40)]
    // 카메라의 X,Y 좌표를 넣을 변수
    public float x=25f;
    public float z=7f;
    public float trace=200.0f;
    public Vector3 pin;
    private Transform tr;

    //싱글턴 패턴을 위한 인스턴스 변수 선언
    public static FollowCamera instance = null;

    void Awake()
    {
        //FollowCamera 클래스를 인스턴스에 대입
        instance = this;
    }

    void Start()
    {
        // 카메라 위치 컴포넌트 변수
        tr = GetComponent<Transform>();
    }

	void LateUpdate () {
        //플레이어를 바라보는 카메라의 위치
        //tr.position = Vector3.Lerp(tr.position, target.position - (target.forward * x) + (Vector3.up * y), Time.deltaTime * trace);
        //if (target.transform.position.z <= 3)
        //{
                                                                      // forward(0,0,1) 위아래   up(0,1,0) 카메라 높이
            tr.position = Vector3.Lerp(tr.position, target.position - (target.forward * x) + (Vector3.up * z), Time.deltaTime * trace);
            tr.position = new Vector3(Mathf.Clamp(transform.position.x, -10.0f, 10.0f), Mathf.Clamp(transform.position.y, -5.0f, 17.0f), Mathf.Clamp(transform.position.z, -40.0f, -23.0f));
            tr.localRotation = Quaternion.Euler(11, 0, 0);
            //카메라를 바라봄
            //tr.LookAt(target.position);
            //카메라 다운
        //}ws
        /*if(target.transform.position.x ==3)
        {
            pin = Vector3.Lerp(tr.position, target.position - (target.forward * x) + (Vector3.up * z), Time.deltaTime * trace);
        }
        if (target.transform.position.z > 3)
        {
            tr.position = Vector3.Lerp(tr.position, target.position - (Vector3.up * pin.x) + (Vector3.up * pin.z), Time.deltaTime * trace);
            //tr.position = new Vector3(Mathf.Clamp(transform.position.x, -3.0f, 3.0f), Mathf.Clamp(transform.position.y, -8.0f, 8.0f), Mathf.Clamp(transform.position.z, -8.0f, 4.0f));

            //tr.LookAt(target.position);
        }*/
    }
}