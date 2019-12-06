using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    [Header("Fade")]
    public Image fadeObject;
    public Image loadingImage;
    public float loadingRotSpeed;
    public float fadeLength;
    private GameObject playerChar;

    private GoToNextLevel player;

    public float fadeSpeed;
    private float alpha = 1; 
    bool alphaUp = false;
    bool doFade = true;

    public float loadingTimer;
    private float currentLoadTimer;
    private bool isLoading = true;

    private float rotAngle = 1;

    public Text levelNumText;
    private int currentLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        fadeObject.gameObject.SetActive(true);
        Color newColor = new Color(0, 0, 0, 1);
        fadeObject.GetComponent<Image>().color = newColor;
        FadeIn();
        player = GetComponent<GoToNextLevel>();
        playerChar = GameObject.FindGameObjectWithTag("Player1");
        currentLoadTimer = loadingTimer * 2;
        Time.timeScale = 0;
        loadingImage.enabled = true;

        levelNumText.enabled = true;
        levelNumText.text = "Now Loading\n\nLevel " + currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (doFade)
        {
            Color newColor = new Color(0, 0, 0, alpha);
            fadeObject.GetComponent<Image>().color = newColor;
            if (alphaUp)
            {
                alpha += fadeSpeed * Time.unscaledDeltaTime;
            }
            else if (!alphaUp)
            {
                alpha -= fadeSpeed * Time.deltaTime;
            }

        }
        if(alphaUp && alpha > 1.1f)
        {
            doFade = true;
            alphaUp = false;
            alpha = 1;
            player.NextLevel();
            isLoading = true;
            Time.timeScale = 0;
            currentLoadTimer = loadingTimer;
        }
        if (!alphaUp && alpha < -0.1f)
        {
            doFade = false;
            alpha = 0;
        }

        if (isLoading)
        {
            levelNumText.enabled = true;
            levelNumText.text = "Now Loading\n\nLevel " + currentLevel;
            loadingImage.enabled = true;
            Time.timeScale = 0;
            fadeObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            playerChar.GetComponent<MainPlayer>().cantGetHitTimer = 1;
            currentLoadTimer -= Time.unscaledDeltaTime;
            rotAngle += Time.unscaledDeltaTime * loadingRotSpeed;
            loadingImage.rectTransform.rotation = Quaternion.Euler(0, 0, rotAngle);
            if(currentLoadTimer < 0)
            {
                isLoading = false;
                Time.timeScale = 1;
                loadingImage.enabled = false;
                levelNumText.enabled = false;
            }
        }

    }

    public void FadeOut()
    {
        doFade = true;
        alphaUp = true;
        alpha = 0;
        Time.timeScale = 0;
        currentLevel++;
        //StartCoroutine(C_FadeOut());
    }

    private void FadeIn()
    {
        doFade = true;
        alphaUp = false;
        alpha = 1;
        isLoading = true;
        Time.timeScale = 0;
        currentLoadTimer = loadingTimer;
        // StartCoroutine(C_FadeIn());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Exit")
        {
            FadeOut();
        }
    }
}
