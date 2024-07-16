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
                detail = "아직도 사람의 형태를 하다니, 괘씸하구나.";
                StartCoroutine(Text());
                isis.SetActive(true);
                break;
            case 2:
                detail = "머리에 뿔...사람이 아니야...?";
                StartCoroutine(Text());
                isis.SetActive(false);
                break;
            case 3:
                detail = "얘야, 이리 나와 보렴. 네 친구를 만들 시간이란다.";
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
                detail = "이...귀여운 아이를 죽이다니... 안타깝구나,안타까워..";
                StartCoroutine(Text());
                isis.SetActive(true);
                break;
            case 6:
                detail = "너에게도 영생의 축복을 내려주려 했건만...";
                StartCoroutine(Text());
                break;
            case 7:
                detail = "이리 거부하니 지금은 다시 권유해도 소용이 없겠구나.";
                StartCoroutine(Text());
                break;
            case 8:
                detail = "다음에 또 볼 수 있기를.";
                StartCoroutine(Text());
                break;
            case 9:
                detail = "아, 나가는 길은 저쪽이란다.";
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
