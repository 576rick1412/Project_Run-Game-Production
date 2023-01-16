using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_effect : MonoBehaviour
{
    public static Fade_effect fade;

    public Image panel;
    [Header("페이드 시간 관리")]public float fTime = 0.5f;

    float time = 0f;


    private void Start()
    {
        panel.gameObject.SetActive(false);
    }
    public void Fade()
    {
        StartCoroutine(FadeEffect());
    }
    public void Fade(GameObject curObj, GameObject nextObj)
    {
        StartCoroutine(FadeEffect(curObj, nextObj));
    }
    public void Fade(AsyncOperation op)
    {
        StartCoroutine(FadeEffect(op));
    }

    // ===========================================================================

    IEnumerator FadeEffect()
    {
        panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = panel.color;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;

            yield return null;
        }

        time = 0f;
        yield return null;


        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator FadeEffect(GameObject curObj, GameObject nextObj)
    {
        panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = panel.color;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;

            yield return null;
        }

        time = 0f;
        curObj.SetActive(false);    
        yield return null;  //yield return new WaitForSeconds(L_time);
        nextObj.SetActive(true);


        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
    IEnumerator FadeEffect(AsyncOperation op)
    {
        panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = panel.color;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;

            yield return null;
        }

        time = 0f;
        op.allowSceneActivation = true;
        yield return null;   //yield return new WaitForSeconds(L_time);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
}
