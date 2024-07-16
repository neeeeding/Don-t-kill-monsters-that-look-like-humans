using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private bool isStart = false;
    private BliveruMove bliveru;
    public GameObject lie;

    private void Awake()
    {
        bliveru = GameObject.FindGameObjectWithTag("bliveru").GetComponent<BliveruMove>();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!isStart && Input.GetMouseButtonDown(0))
        {
            bliveru.enabled = false;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == lie)
                {
                    Animator animator = hit.transform.gameObject.GetComponent<Animator>();
                    if (animator != null)
                    {
                        StartCoroutine(PlayerAnimator(animator));
                        isStart = true;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            bliveru.enabled = true;
        }
    }
    private IEnumerator PlayerAnimator(Animator animator)
    {
        animator.SetBool("isStart", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isStart", false);
    }
}
