using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [Tooltip("Power-Up Names")]
    public string powerUpName = "";

    public float movementSpeed = 1;
    public float attackSize = 1;
    public float attackSpeed = 1;
    public float attackDamage = 1;

    public int healAmt;


    //this gives drop down menu selects, make new line write name then put comma
    //if last line, do not put comma at end of word
    public enum PowerUpTypes
    {
        Null,
        Heal,
        DebuffRemove,
        SlowTime,
        AvoidPlayer,
        SlowEnemy,
        AbsorbAttackUp,
        AbsorbProjectile,
        AbsorbLine,
        Shrapnel,
        ShrapnelMod,
        DebuffTrail,
        RamAttackLine,
        AOETenderizer,
        AOEWhisk,
        ShootAttackLine,
        EasterEgg,
        StunAttack,
        AttackSpeedHealthProportion,
        AttackSpeedDamageProportion,
        NullAttack,
        Poison,
        ItemReedem,
        AttackDmgHitProportion,
        PaasivePoison,
        ExtraLife,
        FatalChance//last one so no comma

    }//END OF ENUMS

    public PowerUpTypes currentPowerUp;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }//END OF ON TRIGGER

    //LAST LEFT OFF - NEED TO FIND WHY THIS DOESNT WORK
    IEnumerator Pickup(Collider2D player)
    {
        
        switch (currentPowerUp)
        {
            //case 1
            case PowerUpTypes.Heal:

                healAmt = 2;
                
                float timer = 2;
                yield return new WaitForSeconds(timer);
                break;

            //case2
            case PowerUpTypes.StunAttack:

                break;

            case PowerUpTypes.Null:

                break;

            default:
                Debug.Log("Basic Power Up");
                break;


        }//END OF SWITCH
    }//END OF IENUMERATOR    
}//END OF SCRIPT