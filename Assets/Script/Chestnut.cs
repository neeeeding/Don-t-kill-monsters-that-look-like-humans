using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestnut : MonoBehaviour
{
    private float z;
    private float speed = 100f;
    private float maxSize = 4;
    private float minSize = 1;

    private ChestnutSpawner spawner;

    private void Awake()
    {
        spawner = FindAnyObjectByType<ChestnutSpawner>();
    }
    private void Start()
    {
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, size);
        speed = StartUI.instance.isHard ? 100f : 70f;
    }

    private void Update()
    {
        z += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("noEnemy4Zone"))
        {
            spawner.cNList.Push(gameObject);
        }
    }
}
