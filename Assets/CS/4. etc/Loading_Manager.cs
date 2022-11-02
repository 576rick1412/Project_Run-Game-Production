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

    public static void LoadScene(string SceneName, TextMeshProUGUI ST_Name, TextMeshProUGUI ST_Description)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Stage_LoadingScene");

        nextScene = SceneName;
        Stage_Name = ST_Name.text;

        Stage_Description = ST_Description.text;

        GameManager.GM.Fade(op);
    }
    public static void LoadScene(string SceneName, string ST_Description)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Main_LoadingScene");
        op.allowSceneActivation = false;

        nextScene = SceneName;
        Stage_Name = "";

        Stage_Description = ST_Description;

        GameManager.GM.Fade(op);
    }
    void Start()
    {
        Name.text = Stage_Name;
        Description.text = Stage_Description;

        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
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
                    GameManager.GM.Fade(op);
                    yield break;
                }
            }
        }
    }
}
