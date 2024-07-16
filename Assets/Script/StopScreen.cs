using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class StopScreen : MonoBehaviour
{
    public BliveruMove bliveru;
    private Image setting;
    private bool isSetting = false;
    public int enemyPage = 1; //�ٸ������� �� ���� ��
    public int itemPage = 1;
    private int memoPage = 20;
    private int page;

    private bool isEnemy = false;
    private bool isItem = false;
    private bool isSettingBook = false;
    private bool isKey = false;
    private bool isMemo = false;
    private bool isMain = false;

    public GameObject rabbit;
    public GameObject tentacle;
    public GameObject blackhole;
    public GameObject human;
    public GameObject chimera;
    public GameObject isis;
    public GameObject bouns;

    public GameObject keyPage;

    public GameObject enter;
    public GameObject decided;
    public GameObject future;
    public GameObject die;
    public GameObject life;
    public GameObject noble;
    public GameObject pay;
    public GameObject basic;
    public GameObject person;
    public GameObject proviso;
    public TMP_Text meaning;
    private string[] meaningText = { "������", "���ϴ�", "�̷�", "�״�", "����", "���", "�밡", "����", "���" };
    
    public GameObject bliveruItem;
    public GameObject bread;
    public GameObject magazine;
    public GameObject drill;
    public GameObject scaffolding;
    public GameObject coffin;
    public GameObject chestnut;
    public GameObject fence;
    public GameObject tertris;
    public TMP_Text explanation;
    public string[] explanationText = { "�� �̸��� �������. ���ƴٴϴ� ���� ��� ���� ��ü�̰� �Ѿ� �߻簡 �����ϴ�. �帱 ��ǰ�� �ִٸ� �帱 ��ɵ� �߰� �� �� �����ٵ�... ", "���� �Ҷ� ���̴�. �� ���� ������ �ִ����� �𸣰�����, ������ ȸ���Ǵ� ����̴�.", "źâ�̴�. �Ѿ��� �������� �� �ſ� �ʿ��� �����̴�. �Ѿ��� 3���� ����ִ�.", "�帱 ��ǰ�̴�. �����翡�� �߰��� ����� �� ��ǰ�̴�.", "�����̴�. ���� �������� ������ ��� �Ѵ�. �� �� ���� �ʵ��� ���� ����", "�������� ġ��� ȭ���� ���̴�. �ȿ��� ������ �����ϴ�.", "��� ���� �� ���� �� ���� ū �͵� ������ ����Ե� ����̴�. �̰͵� ������ ������ �����ɱ�?", "��Ÿ����. ���𰡸� ���� �� �ִ�.", "��Ʈ������. ���� �ֺ��� ���� �ְ�, �߻�� ���� �о �� �ִ�."};

    public GameObject home;
    public GameObject next;
    public GameObject previous;
    
    public GameObject bookmark;
    public GameObject key;
    public GameObject main;

    public AudioSource sound1;
    public AudioSource sound2;
    public AudioSource sound3;
    public AudioSource sound4;
    public AudioSource sound5;
    private List<AudioSource> soundList = new List<AudioSource>(5);
    private int NowPlaySound;
    public GameObject soundSetting;
    public TMP_Text nowplay;
    public bool isStart = false;

    public static StopScreen instance;

    private void Awake()
    {
        setting = GetComponent<Image>();
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        setting.enabled = false;
        Active();
        home.SetActive(false);
        next.SetActive(false);
        previous.SetActive(false);
        bookmark.SetActive(false);
        key.SetActive(false);
        main.SetActive(false);
        SoundPush();
    }

    private void Update()
    {
        if (isStart)
        {
            SoundPlay();
            isStart = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isSetting = !isSetting;
            isMain = isSetting;
            bliveru.isColltime = isSetting;
        }

        if (!isSetting)
        {
            isEnemy = false;
            isItem = false;
            isSettingBook = false;
            isKey = false;
            isMemo = false;
            isMain = false;
        }

        Active();

        setting.enabled = isSetting;
        Time.timeScale = isSetting ? 0 : 1;

        if (!isEnemy && !isItem && !isKey && !isSettingBook && !isMemo && !isMain)
        {
            home.SetActive(false);
            next.SetActive(false);
            previous.SetActive(false);
            bookmark.SetActive(false);
            key.SetActive(false);
            page = 0;
        }
        else
        {
            home.SetActive(true);
        }
    }

    public void Enemys()
    {
        isEnemy = true;
        isItem = false;
        isSettingBook = false;
        isKey = false;
        isMemo = false;
        isMain = false;
        page = 1;
    }

    public void Key()
    {
        isKey = true;
        isEnemy = false;
        isItem = false;
        isSettingBook = false;
        isMemo = false;
        isMain = false;
    }

    public void Item()
    {
        isEnemy = false;
        isItem = true;
        isSettingBook = false;
        isKey = false;
        isMemo = false;
        isMain = false;
        page = 1;
    }

    public void Memo()
    {
        isEnemy = false;
        isItem = false;
        isSettingBook = false;
        isKey = false;
        isMemo = true;
        isMain = false;
        page = 1;
    }

    public void Setting()
    {
        isEnemy = false;
        isItem = false;
        isSettingBook = true;
        isKey = false;
        isMemo = false;
        isMain = false;
    }

    public void Next()
    {
        page++;
        if (isEnemy)
        {
            page = page >= enemyPage ? enemyPage : page;
        }
        if (isItem)
        {
            page = page >= itemPage ? itemPage : page;
        }
        if (isMemo)
        {
            page = page >= memoPage ? memoPage : page;
        }
    }

    public void Previous()
    {
        page--;
        page = page <= 0 ? 1 : page;
    }

    public void Home()
    {
        StartCoroutine(BliveruStop());
        isSetting = false;
    }

    private IEnumerator BliveruStop()
    {
        yield return new WaitForSeconds(0.7f);
        bliveru.isColltime = false;
    }



    private void Active()
    {
        main.SetActive(isMain);

        rabbit.SetActive(page ==1 && isEnemy);
        tentacle.SetActive(page == 2 && isEnemy);
        blackhole.SetActive(page == 3 && isEnemy);
        human.SetActive(page == 4 && isEnemy);
        chimera.SetActive(page == 5 && isEnemy);
        isis.SetActive(page == 6 && isEnemy);
        bouns.SetActive(page == 7 && isEnemy);

        enter.SetActive(page == 1 && isMemo);
        decided.SetActive(page == 2 && isMemo);
        future.SetActive(page == 3 && isMemo);
        die.SetActive(page == 4 && isMemo);
        life.SetActive(page == 5 && isMemo);
        noble.SetActive(page == 6 && isMemo);
        pay.SetActive(page == 7 && isMemo);
        basic.SetActive(page == 8 && isMemo);
        person.SetActive(page == 9 && isMemo);
        proviso.SetActive(page == 19 && isMemo);
        meaning.enabled = (page <= 9 && isMemo);
        meaning.text = meaning.enabled? meaningText[page - 1] : "";

        bliveruItem.SetActive(page == 1 && isItem);
        bread.SetActive(page == 2 && isItem);
        magazine.SetActive(page == 3 && isItem);
        drill.SetActive(page == 4 && isItem);
        scaffolding.SetActive(page == 5 && isItem);
        coffin.SetActive(page == 6 && isItem);
        chestnut.SetActive(page == 7 && isItem);
        fence.SetActive(page == 8 && isItem);
        tertris.SetActive(page == 9 && isItem);
        explanation.enabled = (page <= 10 && isItem);
        explanation.text = explanation.enabled ? explanationText[page - 1] : "";

        next.SetActive(page != 0 && (page < enemyPage && isEnemy) || (page <memoPage && isMemo) || (page < itemPage && isItem));
        previous.SetActive(page > 1);

        soundSetting.SetActive(isSettingBook);
        nowplay.text = $"{NowPlaySound + 1}";

        bookmark.SetActive(true);
        key.SetActive(true);

        keyPage.SetActive(isKey);
    }

    public void SetLevel(float volume)
    {
        sound1.volume = volume;
        sound2.volume = volume;
        sound3.volume = volume;
        sound4.volume = volume;
        sound5.volume = volume;
    }

    private void SoundPlay()
    {
        soundList[NowPlaySound].Play();
    }
    public void SoundPlayPrevious()
    {
        soundList[NowPlaySound].Stop();
        soundList[NowPlaySound = NowPlaySound <= 0 ? 4 : --NowPlaySound].Play();
    }
    public void SoundPlayNext()
    {
        soundList[NowPlaySound].Stop();
        soundList[NowPlaySound = NowPlaySound >=4 ? 0 : ++NowPlaySound].Play();
    }

    private void SoundPush()
    {
        soundList.Add(sound1);
        soundList.Add(sound2);
        soundList.Add(sound3);
        soundList.Add(sound4);
        soundList.Add(sound5);
        sound1.Stop();
        sound2.Stop();
        sound3.Stop();
        sound4.Stop();
        sound5.Stop();
        NowPlaySound = 0;
    }
}
