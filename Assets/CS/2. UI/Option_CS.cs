using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option_CS : MonoBehaviour
{
    [SerializeField] bool SettingCheck;
    Animator anime;

    [SerializeField] GameObject SoundWindow;
    [SerializeField] GameObject SoundGraphic;

    [SerializeField] GameObject ALL_Button;

    public Slider All;
    public Slider BGM;
    public Slider SFX;

    private float deltaTime = 0f;

    [SerializeField, Range(1, 100)]
    private int size;
    [SerializeField] private Color color;
    public bool OnLabal;

    [Header("음소거 아이콘")]
    [SerializeField] GameObject BGM_On;
    [SerializeField] GameObject BGM_Off;

    [SerializeField] GameObject SFX_On;
    [SerializeField] GameObject SFX_Off;

    private void Awake() 
    {
        anime = GetComponent<Animator>(); SettingCheck = true;
        SoundGraphic.SetActive(false);
        ALL_Button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GM.BGM_Value <= 0) { BGM_On.SetActive(false); BGM_Off.SetActive(true); }
        else { BGM_On.SetActive(true); BGM_Off.SetActive(false); }

        if (GameManager.GM.SFX_Value <= 0) { SFX_On.SetActive(false); SFX_Off.SetActive(true); }
        else { SFX_On.SetActive(true); SFX_Off.SetActive(false); }

        ReSetValue();
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    public void SettingUI_ON_OFF()
    {
        switch(SettingCheck)
        {
            case true: anime.SetInteger("SettingNum", 2); SettingCheck = true; SettingCheck = false; ALL_Button.SetActive(true); break;
            case false: anime.SetInteger("SettingNum", 1); SettingCheck = true; SettingCheck = true; ALL_Button.SetActive(false);break;
        }
    }

    public void OnSound() { SoundWindow.SetActive(true); SoundGraphic.SetActive(false); }
    public void OnGraphic() { SoundWindow.SetActive(false); SoundGraphic.SetActive(true); }


    //public void ALLSlider(float value) { GameManager.GM.All_Value = value; }
    public void BGMSlider(float value) { GameManager.GM.BGM_Value = value; }
    public void SFXSlider(float value) { GameManager.GM.SFX_Value = value; }

    //public void ALLbutton() { GameManager.GM.All_Value = 0; }
    public void BGMbutton() { GameManager.GM.BGM_Value = 0;  }
    public void SFXbutton() { GameManager.GM.SFX_Value = 0;  }

    public void defaultSoundbutton() { GameManager.GM.SFX_Value = 1;
                                       GameManager.GM.BGM_Value = 1;}


    public void FPS60() { Application.targetFrameRate = 60; }
    public void FPS120() { Application.targetFrameRate = 120; }
    public void FPSFree() { Application.targetFrameRate = -1; }
    public void FPSLabal() { OnLabal = !OnLabal; }
    public void defaultGraphicbutton() { FPSFree(); OnLabal = false; }

    private void OnGUI()
    {
        if (OnLabal)
        {
            GUIStyle style = new GUIStyle();
            Rect rect = new Rect(30, 60, Screen.width, Screen.height);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = size;
            style.normal.textColor = color;

            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.} FPS", fps);

            GUI.Label(rect, text, style);
        }
    }

    void ReSetValue() { BGM.value = GameManager.GM.BGM_Value; 
                        SFX.value = GameManager.GM.SFX_Value; }

    /*
    void ReSetValue()
    {
        All.value = GameManager.GM.All_Value;
        BGM.value = (GameManager.GM.All_Value + GameManager.GM.BGM_Value);
        SFX.value = (GameManager.GM.All_Value + GameManager.GM.SFX_Value);
    }
    */
}