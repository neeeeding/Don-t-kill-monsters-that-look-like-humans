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

        bulletCount.text = $"���� �Ѿ� �� : {bliveru.canFire} ��";

        if (isStart || isDrill || isTrap || isBonus)
        {
            StartCoroutine(Alarm());
        }
    }

    private IEnumerator Alarm()
    {
        if (isStart)
        {
            alarmText.text = "Tab�� ����  ����â�� Ȯ���ϽǼ� �ֽ��ϴ�.";
        }
        if (isDrill)
        {
            alarmText.text = "Space�� ���� �帱 ����� ����ϽǼ� �ֽ��ϴ�.";
        }
        if (isTrap)
        {
            alarmText.text = "��Ʈ : ����â > �޸�";
        }
        if (isBonus)
        {
            alarmText.text = "�߻� �Ǵ� ��Ʈ������ �ٴ��� ��Ʈ������ ���� ������ ���� ���¸� ���� �Ͻ� �� �ֽ��ϴ�.";
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
