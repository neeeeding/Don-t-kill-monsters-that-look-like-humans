using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public float hp = 10; //ü�� ��
    private bool animationenabled; //ü�¹� ���̱� �Ⱥ��̱� ���ϴ� �ο�
    private bool dont; //ü�¹ٸ� ���̰� �صξ��°�?

    private Animator animator; //ü�¹� �ִϸ��̼�

    private SpriteRenderer hpgauge; //ü�¹� �̹���

    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        hpgauge = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.LeftAlt)) //altŰ ��������?
        {
            if (animationenabled == true) //ü�¹ٰ� ���� ������?
            {
                animationenabled = false; //��. �׷��� �̹��� ������.
            }
            else
            {
                animationenabled = true; //�ƴ�. �׷��� ���� ���̰� �Ϸ�����.
            }
        }

        if (animationenabled == true) //ü�¹� ���̰� �ص׳���?
        {
            hpgauge.enabled = true; //���Դϴ�.
        }
        else //ü�¹� �� ���̰� �ص���?
        {
            hpgauge.enabled = false; //�� �Ⱥ�����.
        }

        if (hp >= 10)
            hp = 10; //hp 10�� �ѱ��� ������.

        animator.SetFloat("hp", hp); //�ִϸ��̼� ���� hp�� ���� hp�� ������ �����ϼ���.
    }

    public void AnimationStart() //�ִϸ��̼� ���� �սô�.
    {
        StartCoroutine(Animationtime());
    }

    public IEnumerator Animationtime()
    {
        dont = false; //ü�¹� ���̰� ���� ���ٰ� Ĩ�ô�.
        if (animationenabled == false) //ü�¹� �� ���̰� �ص׳���?
            animationenabled = true; //��. ���� ���̰� �ҰԿ�.
        else //��� ü�¹� �ѵ״µ���?
            dont = true; //�׷� ü�¹� ���̰� �ص׾��ٰ� ����սô�.
        
        yield return new WaitForSeconds(0.2f); //1�� ��ٸ��Կ�.

        if (dont == false) //ü�¹ٸ� �̰� �������� �� ���̰� �ص׾��?
            animationenabled = false; //��. �׷��� �ٽ� ü�¹� �� ���̰� �ҰԿ�.
    }
}
