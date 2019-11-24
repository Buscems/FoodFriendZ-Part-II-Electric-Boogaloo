using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatChooser : MonoBehaviour
{

    public Sprite[] splats;
    public Color[] color;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        int splatIndex = Random.Range(0, splats.Length);
        int colorIndex = Random.Range(0, color.Length);

        sr.sprite = splats[splatIndex];
        sr.color = color[colorIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
