using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BliveruControl : MonoBehaviour
{
    private float x;
    private float y;
    private float speed = 5f;

    private Vector3 moveDir;
    public Collider2D range;
    public Collider2D attack;
    public Collider2D real;
    private BliveruMove bliveru;
    private Animator animator;

    public GameObject lie;

    public float enemyMinusHp;

    public static BliveruControl instance;

    private void Awake()
    {
        bliveru = GetComponent<BliveruMove>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        attack.enabled = false;
        real.enabled = false;

        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (bliveru.drillCan)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                range.enabled = false;
                attack.enabled = true;
                real.enabled = true;
                bliveru.enabled = false;
                animator.SetBool("Isdrill", true);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                range.enabled = true;
                attack.enabled = false;
                real.enabled = false;
                bliveru.enabled = true;
                animator.SetBool("Isdrill", false);
                transform.position = new Vector3(lie.transform.position.x + 0.8f, lie.transform.position.y + 0.99f, 0);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                x = Input.GetAxis("Horizontal");
                y = Input.GetAxis("Vertical");
                moveDir = new Vector3(x, y, 0).normalized;
                transform.position += moveDir * speed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (range.enabled == false&& (collision.gameObject.CompareTag("chimera")  || collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("bonusBoss")))
        {
            Hp enemyHp = collision.gameObject.GetComponent<Hp>();
            enemyHp.hp -= enemyMinusHp;
            enemyHp.AnimationStart();
        }
    }
}
