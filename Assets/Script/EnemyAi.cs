using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private float decision; //���� �ൿ�� ���� ����
    private bool isplayer = false; //�÷��̾ ���� ������ ���Դ��� Ȯ�� �ϴ� ����
    private float speed = 3f; //���� �ӵ�

    private Animator animator; //���� �ִϸ��̼�

    private GameObject player; //�÷��̾�
    private Vector3 myPosition; //��(�ڽ�)�� ��ġ
    private Vector3 playerPosition; //�÷��̾��� ��ġ
    private Vector3 moveDir;

    public Collider2D before; //���� �� �ݶ��̴�
    public Collider2D after; //���� �� �ݶ��̴�
    public Collider2D afterRange; //���� �� ���� ����(�ݶ��̴�)

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
        StartCoroutine("MovePosition"); //���� ���� ���� �������� ���� ���� �������� 3�� ���� ���ϼ���.
        before.enabled = false; //���� �������� ���� ���� �ݶ��̴��� ��Ȱ��ȭ �ҰԿ�.
    }

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            if (isplayer == true) //�÷��̾ �������� �ֳ���?
            {
                animator.SetBool("isPlayer", true); //���� �ϼ���.
                before.enabled = true; //���� �� �ݶ��̴� �Ѽ���.
                after.enabled = false; //���� �� �ݶ��̴� ����.
                afterRange.enabled = false; //���� �� ���� �ݶ��̴� ����.
                Chase(); //�������� �÷��̾ ���󰡰� �սô�.
            }
            else //�÷��̾ �������� �����.
            {
                Move(); //�������� �����̼���.
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

    IEnumerator MovePosition() //���� ���� ���� �������� ���� ���� �������� 3�� ���� ���ϱ�
    {
        while (true) //�� ���� ���� ���(����) �ݺ� �ϼ���.
        {
            decision = Random.Range(0, 5); //0~4������ ���� �������� ���ϼ���.
            yield return new WaitForSeconds(3f); //3�� ��ٸ�����.
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

    private void Move() //���� �� ���� ���� ���� ������� �����̱�
    {
        moveDir = Vector3.zero; //�ʱ�ȭ
        switch (decision)
        {
            case 0: //����
                animator.SetFloat("chase", 0f);
                break;
            case 1: //������
                transform.localScale = new Vector3(-1f, 1f, 1f); //ȸ�� �ϼ���. �ȱ׷� �ڷ� ���� �ɷ� ������.
                animator.SetFloat("chase", 1f);
                moveDir += new Vector3(1f, 0f, 0f);
                break;
            case 2: //����
                transform.localScale = new Vector3(1f, 1f, 1f); //ȸ�� �ϼ���. �ȱ׷� �ڷ� ���� �ɷ� ������.
                animator.SetFloat("chase", 1f);
                moveDir += new Vector3(-1f, 0f, 0f);
                break;
            case 3: //����
                moveDir += new Vector3(0f, 1f, 0f);
                break;
            case 4: //�Ʒ���
                moveDir += new Vector3(0f, -1f, 0f);
                break;
        }
        transform.position += moveDir * speed * Time.deltaTime; //���� ���� �������� �����Խô�.
    }

    private void Chase() //����
    {
        myPosition = transform.position; //��(�ڽ���) ��ġ
        playerPosition = player.transform.position; //�÷��̾� ��ġ
        transform.position = Vector3.Lerp(myPosition, playerPosition, 0.002f); //�� ��ġ���� �÷��̾� ��ġ�� �����̱�
    }

    private void OnTriggerEnter2D(Collider2D collision) //�÷��̾ �����ȿ� �ִ��� Ȯ��
    {
        if (collision.CompareTag("Player"))
            isplayer = true; //�÷��̾ �������� �־��!
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

    private IEnumerator die() //�״� ��
    {
        yield return new WaitForSeconds(2f);
        ProduceItem();
        Destroy(gameObject);
    }
}
