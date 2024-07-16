using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraBattle : MonoBehaviour
{
    private Hp enemyHp;
    private GameObject player;

    public GameObject breadPrefabs;
    public GameObject enemyRabbit;
    public GameObject enemyTentacle;
    public GameObject Head;
    public Stack<GameObject> HeadPool = new Stack<GameObject>();
    public GameObject my;
    public GameObject storage;
    public GameObject stinger;
    public Stack<GameObject> stingerPool = new Stack<GameObject>();
    public Stack<GameObject> enemysPool = new Stack<GameObject>();

    private Isis isis;

    private Animator animator;

    private float xMax = -381.6f;
    private float xMin = -398.7f;
    private float yMax = -6.08f;
    private float yMin = -16.06f;
    private Vector3 myPosition;
    private Vector3 playerPosition;

    private bool isMove = false;
    public bool isRoll = false;
    private bool isStart = true;

    private void Awake()
    {
        enemyHp = GetComponent<Hp>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        isis = FindAnyObjectByType<Isis>();
    }

    private void Start()
    {
        StingerAneHaedKeep();
        StartCoroutine(Bread());
        StartCoroutine(PatternLoop());

    }

    private void Update()
    {
        AnimatorBool();
        if (isMove && stingerPool.Count>0) //움직이나?
        {
            StartCoroutine(StingerFire());
        }
    }

    private IEnumerator PatternLoop() // 패턴 루프
    {
        while (true)
        {
            yield return StartCoroutine(Pattern()); // 패턴 실행
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator Pattern() //패턴
    {
        if(Time.timeScale != 0)
        {
            if (isStart)
            {
                yield return new WaitForSeconds(Random.Range(1, 3));
                yield return StartCoroutine(FollowPlayer());
                yield return new WaitForSeconds(0.01f);
                yield return StartCoroutine(RollHaed());
                ProduceRabbit();
                isStart = false;
            }
            if (!isRoll)
            {
                ProduceTentacle();
                yield return new WaitForSeconds(3f);
                isStart = true;
            }
        }
    }

    private IEnumerator RollHaed() //머리 굴리기
    {
        if (HeadPool.Count > 0)
        {
            isRoll = true;
            yield return new WaitForSeconds(1f);
            GameObject headObject = HeadPool.Pop();
            headObject.transform.position = transform.position + new Vector3(0.1508789f, -0.04704094f, 0);
            headObject.SetActive(true);
        }
    }

    private IEnumerator FollowPlayer() //플레이어 따라다니기
    {
        isMove = true;
        float followDuration = 3f;
        float startTime = Time.time;

        while (Time.time < startTime + followDuration)
        {
            myPosition = transform.position;
            playerPosition = player.transform.position;

            transform.position = Vector3.Lerp(myPosition, playerPosition, 0.005f);
            yield return null;
        }
        isMove = false;
    }

    private void StingerAneHaedKeep() //독침,머리 저장
    {
        for(int i = 0; i<2; i++)
        {
            GameObject tentacle = Instantiate(stinger, storage.transform);
            stingerPool.Push(tentacle);
            tentacle.SetActive(false);
        }
        GameObject headObject = Instantiate(Head, storage.transform);
        HeadPool.Push(headObject);
        headObject.SetActive(false);
    }
    
    private IEnumerator StingerFire() //독침 생성
    {
        yield return new WaitForSeconds(0.7f);
        if(stingerPool.Count > 0)
        {
            GameObject tentacle = stingerPool.Pop();
            tentacle.SetActive(true);
            tentacle.transform.position = my.transform.position;
        }
    }
    
    private void ProduceTentacle() //촉수 생성
    {
        while (enemysPool.Count > 0)
        {
            GameObject enemy = enemysPool.Pop();
            Destroy(enemy);
        }
        for (int i = 0; i < 5; i++)
        {
            GameObject tentacle = Instantiate(enemyTentacle, storage.transform);
            tentacle.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
            enemysPool.Push(tentacle);
        }
    }

    private void ProduceRabbit() //토끼 2~3마리 생성
    {
        while (enemysPool.Count > 0)
        {
            GameObject enemy = enemysPool.Pop();
            Destroy(enemy);
        }
        for (int i = 0; i<Random.Range(2,4); i++)
        {
            GameObject rabbit = Instantiate(enemyRabbit, storage.transform);
            rabbit.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
            enemysPool.Push(rabbit);
        }
    }

    private IEnumerator Bread() //빵생성
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);

            for (int i = 0; i < 2; i++)
            {
                GameObject bread = Instantiate(breadPrefabs, storage.transform);
                bread.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
            }
        }
    }

    private void AnimatorBool()
    {
        animator.SetBool("isMove", isMove);
        animator.SetBool("isDie", enemyHp.hp <= 0 ? true : false);
        animator.SetBool("isSkill", isRoll);
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        if (enemyHp.hp <= 0)
        {
            yield return new WaitForSeconds(2f);
            isis.isDie = true;
            Destroy(storage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            enemyHp.hp -= StartUI.instance.isHard ? 0.1f : 0.5f; ;
            enemyHp.AnimationStart();
        }
        if (collision.gameObject.CompareTag("bliveru"))
        {
            BliveruControl.instance.enemyMinusHp = StartUI.instance.isHard ? 0.1f : 0.5f; ;
        }
    }
}
