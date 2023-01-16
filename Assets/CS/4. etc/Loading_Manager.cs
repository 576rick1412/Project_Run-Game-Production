using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Loading_Manager : MonoBehaviour
{
    static string nextScene;
    static string stageName;
    static string stageDescription;
    [SerializeField] Image progressBar;

    public TextMeshProUGUI inName;
    public TextMeshProUGUI description;

    public static void LoadScene(string sceneName, string ST_Name, string ST_Description)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Stage_LoadingScene");

        nextScene = sceneName;
        stageName = ST_Name;

        stageDescription = ST_Description;

        GameManager.GM.Fade(op);
    }
    public static void LoadScene(string sceneName, string ST_Description)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Main_LoadingScene");
        op.allowSceneActivation = false;

        nextScene = sceneName;
        stageName = "";

        stageDescription = ST_Description;

        GameManager.GM.Fade(op);
    }
    void Start()
    {
        inName.text = stageName;
        description.text = stageDescription;

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
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    GameManager.GM.Fade(op);
                    yield break;
                }
            }
        }
    }
}
