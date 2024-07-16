using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lieMovement : MonoBehaviour
{
    public float speed = 4f; //������ �ӵ�
    private float x;
    private float y;
    private bool isSlow = false;

    private Vector2 moveDir;
    private Rigidbody2D rigid; //�÷��̾� ������ٵ�
    private Animator animator; //�÷��̾� �ִϸ��̼�
    private Hp hpScript; //���� �÷��̾ ������ �ִ� hp��ũ��Ʈ

    public GameObject dieScreen;

    //public AudioSource walkSound;
    //public AudioSource runSound;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hpScript = GetComponent<Hp>();
    }

    private void Update()
    {
        if (hpScript.hp <= 0) //ü���� 0���� �۾ƿ�?
        {
            animator.SetBool("die", true); //��. �״� �ִϸ��̼� ���� �ҰԿ�.
            animator.SetBool("move", false);
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 0);
            dieScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            animator.SetBool("die", false); //�ƴ�. ��� �����̰ڽ��ϴ�.
            dieScreen.SetActive(false);
            MoveInput(); //Ű���� �Է� �޾ƿ�.
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = moveDir.normalized * speed;//�÷��̾� ������
    }

    public void MoveInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(x, y);

        if (Input.GetKey(KeyCode.Space))
        {
            x = 0;
            y = 0;
            moveDir = new Vector2(0, 0);
        }

        MoveAnimation(); //�����̴� �ִϸ��̼� ���� �ϼ���.

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) //����Ʈ ��������?
        {
            speed = isSlow? 1f: 7f; //�ӵ� �÷���.
        }
        else
        {
            speed = isSlow ? 1f : 4f;
        }

    }

    private void MoveAnimation()
    {
        animator.SetFloat("moveX", x); //moveX���� �÷��̾��� ��ġ X���� �Է� ���� x�� �����ϰڽ��ϴ�.
        animator.SetFloat("moveY", y); //moveY���� �÷��̾��� ��ġ Y���� �Է� ���� y�� �����ϰڽ��ϴ�.
        if (x == 0 && y == 0) //x�� y�� ������ ������?
        { animator.SetBool("move", false);//��. �ȴ� �������� ����ô�.
        }
        else
        { animator.SetBool("move", true);//�����̰� ������ �����̴� �ִϸ��̼��� ���̰ڽ��ϴ�.
            //if (speed == 4)
            //{
            //    if (walkSound.isPlaying)
            //        walkSound.Stop();
            //    else
            //        walkSound.Play();
            //}
            //else if(speed == 7)
            //{
            //    if (runSound.isPlaying)
            //        runSound.Stop();
            //    else
            //        runSound.Play();
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy")) //���̶� ��ҳ���.
        {
            hpScript.hp--; //ü���� 1���� �մϴ�.

            hpScript.AnimationStart(); //ü�� �ִϸ��̼��� ���� ��Ű����.
        }
        if (collision.CompareTag("bread")) //���̶� ��ҳ���.
        {
            if (hpScript.hp > 0)
            {
                Destroy(collision.gameObject);
                hpScript.hp += 3; //ü���� 3���� �մϴ�.
                hpScript.AnimationStart(); //ü�� �ִϸ��̼��� ���� ��Ű����.
                StopScreen.instance.itemPage = StopScreen.instance.itemPage < 2 ? 2 : StopScreen.instance.itemPage;
            }
        }
        if (collision.gameObject.CompareTag("slowZone"))
        {
            isSlow = true;
        }
        if (collision.gameObject.CompareTag("chestnut") || collision.gameObject.CompareTag("chimera"))
        {
            hpScript.hp -= 3;
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("slowZone"))
        {
            isSlow = false;
        }
    }
}
