using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    public TMP_Text bulletCount;
    private bool uiEnabled = false;
    private BliveruMove bliveru;

    private float time;

    public Image alarm;
    public TMP_Text alarmText;
    private Color alarmColor;
    public bool isDrill = false;
    public bool isStart = false;
    public bool isTrap = false;
    public bool isBonus = false;

    public static BulletUI instance;

    private void Awake()
    {
        bliveru = FindAnyObjectByType<BliveruMove>();   
    }

    private void Start()
    {
        alarm.enabled = false;
        alarmText.enabled = false;
        alarmColor = alarm.color;
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.LeftAlt))
        {
            uiEnabled = uiEnabled == true ? false : true;
        }

        bulletCount.enabled = uiEnabled == true ? true : false;

        bulletCount.text = $"남은 총알 수 : {bliveru.canFire} 개";

        if (isStart || isDrill || isTrap || isBonus)
        {
            StartCoroutine(Alarm());
        }
    }

    private IEnumerator Alarm()
    {
        if (isStart)
        {
            alarmText.text = "Tab를 눌러  설정창을 확인하실수 있습니다.";
        }
        if (isDrill)
        {
            alarmText.text = "Space를 눌러 드릴 기능을 사용하실수 있습니다.";
        }
        if (isTrap)
        {
            alarmText.text = "힌트 : 설정창 > 메모";
        }
        if (isBonus)
        {
            alarmText.text = "발사 되는 테트리스를 바닥의 테트리스와 맞춰 보스의 무적 상태를 해지 하실 수 있습니다.";
        }
        yield return isStart = false;
        yield return isDrill = false;
        yield return isTrap = false;
        yield return isBonus = false;
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        alarm.enabled = true;
        alarmText.enabled = true;
        time = 0;
        while (alarmColor.a >= 0)
        {
            time += Time.deltaTime / 2f;
            alarmColor.a = Mathf.Lerp(0, 1, time);
            alarm.color = alarmColor;
            yield return null;
            if (alarmColor.a >= 1f)
            {
                break;
            }
        }
    }

    private IEnumerator FadeOut()
    {
        time = 0;
        while (alarmColor.a <= 1)
        {
            time += Time.deltaTime / 2f;
            alarmColor.a = Mathf.Lerp(1, 0, time);
            alarm.color = alarmColor;
            yield return null;
            if (alarmColor.a <= 0)
            {
                alarm.enabled = false;
                alarmText.enabled = false;
                break;
            }
        }
    }
}
