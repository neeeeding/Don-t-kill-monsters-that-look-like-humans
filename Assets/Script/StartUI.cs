using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartUI : MonoBehaviour
{
    public Image ui;
    private Color uiColor;
    private float time;

    public GameObject startui;

    public GameObject lie;
    public GameObject hajun;
    public GameObject chimera;
    public GameObject blackhole;
    public GameObject isis;
    public GameObject rabbit;
    public GameObject fire;
    private StopScreen stopScreen;

    public GameObject mode;

    public static StartUI instance;
    public bool isHard;

    public BliveruMove bliveru;

    private void Awake()
    {
        SetActiveFalse();
        stopScreen = FindAnyObjectByType<StopScreen>();
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        StartCoroutine(Character());
        uiColor = ui.color;
        StartCoroutine(Fade());
        mode.SetActive(false);
        bliveru.isColltime = true;
    }

    private void Update()
    {
        Time.timeScale = 0;
    }

    private void SetActiveFalse()
    {
        lie.SetActive(false);
        hajun.SetActive(false);
        chimera.SetActive(false);
        blackhole.SetActive(false);
        isis.SetActive(false);
        rabbit.SetActive(false);
        fire.SetActive(false);
    }

    private IEnumerator Character()
    {
        GameObject nowCharacter;
        int rand;
        while (true)
        {
            rand = Random.Range(0, 7);
            switch (rand)
            {
                case 0:
                    nowCharacter = lie;

                    break;
                case 1:
                    nowCharacter = hajun;
                    break;
                case 2:
                    nowCharacter = chimera;
                    break;
                case 3:
                    nowCharacter = blackhole;
                    break;
                case 4:
                    nowCharacter = isis;
                    break;
                case 5:
                    nowCharacter = rabbit;
                    break;
                case 6:
                    nowCharacter = fire;
                    break;
                default:
                    nowCharacter = null;
                    break;
            }
            nowCharacter.SetActive(true);
            yield return new WaitForSeconds(5f);
            nowCharacter.SetActive(false);
        }
    }

    private IEnumerator Fade()
    {
        while (true)
        {
            yield return StartCoroutine(FadeIn());
            yield return StartCoroutine(FadeOut());
        }
    }
    private IEnumerator FadeIn()
    {
        time = 0;
        while (uiColor.a >= 0)
        {
            time += Time.deltaTime / 2f;
            uiColor.a = Mathf.Lerp(0, 0.7f, time);
            ui.color = uiColor;
            yield return null;
            if(uiColor.a >= 0.7f)
            {
                break;
            }
        }
    }
    
    private IEnumerator FadeOut()
    {
        time = 0;
        while (uiColor.a <= 0.7f)
        {
            time += Time.deltaTime / 2f;
            uiColor.a = Mathf.Lerp(0.7f, 0, time);
            ui.color = uiColor;
            yield return null;
            if(uiColor.a <= 0)
            {
                break;
            }
        }
    }

    public void StartButton()
    {
        mode.SetActive(true);
        startui.SetActive(false);
    }

    public void Hard()
    {
        Time.timeScale = 1;
        BulletUI.instance.isStart = true;
        stopScreen.isStart = true;
        isHard = true;
        mode.SetActive(false);
        bliveru.isColltime = false;
    }
    public void Easy()
    {
        Time.timeScale = 1;
        BulletUI.instance.isStart = true;
        stopScreen.isStart = true;
        isHard = false;
        mode.SetActive(false);
        StopScreen.instance.explanationText[4] = "최대 4번의 기회가 있다. 잘 못 밟지 않도록 조심하자.";
        bliveru.isColltime = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
