using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class StoryStep : MonoBehaviour
{
    [System.Serializable]
    public class Choice
    {
        public string name;
        public GameObject nextStep;
    }

    public Choice[] choices;
    public Sprite sprite;
    public AudioClip[] clipsToPlay;
    public Sprite extraSprite;

    [HideInInspector] public Image img;
    [HideInInspector] public Transform btnHolder;
    [HideInInspector] public CanvasGroup imgGroup, btnGroup;

    //gamestate rules
    static bool canCross;
    static bool didWalkDog;

    bool fadeOut;
    const float fadeSpeed = 1f;
    const float fadeDur = 1 / fadeSpeed;

    const float blurSizeMin = 3, blurSizeMax = 4.8f;
    const int blurItersMin = 1, blurItersMax = 3;
    int currDrunkLvl = 0;

    void Awake()
    {
        if(!img)
        {
            img = GameObject.Find("ImageHolder").transform.GetChild(0).GetComponent<Image>();
            btnHolder = GameObject.Find("BtnsHolder").transform;
            imgGroup = img.transform.parent.GetComponent<CanvasGroup>();
            btnGroup = btnHolder.GetComponent<CanvasGroup>();
            btnGroup.alpha = imgGroup.alpha = 0;
        }
    }

    void OnEnable()
    {
        if(name == "Dog")
        {
            img.sprite = didWalkDog ? sprite : extraSprite;
            img.GetComponent<AudioSource>().PlayOneShot(clipsToPlay[didWalkDog ? 0 : 1]);
        }
        else //normal gameplay
        {
            img.sprite = sprite;
            StartCoroutine(ScheduleMusic());
        }
        for(int i=0; i<choices.Length; i++)
        {
            Choice choice = choices[i];
            Button btn = btnHolder.GetChild(i).GetComponent<Button>();
            btn.gameObject.SetActive(true);
            btn.onClick.AddListener(delegate () {
                StartCoroutine(StartTransition(fadeDur, choice));
            });
            Text lbl = btn.GetComponentInChildren<Text>();
            lbl.text = choice.name;
        }
    }

    void Update()
    {
        if(fadeOut)
        {
            if(imgGroup.alpha > 0)
            {
                imgGroup.alpha -= fadeSpeed * Time.deltaTime;
                btnGroup.alpha -= fadeSpeed * Time.deltaTime;
            }
        }
        else if(imgGroup.alpha < 1)
        {
            imgGroup.alpha += fadeSpeed * Time.deltaTime;
            btnGroup.alpha += fadeSpeed * Time.deltaTime;
        }
    }

    void OnDisable()
    {
        if (!btnHolder)
            return;

        //cleaning after ourselves
        for(int i=0; i<btnHolder.childCount; i++)
            btnHolder.GetChild(i).gameObject.SetActive(false);
    }

    IEnumerator ScheduleMusic()
    {
        for (int i = 0; i < clipsToPlay.Length; i++)
        {
            img.GetComponent<AudioSource>().PlayOneShot(clipsToPlay[i]);
            yield return new WaitForSeconds(clipsToPlay[i].length);
        }
    }

    IEnumerator StartTransition(float fadeDelay, Choice choice)
    {
        fadeOut = true;
        //cleaning after ourselves
        for (int i = 0; i < btnHolder.childCount; i++)
        {
            Button btn = btnHolder.GetChild(i).GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
        }
        img.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(fadeDelay);

        //game state changes
        switch(choice.name)
        {
            case "Walk it":
                didWalkDog = true;
                break;
        }
        if (name == "Pub1" && choice.name != "Left One")
            currDrunkLvl++;
        if (name == "Pub2" && choice.name != "Light")
            currDrunkLvl++;
        if (name == "Pub3" && choice.name != "Left")
            currDrunkLvl++;
        BlurOptimized.Inst.blurSize = blurSizeMin + (blurSizeMax - blurSizeMin) * (currDrunkLvl / 3f);
        BlurOptimized.Inst.blurIterations = Mathf.RoundToInt(blurItersMin + (blurItersMax - blurItersMin) * (currDrunkLvl / 3f));

        //cleanup
        gameObject.SetActive(false);
        //init
        StoryStep nextStep = choice.nextStep.GetComponent<StoryStep>();
        nextStep.img = img;
        nextStep.btnHolder = btnHolder;
        nextStep.btnGroup = btnGroup;
        nextStep.imgGroup = imgGroup;
        //reveal
        choice.nextStep.SetActive(true);
    }
}
