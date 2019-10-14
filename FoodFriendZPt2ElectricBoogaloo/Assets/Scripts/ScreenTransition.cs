using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    [Header("Fade")]
    public Image fadeObject;
    public float fadeLength;

    // Start is called before the first frame update
    void Start()
    {
        Color newColor = new Color(0, 0, 0, 1);
        fadeObject.GetComponent<Image>().color = newColor;
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            FadeOut();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            FadeIn();
        }
    }

    public void FadeOut()
    {
        StartCoroutine(C_FadeOut());
    }

    IEnumerator C_FadeOut()
    {
        float alpha = fadeObject.GetComponent<Image>().color.a;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / fadeLength)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, 1, i));
            fadeObject.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }

    private void FadeIn()
    {
        StartCoroutine(C_FadeIn());
    }

    IEnumerator C_FadeIn()
    {
        float alpha = fadeObject.GetComponent<Image>().color.a;
        for (float i = 1.0f; i > 0.0f; i -= Time.deltaTime / fadeLength)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(0, alpha, i));
            fadeObject.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }
}
