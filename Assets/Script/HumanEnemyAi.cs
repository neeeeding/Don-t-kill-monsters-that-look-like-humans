using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanEnemyAi : MonoBehaviour
{
    private GameObject player; //�÷��̾�
    private Vector3 myPosition; //��(�ڽ�)�� ��ġ
    private Vector3 playerPosition; //�÷��̾��� ��ġ

    private Animator animator;
    private Hp enemyHp;
    private Taehan taehan;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyHp = GetComponent<Hp>();
        taehan = FindAnyObjectByType<Taehan>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
       if(Time.timeScale != 0)
        {
            Chase();
            animator.SetFloat("moveX", Input.GetAxis("Horizontal"));
            animator.SetFloat("moveY", Input.GetAxis("Vertical"));

            if (enemyHp.hp <= 0)
            {
                taehan.isDie = true;
                StartCoroutine(die());
            }
        }
    }
    private void Chase() //����
    {
        myPosition = transform.position; //��(�ڽ���) ��ġ
        playerPosition = player.transform.position; //�÷��̾� ��ġ
        transform.position = Vector3.Lerp(myPosition, playerPosition, 0.005f); //�� ��ġ���� �÷��̾� ��ġ�� �����̱�
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            enemyHp.hp -= StartUI.instance.isHard ? 0.5f : 1f;
            enemyHp.AnimationStart();
        }
        if (collision.gameObject.CompareTag("bliveru"))
        {
            BliveruControl.instance.enemyMinusHp = StartUI.instance.isHard ? 0.5f : 1f;
        }
        if (collision.gameObject.CompareTag("noEnemy4Zone"))
        {
            this.enabled = false;
        }
    }

    private IEnumerator die()
    {
        animator.SetBool("die", true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
