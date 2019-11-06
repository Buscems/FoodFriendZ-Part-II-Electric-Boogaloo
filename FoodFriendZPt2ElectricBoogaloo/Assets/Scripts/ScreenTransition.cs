using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    [Header("Fade")]
    public Image fadeObject;
    public float fadeLength;

    private GoToNextLevel player;

    public float fadeSpeed;
    private float alpha = 1; 
    bool alphaUp = false;
    bool doFade = true;

    // Start is called before the first frame update
    void Start()
    {
        fadeObject.gameObject.SetActive(true);
        Color newColor = new Color(0, 0, 0, 1);
        fadeObject.GetComponent<Image>().color = newColor;
        FadeIn();
        player = GetComponent<GoToNextLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        Color newColor = new Color(0, 0, 0, alpha);
        fadeObject.GetComponent<Image>().color = newColor;
        if (doFade)
        {
            if (alphaUp)
            {
                alpha += fadeSpeed * Time.deltaTime;
            }
            else if (!alphaUp)
            {
                alpha -= fadeSpeed * Time.deltaTime;
            }

        }
        if(alphaUp && alpha > 1.1f)
        {
            doFade = false;
            alpha = 1;
            player.NextLevel();
        }
        if (!alphaUp && alpha < -0.1f)
        {
            doFade = false;
            alpha = 0;
        }

        /*
        if (fadeObject.GetComponent<Image>().color.a >= 1)
        {

            player.NextLevel();
            try
            {
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Projectile");

                for (var i = 0; i < gameObjects.Length; i++)
                    Destroy(gameObjects[i]);
            }
            catch { }

            FadeIn();
        }
        */
        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            FadeOut();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            FadeIn();
        }
        */
    }

    public void FadeOut()
    {
        doFade = true;
        alphaUp = true;
        //StartCoroutine(C_FadeOut());
    }

    IEnumerator C_FadeOut()
    {
        float alpha = fadeObject.GetComponent<Image>().color.a;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / fadeLength)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, 1f, i));
            fadeObject.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }

    private void FadeIn()
    {
        doFade = true;
        alphaUp = false;
        // StartCoroutine(C_FadeIn());
    }

    IEnumerator C_FadeIn()
    {
        float alpha = fadeObject.GetComponent<Image>().color.a;
        for (float i = 1.0f; i > 0.0f; i -= Time.deltaTime / fadeLength)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(0f, alpha, i));
            fadeObject.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Exit")
        {
            FadeOut();
        }
    }
}
