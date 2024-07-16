using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public int num;
    private SpriteRenderer color;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tertris"))
        {
            PuzzleTertris tertrisNum = collision.GetComponent<PuzzleTertris>();
            if(tertrisNum.num == num)
            {
                Tetris.instance.ClearPuzzle(num);
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    color = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
                    color.color = new Color(1, 1, 1, 1);
                }
                Destroy(this);
            }
        }
    }
}
