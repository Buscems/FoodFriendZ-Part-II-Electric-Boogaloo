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
    private GameObject hole;
    private Vector3 origScale;

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
    private float endOfLevelTimer = -1;
    float scaleTimer = 0;

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
        origScale = playerChar.transform.localScale;

        levelNumText.enabled = true;
        levelNumText.text = "Now Loading\n\nLevel " + currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        endOfLevelTimer -= Time.unscaledDeltaTime;
        scaleTimer -= Time.unscaledDeltaTime;
        if(endOfLevelTimer > -1 && endOfLevelTimer < 0)
        {
            endOfLevelTimer = -1;
            playerChar.transform.rotation = Quaternion.Euler(0, 0, 0);
            FadeOut();
        }
        if(scaleTimer < 0)
        {
            playerChar.transform.localScale = origScale;
        }

        if(endOfLevelTimer > 0)
        {
            Vector3 vel = Vector3.zero;
            if (playerChar.transform.position.x < hole.transform.position.x)
            {
                vel.x += 1 * Time.unscaledDeltaTime;
            }
            else
            {
                vel.x -= 1 * Time.unscaledDeltaTime;
            }
            
            if (playerChar.transform.position.y < hole.transform.position.y)
            {
                vel.y += 1 * Time.unscaledDeltaTime;
            }
            else
            {
                vel.y -= 1 * Time.unscaledDeltaTime;
            }
            
            playerChar.transform.position += vel;
            playerChar.transform.localScale -= new Vector3(.2f * Time.unscaledDeltaTime, .2f * Time.unscaledDeltaTime, .2f * Time.unscaledDeltaTime);
            if(playerChar.transform.localScale.x < .1f)
            {
                playerChar.transform.localScale = new Vector3(.1f,.1f,.1f);
            }
            playerChar.transform.Rotate(new Vector3(0, 0, 360 * Time.unscaledDeltaTime));
        }
       
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
            hole = other.gameObject;
            hole.GetComponent<BoxCollider2D>().enabled = false;
            Time.timeScale = 0;
            endOfLevelTimer = 1;
            scaleTimer = 5;
        }
    }
}
