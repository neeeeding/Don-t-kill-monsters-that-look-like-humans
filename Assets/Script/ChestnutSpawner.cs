using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestnutSpawner : MonoBehaviour
{
    public GameObject chestnutPrefabs;
    public GameObject chestnuts;
    private float x;
    private bool isPlayer = false;

    public Stack<GameObject> cNList = new Stack<GameObject>();

    private void Start()
    {
        storage();
    }

    private void Update()
    {
        if (isPlayer && cNList.Count > 0)
        {
            x = Random.Range(5, 87);
            GameObject chestnut = cNList.Pop();
            chestnut.SetActive(true);
            chestnut.transform.position = new Vector3(transform.position.x - x, 8, 0);
        }
    }

    private void storage()
    {
        for(int i = 0; i< 18; i++)
        {
            GameObject chestnut = Instantiate(chestnutPrefabs, chestnuts.transform);
            cNList.Push(chestnut);
            chestnut.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
