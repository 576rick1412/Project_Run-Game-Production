using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Loading_Manager : MonoBehaviour
{
    static string nextScene;
    static string Stage_Name;
    static string Stage_Description;
    [SerializeField] Image ProgressBar;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;

    public static void LoadScene(string SceneName, string ST_Name, string ST_Description)
    {
        Fade_effect oc = GameObject.Find("Hephaestus_Canvas").GetComponent<Fade_effect>();
        AsyncOperation op = SceneManager.LoadSceneAsync("Main_LoadingScene");

        nextScene = SceneName;
        Stage_Name = ST_Name;

        Stage_Description = ST_Description;

        oc.Fade(op);
    }
    public static void LoadScene(string SceneName, string ST_Description)
    {
        Fade_effect oc = GameObject.Find("Hephaestus_Canvas").GetComponent<Fade_effect>();
        AsyncOperation op = SceneManager.LoadSceneAsync("Main_LoadingScene");
        op.allowSceneActivation = false;

        nextScene = SceneName;
        Stage_Name = "";

        Stage_Description = ST_Description;

        oc.Fade(op);
    }
    void Start()
    {
        Name.text = Stage_Name;
        Description.text = Stage_Description;

        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        Fade_effect oc = GameObject.Find("Hephaestus_Canvas").GetComponent<Fade_effect>();

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                ProgressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                ProgressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (ProgressBar.fillAmount >= 1f)
                {
                    oc.Fade(op);
                    yield break;
                }
            }
        }
    }
}
