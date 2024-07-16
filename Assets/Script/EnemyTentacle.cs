using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTentacle : MonoBehaviour
{
    private Animator animator;
    private Hp enemyHp;

    public GameObject enforce;
    public GameObject pageAdd;

    private GameObject obstruct;

    public bool isKing;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyHp = GetComponent<Hp>();
    }

    private void Start()
    {
        obstruct = GameObject.Find("cantTree");
    }
    private void Update()
    {
        if (enemyHp.hp <= 0)
        {
            animator.SetBool("die", true);
            StartCoroutine(die());
        }
    }
    private IEnumerator die() 
    {
        yield return new WaitForSeconds(2f);
        if(isKing == true)
        {
            ProduceItem();
            Destroy(obstruct);
        }
        Destroy(gameObject);
    }
    private void ProduceItem()
    {
        GameObject item = Instantiate(enforce);
        item.transform.position = transform.position;
        item = Instantiate(pageAdd);
        item.transform.position = transform.position + new Vector3(-0.5572909f, -0.9059667f, 0); ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            enemyHp.hp -= isKing ? (StartUI.instance.mode ? 1 : 2): (StartUI.instance.isHard ? 2 : 3);
            enemyHp.AnimationStart();
        }
        if (collision.gameObject.CompareTag("bliveru"))
        {
            BliveruControl.instance.enemyMinusHp = isKing ? (StartUI.instance.isHard ? 1 : 2) : (StartUI.instance.isHard ? 2 : 3);
        }
    }
}
