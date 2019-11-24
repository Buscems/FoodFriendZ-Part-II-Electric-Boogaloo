using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseBoss : MonoBehaviour
{
    ItemExtension ieScript;

    [Header("Generic Enemy Values")]
    [Tooltip("How much health the enemy will have(This will be a high number for now so that the player can have high damage numbers")]
    public float maxHealth;
    //[HideInInspector]
    public float health;
    [HideInInspector]
    public float healthPercent;
    [Tooltip("How fast we want the enemy to move")]
    public float speed;
    private float origSpeed;
    [Tooltip("How much damage this enemy deals to the player when the player runs into them (Should only be between 0 and 1)")]
    [Range(0, 1)]
    public int walkIntoDamage;

    bool dead;

    Animator anim;
    public Animator backAnim;

    [Tooltip("How much money the boss will drop when killed")]
    public int money;

    [Header("Camera Animations")]
    public GameObject cam;
    [HideInInspector]
    public bool playerEntered;
    bool startAnim;

    [Header("Boss UI")]
    public GameObject healthImage;
    public GameObject healthBackground;
    public float fadeInTime;

    public GameObject bossName;

    [Header("Current Aggro Script")]
    public Aggro aggroScript;

    public enum BossStage { stage1, stage2, stage3, death }
    //[HideInInspector]
    public BossStage stage;


    private float bleedTimer = 0;
    private float bleedDamage;
    private float bleedTickRate = 0;
    private float currentBleedTickRate = 0;
    private float burnTimer = 0;
    private float burnDamage;
    private float poisonTimer = 0;
    private float poisonDamage;
    private float freezeTimer = 0;
    private float stunTimer = 0;

    private float slowDownPercentage = 1;

    private SpriteRenderer sr;

    private void Awake()
    {
        ieScript = GameObject.Find("Player").GetComponent<ItemExtension>();
    }

    void Start()
    {
        origSpeed = speed;
        cam = GameObject.Find("Main Camera");
        healthImage = GameObject.Find("Health");
        healthBackground = GameObject.Find("HealthBackground");
        bossName = GameObject.Find("BossName");

        healthImage.GetComponent<Image>().enabled = false;
        healthBackground.GetComponent<Image>().enabled = false;
        bossName.GetComponent<TextMeshProUGUI>().enabled = false;

        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        health = maxHealth;

        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (ieScript.needBossScript && ieScript != this)
        {
            ieScript.bossScript = this;
        }

        sr.color = new Color(1, sr.color.g + 5f * Time.deltaTime, sr.color.b + 5f * Time.deltaTime);
        //starting the animations for the boss fight
        if (playerEntered && !startAnim)
        {
            StartCoroutine(EnterRoomCamera());
            startAnim = true;
        }

        //keeping track of the percentage of the bosses health to have different stages
        healthPercent = health / maxHealth;

        if (healthPercent > .75f)
        {
            stage = BossStage.stage1;
        }
        else if (healthPercent > .5f)
        {
            stage = BossStage.stage2;
        }
        else
        {
            stage = BossStage.stage3;
        }

        //setting the health bar equal to the boss
        healthImage.GetComponent<Image>().fillAmount = healthPercent;

        speed = origSpeed * slowDownPercentage;

        StatusEffectTimers();
        StatusEffects();

        //boss death
        if (health <= 0)
        {
            if (!dead)
            {
                StartCoroutine(Death());
            }
        }

    }

    private void StatusEffectTimers()
    {
        bleedTimer -= Time.deltaTime;
        currentBleedTickRate -= Time.deltaTime;
        burnTimer -= Time.deltaTime;
        poisonTimer -= Time.deltaTime;
        freezeTimer -= Time.deltaTime;
        stunTimer -= Time.deltaTime;
    }

    public void StatusEffects()
    {
        if (bleedTimer > 0)
        {
            if (currentBleedTickRate < 0)
            {
                currentBleedTickRate = bleedTickRate;
                health -= bleedDamage;
            }
        }

        if (burnTimer > 0)
        {
            health -= burnDamage;
        }

        if (poisonTimer > 0)
        {
            health -= poisonDamage;
        }
        else
        {
            slowDownPercentage = 1;
        }

        if (freezeTimer < 0 && stunTimer < 0)
        {
            slowDownPercentage = 1;
        }

    }

    public void SetBleed(float[] _bleedVariables)
    {
        bleedDamage = _bleedVariables[0];
        bleedTimer = _bleedVariables[1];
        bleedTickRate = _bleedVariables[2];
    }
    public void SetBurn(float[] _burnVariables)
    {
        burnDamage = _burnVariables[0];
        burnTimer = _burnVariables[1];
    }
    public void SetPoison(float[] _poisonVariables)
    {
        poisonDamage = _poisonVariables[0];
        poisonTimer = _poisonVariables[1];
        freezeTimer = _poisonVariables[1];
        stunTimer = _poisonVariables[1];
        slowDownPercentage = _poisonVariables[2];
    }
    public void SetFreeze(float[] _freezeVariables)
    {
        freezeTimer = _freezeVariables[0];
        stunTimer = _freezeVariables[0];
        slowDownPercentage = _freezeVariables[1];
    }
    public void SetStun(float _stun)
    {
        stunTimer = _stun;
        freezeTimer = _stun;
        slowDownPercentage = 0;
    }
    public IEnumerator EnterRoomCamera()
    {
        var follow = cam.GetComponent<FollowPlayer>();
        follow.player.transform.parent.transform.parent.GetComponent<MainPlayer>().canMove = false;
        follow.player = this.gameObject.transform;
        follow.bossCamera = true;
        while (Mathf.Abs(follow.distance) >= follow.radius)
        {
            yield return null;
        }
        anim.SetTrigger("roar");
        if (backAnim != null)
        {
            backAnim.SetTrigger("roar");
        }
    }
    public void StartFightCamera()
    {
        StartCoroutine(FightCam());
    }

    private IEnumerator FightCam()
    {
        var follow = cam.GetComponent<FollowPlayer>();
        follow.player = GameObject.Find("CamTrackPos").gameObject.transform;
        follow.bossCamera = false;
        while (Mathf.Abs(follow.distance) >= follow.radius)
        {
            yield return null;
        }
        follow.player.transform.parent.transform.parent.GetComponent<MainPlayer>().canMove = true;
        aggroScript.aggro = true;
    }


    private IEnumerator HealthFadeIn()
    {
        healthImage.GetComponent<Image>().enabled = true;
        healthBackground.GetComponent<Image>().enabled = true;
        bossName.GetComponent<TextMeshProUGUI>().enabled = true;
        //health bar
        healthImage.GetComponent<Image>().color = new Color(healthImage.GetComponent<Image>().color.r, healthImage.GetComponent<Image>().color.g, healthImage.GetComponent<Image>().color.b, 0);
        //health bar background
        healthBackground.GetComponent<Image>().color = new Color(healthBackground.GetComponent<Image>().color.r, healthBackground.GetComponent<Image>().color.g, healthBackground.GetComponent<Image>().color.b, 0);
        //boss name
        bossName.GetComponent<TextMeshProUGUI>().text = this.gameObject.name;
        bossName.GetComponent<TextMeshProUGUI>().color = new Color(bossName.GetComponent<TextMeshProUGUI>().color.r, bossName.GetComponent<TextMeshProUGUI>().color.g, bossName.GetComponent<TextMeshProUGUI>().color.b, 0);

        while (healthImage.GetComponent<Image>().color.a < 1 && healthBackground.GetComponent<Image>().color.a < 1 && bossName.GetComponent<TextMeshProUGUI>().color.a < 1)
        {
            healthImage.GetComponent<Image>().color += new Color(0, 0, 0, fadeInTime * Time.deltaTime);
            healthBackground.GetComponent<Image>().color += new Color(0, 0, 0, fadeInTime * Time.deltaTime);
            bossName.GetComponent<TextMeshProUGUI>().color += new Color(0, 0, 0, fadeInTime * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator HealthFadeOut()
    {
        while (healthImage.GetComponent<Image>().color.a > 0 && healthBackground.GetComponent<Image>().color.a > 0 && bossName.GetComponent<TextMeshProUGUI>().color.a > 0)
        {
            healthImage.GetComponent<Image>().color -= new Color(0, 0, 0, fadeInTime * Time.deltaTime);
            healthBackground.GetComponent<Image>().color -= new Color(0, 0, 0, fadeInTime * Time.deltaTime);
            bossName.GetComponent<TextMeshProUGUI>().color -= new Color(0, 0, 0, fadeInTime * Time.deltaTime);
            yield return null;
        }
        healthImage.GetComponent<Image>().enabled = false;
        healthBackground.GetComponent<Image>().enabled = false;
        bossName.GetComponent<TextMeshProUGUI>().enabled = false;
    }
    public void StartFadeIn()
    {
        StartCoroutine(HealthFadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(HealthFadeOut());
    }

    public void TakeDamage(float _damage)
    {
        if (ieScript.needBossScript)
        {
            ieScript.hasPlayerHitBoss = true;
        }

        health -= _damage;
        sr.color = new Color(1, .35f, .35f);
    }

    IEnumerator Death()
    {
        stage = BossStage.death;
        if (anim != null)
        {
            anim.SetTrigger("death");
        }
        if (backAnim != null)
        {
            anim.SetTrigger("death");
        }
        //testing now
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

}
