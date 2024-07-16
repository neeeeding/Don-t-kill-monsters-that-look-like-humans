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
                detail = "�Ͼ� �Ӹ��� ����� ���� �ʾҳ�?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 2:
                detail = "�Ͼ� �Ӹ��� ���...? \n (��� ����.)";
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
                detail = "��...? �� ������ ���� ������� ���̳�?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 102:
                detail = "�ʵ� �����ΰ� �и��ϱ�.";
                StartCoroutine(Text());
                break;
            case 103:
                detail = "�׾��!!";
                StartCoroutine(Text());
                break;
            case 104:
                conversation.SetActive(false);
                bullet = Instantiate(bazooka);
                bullet.transform.position = transform.position;
                break;
            case 201:
                detail = "Ȥ��...�Ӹ��� �Դ޸� �̻��� ����̿�?";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 202:
                detail = "�׸� �����µ��� ��� �־�?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 203:
                detail = "��... �����̳�? ���� ��ģ �Ƕ��̰� �ΰ��� ����� ������ �������...";
                StartCoroutine(Text());
                break;
            case 204:
                detail = "��? �װ� ����... �� ����Դϴ�.";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 205:
                detail = "���� ����ϰԵ� �������.";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 206:
                detail = "�׸� �׾��.";
                StartCoroutine(Text());
                break;
            case 207:
                conversation.SetActive(false);
                bullet = Instantiate(bazooka);
                bullet.transform.position = transform.position;
                break;
            case 301:
                detail = "��¦�� �ִ� �����̿�?";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 302:
                detail = "��, ������ �׿���?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 303:
                detail = "�� �����̶� �׿����ϴ�.";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 304:
                detail = "......�״� ����̴�. �� ����� �׿���.";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 305:
                detail = "��? �װ� �и� �����̾����!";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 306:
                detail = "�Ǻΰ� ���ΰ������� �������̾��� �� �i�ƿ� ���� �ߴٰ��.";
                StartCoroutine(Text());
                break;
            case 307:
                detail = "��������. �������� �� ����.";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 308:
                detail = "�ʴ� ������ ��ġ�� ���� �ΰ��̱�.";
                StartCoroutine(Text());
                break;
            case 309:
                detail = "���� ����! �װ� ���� ��ü�� ȸ���ؾ� �ϴ�.";
                StartCoroutine(Text());
                break;
            case 310:
                detail = "������� ���δ�.";
                StartCoroutine(Text());
                break;
            case 311:
                detail = "��Ű���!";
                StartCoroutine(Text());
                conversation.SetActive(false);
                isTrigger.enabled = true;
                isMove = true;
                break;
            case 401:
                detail = "��¦�� �ִ� �����̿�?";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 402:
                detail = "��, ������ �׿���?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 403:
                detail = "�ƴ� ��Ÿ���� ���׽��ϴ�.";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 404:
                detail = "�׷�, �� ��Ÿ���� ���...?";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 405:
                detail = "���ʿ� �ֽ��ϴ�.";
                StartCoroutine(Text());
                taehan.SetActive(false);
                break;
            case 406:
                detail = "�����̾�... �״� �� �����̰ŵ�.";
                StartCoroutine(Text());
                taehan.SetActive(true);
                break;
            case 407:
                detail = "���� ���� ��� �ִ� �����ִ�. ���Ѵٸ� ����Ͷ�.";
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
