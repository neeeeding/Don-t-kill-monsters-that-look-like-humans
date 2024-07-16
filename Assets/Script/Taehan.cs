using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Taehan : MonoBehaviour
{
    private Animator animator;
    public Collider2D isTrigger;
    public GameObject conversation;
    public GameObject taehan;
    public TMP_Text text;
    private string detail;

    public GameObject select;
    public GameObject next;

    public GameObject bazooka;

    public BliveruMove bliveru;
    public Collider2D tree;

    private int page = 1;

    public bool isDie = false;
    public bool isMove = false;

    private Vector3 moveDir = new Vector3();
    private float speed = 0.02f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        conversation.SetActive(false);
        select.SetActive(false);
    }

    private void Update()
    {
        if (isMove)
        {
            float moveX = page < 400 ? 1 : -1;
            transform.localScale = new Vector3(moveX, 1f, 1f);
            Walk(moveX);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTrigger.enabled = false;
            conversation.SetActive(true);
            select.SetActive(false);
            BliveruStop();
            if (isMove)
            {
                GameObject bullet = Instantiate(bazooka);
                bullet.transform.position = transform.position;
                isTrigger.enabled = false;
            }
        }
    }

    public void Next()
    {
        page++;
        BliveruStop();
        GameObject bullet;
        switch (page)
        {
            case 1:
                detail = "하얀 머리의 사람을 보지 않았나?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 2:
                detail = "하얀 머리의 사람...? \n (골라 보자.)";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 3:
                detail = "";
                StartCoroutine(Text());
                select.SetActive(true);
                next.SetActive(false);
                break;
            case 101:
                detail = "뭐...? 네 눈에는 저게 사람으로 보이나?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 102:
                detail = "너도 괴물인게 분명하군.";
                StartCoroutine(Text());
                break;
            case 103:
                detail = "죽어라!!";
                StartCoroutine(Text());
                break;
            case 104:
                conversation.SetActive(false);
                bullet = Instantiate(bazooka);
                bullet.transform.position = transform.position;
                break;
            case 201:
                detail = "혹시...머리에 뿔달린 이상한 사람이요?";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 202:
                detail = "그를 만났는데도 살아 있어?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 203:
                detail = "너... 괴물이냐? 드디어 미친 또라이가 인간과 비슷한 괴물을 만들었군...";
                StartCoroutine(Text());
                break;
            case 204:
                detail = "예? 그게 무슨... 전 사람입니다.";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 205:
                detail = "정말 비슷하게도 만들었군.";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 206:
                detail = "그만 죽어라.";
                StartCoroutine(Text());
                break;
            case 207:
                conversation.SetActive(false);
                bullet = Instantiate(bazooka);
                bullet.transform.position = transform.position;
                break;
            case 301:
                detail = "관짝에 있던 괴물이요?";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 302:
                detail = "그, 괴물을 죽였나?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 303:
                detail = "네 괴물이라 죽였습니다.";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 304:
                detail = "......그는 사람이다. 넌 사람을 죽였어.";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 305:
                detail = "예? 그건 분명 괴물이었어요!";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 306:
                detail = "피부가 비인간적으로 붉은색이었고 절 쫒아와 공격 했다고요.";
                StartCoroutine(Text());
                break;
            case 307:
                detail = "조용히해. 살인자의 말 따위.";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 308:
                detail = "너는 데려갈 가치도 없는 인간이군.";
                StartCoroutine(Text());
                break;
            case 309:
                detail = "저리 비켜! 네가 죽인 시체를 회수해야 하니.";
                StartCoroutine(Text());
                break;
            case 310:
                detail = "따라오면 죽인다.";
                StartCoroutine(Text());
                break;
            case 311:
                detail = "비키라고!";
                StartCoroutine(Text());
                conversation.SetActive(false);
                isTrigger.enabled = true;
                isMove = true;
                break;
            case 401:
                detail = "관짝에 있던 괴물이요?";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 402:
                detail = "그, 괴물을 죽였나?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 403:
                detail = "아뇨 울타리에 가뒀습니다.";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 404:
                detail = "그럼, 그 울타리는 어디에...?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 405:
                detail = "저쪽에 있습니다.";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 406:
                detail = "다행이야... 그는 내 동생이거든.";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 407:
                detail = "지금 내가 살고 있는 곳이있다. 원한다면 따라와라.";
                StartCoroutine(Text());
                break;
            case 408:
                conversation.SetActive(false);
                isMove = true;
                tree.enabled = false;
                break;
        }
    }
    private IEnumerator Text()
    {
        for (int i = 0; i <= detail.Length; i++)
        {
            text.text = detail.Substring(0, i);
            yield return new WaitForSeconds(0.08f);
        }
    }

    public void Rabbit()
    {
        select.SetActive(false);
        next.SetActive(true);
        page = 100;
        Next();
    }

    public void Isis()
    {
        select.SetActive(false);
        next.SetActive(true);
        page = 200;
        Next();
    }

    public void Hajun()
    {
        select.SetActive(false);
        next.SetActive(true);
        page = isDie? 300: 400;
        Next();
    }

    private void Walk(float moveX)
    {
        animator.SetBool("isMove", true);
        moveDir += new Vector3(moveX, 0, 0);
        transform.position += moveDir * speed * Time.deltaTime;
        if (transform.position.x >= -404 || transform.position.x <= -472)
        {
            moveDir = new Vector3(0, 0, 0);
            animator.SetBool("isMove", false);
        }
    }

    private void BliveruStop()
    {
        bliveru.isColltime = true;
    }
}
