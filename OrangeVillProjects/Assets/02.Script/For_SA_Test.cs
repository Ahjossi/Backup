using UnityEngine;
using System.Collections;

public class For_SA_Test : MonoBehaviour
{

    public SkeletonAnimation monsterAnimation;//spine 애니메이션
    private string curAnimation = "";//현재 실행중인 애니메이션
    public Sprite[] sprite;//이미지 교체를 위해
    [SpineSlot]
    public string slot;//교체될 이미지가들어갈 슬롯
    [SpineSkin]
    public string skin;//애니 본에 씌움

    // Use this for initialization
    void Start()
    {
        var SkeletonRender = GetComponent<SkeletonRenderer>();
        var attachMent = SkeletonRender.skeleton.Data.AddUnitySprite(slot, sprite[0], skin);
        SkeletonRender.skeleton.SetAttachment(slot, sprite[0].name);
        //SetAnimation("dead", false, 1.0f);
        StartCoroutine(CheckAni());
        
    }
    public void SetAnimation(string name, bool loop, float speed)//스켈레톤 애니 세팅 이름,루프여부,재생속도
    {
        if (name == curAnimation)
        {
            return;
        }
        else
        {
            monsterAnimation.state.SetAnimation(0, name, loop).TimeScale = speed;
            //monsterAnimation.state.SetAnimation
            curAnimation = name;
        }
    }
    IEnumerator CheckAni()
    {
        var SkeletonRender = GetComponent<SkeletonRenderer>();
        var attachMent = SkeletonRender.skeleton.Data.AddUnitySprite(slot, sprite[1], skin);
        SkeletonRender.skeleton.SetAttachment(slot, sprite[1].name);
        yield return new WaitForSeconds(2.0f);
        SetAnimation("dead", false, 1.0f);
      
        yield return new WaitForSeconds(2.0f);
        attachMent = SkeletonRender.skeleton.Data.AddUnitySprite(slot, sprite[0], skin);
        SkeletonRender.skeleton.SetAttachment(slot, sprite[0].name);

    }
}
