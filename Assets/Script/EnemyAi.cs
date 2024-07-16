using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private float decision; //적의 행동을 정할 변수
    private bool isplayer = false; //플레이어가 적의 범위에 들어왔는지 확인 하는 변수
    private float speed = 3f; //적의 속도

    private Animator animator; //적의 애니메이션

    private GameObject player; //플레이어
    private Vector3 myPosition; //적(자신)의 위치
    private Vector3 playerPosition; //플레이어의 위치
    private Vector3 moveDir;

    public Collider2D before; //변신 후 콜라이더
    public Collider2D after; //변신 전 콜라이더
    public Collider2D afterRange; //변신 전 적의 범위(콜라이더)

    public GameObject bulletCharge;
    private float itemRandom;

    private Hp enemyHp;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyHp = GetComponent<Hp>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("MovePosition"); //변신 전의 적의 움직임을 정할 수를 랜덤으로 3초 마다 정하세요.
        before.enabled = false; //변신 안했으니 변신 후의 콜라이더는 비활성화 할게요.
    }

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            if (isplayer == true) //플레이어가 범위내에 있나요?
            {
                animator.SetBool("isPlayer", true); //변신 하세요.
                before.enabled = true; //변신 후 콜라이더 켜세요.
                after.enabled = false; //변신 전 콜라이더 꺼요.
                afterRange.enabled = false; //변신 전 범위 콜라이더 꺼요.
                Chase(); //움직임을 플레이어를 따라가게 합시다.
            }
            else //플레이어가 범위내에 없어요.
            {
                Move(); //랜덤으로 움직이세요.
            }

            if (enemyHp.hp <= 0)
            {
                animator.SetBool("die", true);
                StartCoroutine(die());
            }
        }
    }

    private void ProduceItem()
    {
        GameObject bullet = Instantiate(bulletCharge);
        bullet.transform.position = transform.position - new Vector3(0, 1.2f, 0);
    }

    IEnumerator MovePosition() //변신 전의 적의 움직임을 정할 수를 랜덤으로 3초 마다 정하기
    {
        while (true) //이 안의 것을 계속(무한) 반복 하세요.
        {
            decision = Random.Range(0, 5); //0~4까지의 수를 랜덤으로 정하세요.
            yield return new WaitForSeconds(3f); //3초 기다리세요.
        }
    }

    private IEnumerator GetOut()
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);
        animator.SetFloat("chase", 1f);
        moveDir += new Vector3(1f, 0f, 0f);
        transform.position += moveDir * speed * Time.deltaTime;

        yield return new WaitForSeconds(5f);

        Move();
    }

    private void Move() //변신 전 적을 랜덤 수를 기반으로 움직이기
    {
        moveDir = Vector3.zero; //초기화
        switch (decision)
        {
            case 0: //정지
                animator.SetFloat("chase", 0f);
                break;
            case 1: //오른쪽
                transform.localScale = new Vector3(-1f, 1f, 1f); //회전 하세요. 안그럼 뒤로 가는 걸로 보여요.
                animator.SetFloat("chase", 1f);
                moveDir += new Vector3(1f, 0f, 0f);
                break;
            case 2: //왼쪽
                transform.localScale = new Vector3(1f, 1f, 1f); //회전 하세요. 안그럼 뒤로 가는 걸로 보여요.
                animator.SetFloat("chase", 1f);
                moveDir += new Vector3(-1f, 0f, 0f);
                break;
            case 3: //위로
                moveDir += new Vector3(0f, 1f, 0f);
                break;
            case 4: //아래로
                moveDir += new Vector3(0f, -1f, 0f);
                break;
        }
        transform.position += moveDir * speed * Time.deltaTime; //위에 것을 바탕으로 움직입시다.
    }

    private void Chase() //추적
    {
        myPosition = transform.position; //적(자신의) 위치
        playerPosition = player.transform.position; //플레이어 위치
        transform.position = Vector3.Lerp(myPosition, playerPosition, 0.002f); //적 위치에서 플레이어 위치로 움직이기
    }

    private void OnTriggerEnter2D(Collider2D collision) //플레이어가 범위안에 있는지 확인
    {
        if (collision.CompareTag("Player"))
            isplayer = true; //플레이어가 범위내에 있어요!
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("noEnemy4Zone"))
        {
            isplayer = false;
            GetOut();
        }
        if (collision.gameObject.CompareTag("bullet"))
        {
            enemyHp.hp -= StartUI.instance.isHard ? 3 : 3.5f; ;
            enemyHp.AnimationStart();
        }
        if (collision.gameObject.CompareTag("bliveru"))
        {
            BliveruControl.instance.enemyMinusHp = StartUI.instance.isHard ? 3 : 3.5f; ;
        }
    }

    private IEnumerator die() //죽는 거
    {
        yield return new WaitForSeconds(2f);
        ProduceItem();
        Destroy(gameObject);
    }
}
