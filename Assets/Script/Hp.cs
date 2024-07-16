using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public float hp = 10; //체력 수
    private bool animationenabled; //체력바 보이기 안보이기 정하는 부울
    private bool dont; //체력바를 보이게 해두었는가?

    private Animator animator; //체력바 애니메이션

    private SpriteRenderer hpgauge; //체력바 이미지

    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        hpgauge = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.LeftAlt)) //alt키 눌렀나요?
        {
            if (animationenabled == true) //체력바가 지금 보여요?
            {
                animationenabled = false; //네. 그래서 이번엔 꺼줬어요.
            }
            else
            {
                animationenabled = true; //아뇨. 그래서 이제 보이게 하려구요.
            }
        }

        if (animationenabled == true) //체력바 보이게 해뒀나요?
        {
            hpgauge.enabled = true; //보입니다.
        }
        else //체력바 안 보이게 해뒀죠?
        {
            hpgauge.enabled = false; //네 안보여요.
        }

        if (hp >= 10)
            hp = 10; //hp 10을 넘기지 마세요.

        animator.SetFloat("hp", hp); //애니메이션 변수 hp를 변수 hp의 값으로 지정하세요.
    }

    public void AnimationStart() //애니메이션 실행 합시다.
    {
        StartCoroutine(Animationtime());
    }

    public IEnumerator Animationtime()
    {
        dont = false; //체력바 보이게 안해 놨다고 칩시다.
        if (animationenabled == false) //체력바 안 보이게 해뒀나요?
            animationenabled = true; //네. 인제 보이게 할게요.
        else //어라 체력바 켜뒀는데요?
            dont = true; //그럼 체력바 보이게 해뒀었다고 기억합시다.
        
        yield return new WaitForSeconds(0.2f); //1초 기다릴게요.

        if (dont == false) //체력바를 이거 실행전에 안 보이게 해뒀어요?
            animationenabled = false; //네. 그래서 다시 체력바 안 보이게 할게요.
    }
}
