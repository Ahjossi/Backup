using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour {
    public GameObject bBoard;
    public GameObject collegeLogo;
    public GameObject teamLogo;
    public GameObject mainLogo;
    public GameObject labelPanel1;
    public GameObject labelPanel2;
    public UIPanel uipanel1;
    private bool isEnter = false;
    private float fadeSpeed = 0.005f;
    private float panelFadeSpeed = 0.025f;
 
    IEnumerator Start()
    {
        bBoard.transform.localScale = new Vector3(Screen.width, Screen.height*0.5f, -1);
        yield return StartCoroutine("FadeIn");
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine("FadeOut");
        collegeLogo.SetActive(false);
        teamLogo.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine("FadeIn");
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine("FadeOut");
        teamLogo.SetActive(false);
        mainLogo.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine("FadeIn");
        NGUITools.SetActive(labelPanel1, true);
        NGUITools.SetActive(labelPanel2, true);
        yield return StartCoroutine("PressFadeInOut");
    }

    IEnumerator FadeIn()
    {
        for (float i = 1f; i >= 0; i -= fadeSpeed)
        {
            Color color = bBoard.GetComponent<Renderer>().material.color;
            color = new Vector4(0, 0, 0, i);
            bBoard.GetComponent<Renderer>().material.color = color;
            yield return 0;
        }
    }

     IEnumerator FadeOut()
     {
        for (float i = 0f; i <= 1; i += fadeSpeed)
        {
            Color color = bBoard.GetComponent<Renderer>().material.color;
            color = new Vector4(0, 0, 0, i);
            bBoard.GetComponent<Renderer>().material.color = color;
            yield return 0;
        }
    }

    IEnumerator PressFadeInOut()
    {
        while (!isEnter)
        {
            for (float i = 1f; i >= 0; i -= panelFadeSpeed)
            {
                if (Input.anyKey)
                {
                    SceneManager.LoadScene("Main");
                }
                uipanel1.alpha = i;
                yield return 0;
            }
            for (float i = 0f; i <= 1; i += panelFadeSpeed)
            {
                if (Input.anyKey)
                {
                    SceneManager.LoadScene("Main");
                }
                uipanel1.alpha = i;
                yield return 0;
            }
        }
    }

    IEnumerator SceneChange()
    {
        SceneManager.LoadScene("Main");
        yield return 0;
    }
}