using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Isis : MonoBehaviour
{
    private Animator animator;
    public Collider2D isTrigger;
    public Collider2D BoxCollider;
    public GameObject conversation;
    public GameObject isis;
    public TMP_Text text;
    private string detail;
    public GameObject chimera;

    public GameObject tree;
    public GameObject road;

    public BliveruMove bliveru;

    private bool isBattle = false;
    public bool isDie = false;

    private int page = 1;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        conversation.SetActive(false);
        BoxCollider.enabled = false;
        road.SetActive(false);
    }

    private void Update()
    {
        if(isBattle && isDie)
        {
            isTrigger.enabled = false;
            conversation.SetActive(true);
            Next();
            isBattle = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTrigger.enabled = false;
            conversation.SetActive(true);
            BliveruStop();
        }
    }

    public void Next()
    {
        page++;
        BliveruStop();

        switch (page)
        {
            case 1:
                detail = "������ ����� ���¸� �ϴٴ�, �����ϱ���.";
                StartCoroutine(Text());
                isis.SetActive(true);
                break;
            case 2:
                detail = "�Ӹ��� ��...����� �ƴϾ�...?";
                StartCoroutine(Text());
                isis.SetActive(false);
                break;
            case 3:
                detail = "���, �̸� ���� ����. �� ģ���� ���� �ð��̶���.";
                StartCoroutine(Text());
                isis.SetActive(true);
                break;
            case 4:
                isis.SetActive(true);
                animator.SetBool("isCane", true);
                conversation.SetActive(false);
                GameObject enemy = Instantiate(chimera);
                enemy.transform.position = transform.position;
                BoxCollider.enabled = true;
                isBattle = true;
                StopScreen.instance.enemyPage+=2;
                break;
            case 5:
                detail = "��...�Ϳ��� ���̸� ���̴ٴ�... ��Ÿ������,��Ÿ���..";
                StartCoroutine(Text());
                isis.SetActive(true);
                break;
            case 6:
                detail = "�ʿ��Ե� ������ �ູ�� �����ַ� �߰Ǹ�...";
                StartCoroutine(Text());
                break;
            case 7:
                detail = "�̸� �ź��ϴ� ������ �ٽ� �����ص� �ҿ��� ���ڱ���.";
                StartCoroutine(Text());
                break;
            case 8:
                detail = "������ �� �� �� �ֱ⸦.";
                StartCoroutine(Text());
                break;
            case 9:
                detail = "��, ������ ���� �����̶���.";
                StartCoroutine(Text());
                break;
            case 10:
                road.SetActive(true);
                conversation.SetActive(false);
                Destroy(tree);
                Destroy(gameObject);
                break;

        }
    }

    private IEnumerator Text()
    {
        for(int i = 0; i<= detail.Length; i++)
        {
            text.text = detail.Substring(0, i);
            yield return new WaitForSeconds(0.08f);
        }
    }

    private void BliveruStop()
    {
        bliveru.isColltime = page == 4 || page >= 10 ? false : true;
    }
}
