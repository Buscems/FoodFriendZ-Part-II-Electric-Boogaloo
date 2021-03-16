using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplatChooser : MonoBehaviour
{

    public Sprite[] splats;
    public Color[] color;
    
    public bool isUI;
    public float sizeMin;
    public float sizeMax;

    private Image img;
    private SpriteRenderer sr;

    public float waitTime;
    public float disappearSpeed;
    private float alpha;

    // Start is called before the first frame update
    void Start()
    {
        if (isUI)
        {
            img = GetComponent<Image>();
            int splatIndex = Random.Range(0, splats.Length);
            int colorIndex = Random.Range(0, color.Length);
            float size = Random.Range(sizeMin, sizeMax);
            float positionX = Random.Range(-345, 345);
            float positionY = Random.Range(-170, 170);

            alpha = .75f;
            img.sprite = splats[splatIndex];
            img.color = color[colorIndex];
            img.rectTransform.localScale = new Vector3(size, size, size);
            img.rectTransform.localPosition = new Vector3(positionX, positionY, 1);
        }
        else
        {
            sr = GetComponent<SpriteRenderer>();
            int splatIndex = Random.Range(0, splats.Length);
            int colorIndex = Random.Range(0, color.Length);

            sr.sprite = splats[splatIndex];
            sr.color = color[colorIndex];
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (isUI)
        {
            waitTime -= Time.deltaTime;
            if (waitTime < 0)
            {
                alpha -= Time.deltaTime * disappearSpeed / 2;
                img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);

                if(alpha < 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
