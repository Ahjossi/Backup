using UnityEngine;
using System.Collections;

public class UImanager2 : MonoBehaviour {
    //연동할 오브젝트
    public GameObject InputBox;
    //UIInput 매개변수 선언
    UIInput uiinput;
    //입력된 값을 저장할 변수 선언
    string text;
    // y 값 저장할 변수 선언
    float yMove;

    //함수 선언
    public void getMessage()
    {
        //InputBox.GetComponent<UIInput>() 선언
        uiinput = InputBox.GetComponent<UIInput>();
        // 매개변수.label.text 선언
        text = uiinput.label.text;
        //print 출력
        print(text);
        // 문자열을 상수 변환
        yMove = float.Parse(text);
        //싱글턴 값에 저장
        FollowCamera.instance.z = yMove;
    }
}
