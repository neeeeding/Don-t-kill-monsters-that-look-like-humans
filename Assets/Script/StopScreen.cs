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
    public int enemyPage = 1; //다른곳에서 존 만들 것
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
    private string[] meaningText = { "나가다", "정하다", "미래", "죽다", "생명", "고귀", "대가", "보통", "사람" };
    
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
    public string[] explanationText = { "얘 이름은 블리베루다. 날아다니는 소형 드론 같은 물체이고 총알 발사가 가능하다. 드릴 부품만 있다면 드릴 기능도 추가 할 수 있을텐데... ", "초코 소라 빵이다. 왜 땅에 떨어져 있는지는 모르겠지만, 먹으면 회복되는 기분이다.", "탄창이다. 총알이 떨어졌을 때 매우 필요한 물건이다. 총알이 3개씩 들어있다.", "드릴 부품이다. 블리베루에게 추가된 기능의 주 부품이다.", "발판이다. 옳지 않은것을 밟으면 즉사 한다. 잘 못 밟지 않도록 조심 하자", "보석으로 치장된 화려한 관이다. 안에는 꽃으로 가득하다.", "콩알 만한 것 부터 나 보다 큰 것도 있지만 놀랍게도 밤송이다. 이것도 괴물의 영향을 받은걸까?", "울타리다. 무언가를 가둘 수 있다.", "테트리스다. 적의 주변을 돌고 있고, 발사된 것은 밀어낼 수 있다."};

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
