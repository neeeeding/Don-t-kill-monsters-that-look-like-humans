using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    [SerializeField] public List<GameObject> tetrisList = new List<GameObject>();
    public GameObject tetriss;

    public GameObject tetris1;
    public GameObject tetris2;
    public GameObject tetris3;
    public GameObject tetris4;
    public GameObject tetris5;
    public GameObject tetris6;
    public GameObject tetris7;

    private float time = 0;
    private float speed = 50;
    private int rand;
    private int fireTetris = 0;

    private Animator animator;
    
    public int puzzleCount = 7;

    public static Tetris instance;

    public GameObject breadPrefabs;
    private float xMax = -402.4f;
    private float xMin = -420.2f;
    private float yMax = 5.14f;
    private float yMin = -7.6f;

    private Hp enemyHp;
    public GameObject hp;

    private int clearPuzzle = 1;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (instance == null)
            instance = this;
        enemyHp = GetComponent<Hp>();
    }
    private void Start()
    {
        StartCoroutine(Fire());
        StartCoroutine(Bread());
        enemyHp.enabled = false;
        hp.SetActive(false);
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            Turn();
        }
        if(puzzleCount <= 0)
        {
            enemyHp.enabled = true;
            hp.SetActive(true);
            animator.SetBool("isSkill", false);//�� ������
            StopCoroutine(Fire());
        }

        if(enemyHp.hp <= 0)
        {
            Destroy(gameObject);
        }

    }
    private IEnumerator Fire()
    {
        while (true)
        {
            if(Time.timeScale != 0)
            {
                animator.SetBool("isSkill", true); //�յ�� �ִϸ��̼�
                yield return new WaitForSeconds(1f);//1�� ��ٸ��� (�ٷ� ��ȯ �ϸ� ��� �ؼ�)
                Selected();
                yield return new WaitForSeconds(1f);//1�� ��ٸ���
                animator.SetBool("isSkill", false);//�� ������
                yield return new WaitForSeconds(4f);//4�� ��ٸ��� (�ٷ� ��Ʈ���� ����� ����ؼ�)
                if (tetrisList[rand] != null)
                {
                    tetrisList[rand].SetActive(true);//���� ���� �ѷ� ��Ʈ���� �ٽ� ���̱�
                }
                fireTetris = 0;//��ȯ�� ��ȣ �ʱ�ȭ
                yield return new WaitForSeconds(2f);//2�� ��ٸ��� (������ �ð�)
            }
        }
    }

    private void Selected()
    {
        if (puzzleCount >0)
        {
            rand = Random.Range(0, 7);//���� �� ���ϱ�
            fireTetris = rand + 1;//TertrissSetActive�� (��Ʈ���� ������) ��ȯ �� ��ȣ
            if (tetrisList[rand] != null)
            {
                tetrisList[rand].SetActive(false);//���� ���� �ѷ� ��Ʈ���� �����
                TetrisSetActive(clearPuzzle);//��Ʈ�����¾�Ƽ�� ����(�ߺ� ����)
            }
            else
            {
                Selected();
            }
        }
    }

    private void TetrisSetActive(int clearPuzzle)
    {
        GameObject tertris;
        switch (fireTetris)
        {
            case 1:
                if(clearPuzzle %2 != 0)
                {
                    tertris = Instantiate(tetris1, gameObject.transform);
                    tertris.transform.position = transform.position;
                    tertris.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                break;
            case 2:
                if (clearPuzzle % 3 != 0)
                {
                    tertris = Instantiate(tetris2, gameObject.transform);
                    tertris.transform.position = transform.position;
                    tertris.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                break;
            case 3:
                if (clearPuzzle % 5 != 0)
                {
                    tertris = Instantiate(tetris3, gameObject.transform);
                    tertris.transform.position = transform.position;
                    tertris.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                break;
            case 4:
                if (clearPuzzle % 7 != 0)
                {
                    tertris = Instantiate(tetris4, gameObject.transform);
                    tertris.transform.position = transform.position;
                    tertris.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                break;
            case 5:
                if (clearPuzzle % 11 != 0)
                {
                    tertris = Instantiate(tetris5, gameObject.transform);
                    tertris.transform.position = transform.position;
                    tertris.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                break;
            case 6:
                if (clearPuzzle % 13 != 0)
                {
                    tertris = Instantiate(tetris6, gameObject.transform);
                    tertris.transform.position = transform.position;
                    tertris.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                break;
            case 7:
                if (clearPuzzle % 17 != 0)
                {
                    tertris = Instantiate(tetris7, gameObject.transform);
                    tertris.transform.position = transform.position;
                    tertris.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                break;
            default:
                break;
        }
    }

    private void Turn()
    {
        time = Time.deltaTime * speed;
        Quaternion angle = Quaternion.Euler(0, 0, time);
        tetriss.transform.rotation = tetriss.transform.rotation * angle;
    }

    private IEnumerator Bread() //������
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);

            for (int i = 0; i < 2; i++)
            {
                GameObject bread = Instantiate(breadPrefabs, gameObject.transform);
                bread.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
                bread.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemyHp.enabled)
        {
            if (collision.gameObject.CompareTag("bullet"))
            {
                enemyHp.hp -= StartUI.instance.mode ? 1 : 2;
                enemyHp.AnimationStart();
            }
            if (collision.gameObject.CompareTag("bliveru"))
            {
                BliveruControl.instance.enemyMinusHp = StartUI.instance.isHard ? 1 : 2 ;
            }
        }
    }

    public void ClearPuzzle(int Puzzle)
    {
        puzzleCount -= 1;

        switch (Puzzle)
        {
            case 1:
                Destroy(tetrisList[0]);
                clearPuzzle *= 2;
                break;
            case 2:
                Destroy(tetrisList[1]);
                clearPuzzle *= 3;
                break;
            case 3:
                Destroy(tetrisList[2]);
                clearPuzzle *= 5;
                break;
            case 4:
                Destroy(tetrisList[3]);
                clearPuzzle *= 7;
                break;
            case 5:
                Destroy(tetrisList[4]);
                clearPuzzle *= 11;
                break;
            case 6:
                Destroy(tetrisList[5]);
                clearPuzzle *= 13;
                break;
            case 7:
                Destroy(tetrisList[6]);
                clearPuzzle *= 17;
                break;
            default:
                break;
        }
    }
}
