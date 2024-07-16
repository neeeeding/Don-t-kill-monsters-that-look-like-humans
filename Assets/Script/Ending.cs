using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ending : MonoBehaviour
{
    public Image ui;
    public GameObject Buttons;
    public TMP_Text endText;
    private string text = "End";
    private float time;
    private Color uiColor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeIn());
            StartCoroutine(Text());
        }
    }

    private void Start()
    {
        ui.enabled = false;
        Buttons.SetActive(false);
        uiColor = ui.color;
        endText.enabled = false;
    }

    private IEnumerator FadeIn()
    {
        ui.enabled = true;
        while (uiColor.a > 0)
        {
            time += Time.deltaTime / 2f;
            uiColor.a = Mathf.Lerp(0, 1, time);
            ui.color = uiColor;
            yield return null;
        }
    }
    private IEnumerator Text()
    {
        endText.enabled = true;
        for(int i = 0; i<=text.Length; i++)
        {
            endText.text = text.Substring(0, i);
            yield return new WaitForSeconds(1f);
        }
        Buttons.SetActive(true);
    }
}
