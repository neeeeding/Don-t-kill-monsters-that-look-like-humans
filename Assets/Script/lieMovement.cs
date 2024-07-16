using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lieMovement : MonoBehaviour
{
    public float speed = 4f; //움직임 속도
    private float x;
    private float y;
    private bool isSlow = false;

    private Vector2 moveDir;
    private Rigidbody2D rigid; //플레이어 리지드바디
    private Animator animator; //플레이어 애니메이션
    private Hp hpScript; //현재 플레이어가 가지고 있는 hp스크립트

    public GameObject dieScreen;

    //public AudioSource walkSound;
    //public AudioSource runSound;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hpScript = GetComponent<Hp>();
    }

    private void Update()
    {
        if (hpScript.hp <= 0) //체력이 0보다 작아요?
        {
            animator.SetBool("die", true); //네. 죽는 애니메이션 실행 할게요.
            animator.SetBool("move", false);
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 0);
            dieScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            animator.SetBool("die", false); //아뇨. 계속 움직이겠습니다.
            dieScreen.SetActive(false);
            MoveInput(); //키보드 입력 받아요.
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = moveDir.normalized * speed;//플레이어 움직임
    }

    public void MoveInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(x, y);

        if (Input.GetKey(KeyCode.Space))
        {
            x = 0;
            y = 0;
            moveDir = new Vector2(0, 0);
        }

        MoveAnimation(); //움직이는 애니메이션 실행 하세요.

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) //쉬프트 눌렀나요?
        {
            speed = isSlow? 1f: 7f; //속도 올려요.
        }
        else
        {
            speed = isSlow ? 1f : 4f;
        }

    }

    private void MoveAnimation()
    {
        animator.SetFloat("moveX", x); //moveX값을 플레이어의 위치 X값을 입력 받은 x로 설정하겠습니다.
        animator.SetFloat("moveY", y); //moveY값을 플레이어의 위치 Y값을 입력 받은 y로 설정하겠습니다.
        if (x == 0 && y == 0) //x랑 y가 변동이 없나요?
        { animator.SetBool("move", false);//네. 걷는 움직임을 멈춥시다.
        }
        else
        { animator.SetBool("move", true);//움직이고 있으니 움직이는 애니메이션을 보이겠습니다.
            //if (speed == 4)
            //{
            //    if (walkSound.isPlaying)
            //        walkSound.Stop();
            //    else
            //        walkSound.Play();
            //}
            //else if(speed == 7)
            //{
            //    if (runSound.isPlaying)
            //        runSound.Stop();
            //    else
            //        runSound.Play();
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy")) //적이랑 닿았나요.
        {
            hpScript.hp--; //체력이 1감소 합니다.

            hpScript.AnimationStart(); //체력 애니메이션을 실행 시키세요.
        }
        if (collision.CompareTag("bread")) //빵이랑 닿았나요.
        {
            if (hpScript.hp > 0)
            {
                Destroy(collision.gameObject);
                hpScript.hp += 3; //체력이 3증가 합니다.
                hpScript.AnimationStart(); //체력 애니메이션을 실행 시키세요.
                StopScreen.instance.itemPage = StopScreen.instance.itemPage < 2 ? 2 : StopScreen.instance.itemPage;
            }
        }
        if (collision.gameObject.CompareTag("slowZone"))
        {
            isSlow = true;
        }
        if (collision.gameObject.CompareTag("chestnut") || collision.gameObject.CompareTag("chimera"))
        {
            hpScript.hp -= 3;
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("slowZone"))
        {
            isSlow = false;
        }
    }
}
