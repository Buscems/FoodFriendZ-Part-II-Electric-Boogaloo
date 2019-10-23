using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BasePlayer))]
public class BasePlayerEditor : Editor
{

    private BasePlayer myTarget;
    private SerializedObject soTarget;

    #region Generic Properties
    SerializedProperty hudIcon;
    SerializedProperty characterName;
    SerializedProperty attackType;
    SerializedProperty Mspeed;
    SerializedProperty baseDamage;
    SerializedProperty critChance;
    SerializedProperty critDamageMulitiplier;
    SerializedProperty dodgeSpeedMultiplier;
    SerializedProperty dodgeLength;
    SerializedProperty dodgeWaitTime;
    SerializedProperty attackSize;
    SerializedProperty canPierce;
    SerializedProperty pierceMultiplier;
    SerializedProperty maxAmountOfEnemiesCanPassThrough;
    SerializedProperty offset;
    SerializedProperty currentPosition;
    SerializedProperty currentDirection;
    SerializedProperty isChargable;
    SerializedProperty isNeedler;
    SerializedProperty isPinshot;
    #endregion

    //These are going to be all melee specific variables
    SerializedProperty weapon;
    SerializedProperty awayPos;
    SerializedProperty attackSpeed;
    SerializedProperty rotationalOffset;

    //Chargeable Variables
    SerializedProperty maxDamage;
    SerializedProperty timeTillMaxDamage;

    //Ranged Variables
    SerializedProperty bullet;
    SerializedProperty firerate;
    SerializedProperty bulletSpeed;
    SerializedProperty timeTillDespawn;
    SerializedProperty canBounce;

    //Ranged-Split Fire
    SerializedProperty radius;
    SerializedProperty bulletsPerShot;

    //Ranged-Burst Fire
    SerializedProperty bulletsPerBurst;
    SerializedProperty timeBetweenBursts;

    //Needler/Pinshot
    SerializedProperty explosionDamage;
    SerializedProperty timeBeforeExplosion;

    //Boomerang
    SerializedProperty timeBeforeReturning;

    //Builder
    SerializedProperty drop;
    SerializedProperty dropRadius;
    SerializedProperty explosionForce;

    private void OnEnable()
    {
        myTarget = (BasePlayer)target;
        soTarget = new SerializedObject(target);

        #region Variables
        hudIcon = soTarget.FindProperty("hudIcon");
        characterName = soTarget.FindProperty("characterName");
        attackType = soTarget.FindProperty("attackType");
        Mspeed = soTarget.FindProperty("Mspeed");
        baseDamage = soTarget.FindProperty("baseDamage");
        critChance = soTarget.FindProperty("critChance");
        critDamageMulitiplier = soTarget.FindProperty("critDamageMulitiplier");
        dodgeSpeedMultiplier = soTarget.FindProperty("dodgeSpeedMultiplier");
        dodgeLength = soTarget.FindProperty("dodgeLength");
        dodgeWaitTime = soTarget.FindProperty("dodgeWaitTime");
        attackSize = soTarget.FindProperty("attackSize");
        canPierce = soTarget.FindProperty("canPierce");
        pierceMultiplier = soTarget.FindProperty("pierceMultiplier");
        maxAmountOfEnemiesCanPassThrough = soTarget.FindProperty("maxAmountOfEnemiesCanPassThrough");
        offset = soTarget.FindProperty("offset");
        weapon = soTarget.FindProperty("weapon");
        awayPos = soTarget.FindProperty("awayPos");
        attackSpeed = soTarget.FindProperty("attackSpeed");
        rotationalOffset = soTarget.FindProperty("rotationalOffset");
        isChargable = soTarget.FindProperty("isChargable");
        maxDamage = soTarget.FindProperty("maxDamage");
        timeTillMaxDamage = soTarget.FindProperty("timeTillMaxDamage");
        bullet = soTarget.FindProperty("bullet");
        firerate = soTarget.FindProperty("firerate");
        bulletSpeed = soTarget.FindProperty("bulletSpeed");
        timeTillDespawn = soTarget.FindProperty("timeTillDespawn");
        canBounce = soTarget.FindProperty("canBounce");
        radius = soTarget.FindProperty("radius");
        bulletsPerShot = soTarget.FindProperty("bulletsPerShot");
        bulletsPerBurst = soTarget.FindProperty("bulletsPerBurst");
        timeBetweenBursts = soTarget.FindProperty("timeBetweenBursts");
        isNeedler = soTarget.FindProperty("isNeedler");
        isPinshot = soTarget.FindProperty("isPinshot");
        explosionDamage = soTarget.FindProperty("explosionDamage");
        timeBeforeExplosion = soTarget.FindProperty("timeBeforeExplosion");
        timeBeforeReturning = soTarget.FindProperty("timeBeforeReturning");
        drop = soTarget.FindProperty("drop");
        dropRadius = soTarget.FindProperty("dropRadius");
        explosionForce = soTarget.FindProperty("explosionForce");
        #endregion

    }

    public override void OnInspectorGUI()
    {

        soTarget.Update();

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(hudIcon);
        EditorGUILayout.PropertyField(characterName);
        EditorGUILayout.PropertyField(attackType);
        EditorGUILayout.PropertyField(Mspeed);
        EditorGUILayout.PropertyField(baseDamage);
        EditorGUILayout.PropertyField(critChance);
        if (myTarget.critChance > 0)
        {
            EditorGUILayout.PropertyField(critDamageMulitiplier);
        }
        EditorGUILayout.PropertyField(dodgeSpeedMultiplier);
        EditorGUILayout.PropertyField(dodgeLength);
        EditorGUILayout.PropertyField(dodgeWaitTime);
        EditorGUILayout.PropertyField(attackSize);
        EditorGUILayout.PropertyField(offset);
        EditorGUILayout.PropertyField(canPierce);
        if (myTarget.canPierce)
        {
            EditorGUILayout.PropertyField(pierceMultiplier);
            EditorGUILayout.PropertyField(maxAmountOfEnemiesCanPassThrough);
        }
        //Melee
        if (myTarget.attackType == BasePlayer.AttackType.Melee)
        {
            EditorGUILayout.PropertyField(weapon);
            EditorGUILayout.PropertyField(awayPos);
            EditorGUILayout.PropertyField(attackSpeed);
            EditorGUILayout.PropertyField(rotationalOffset);
        }
        if (myTarget.attackType == BasePlayer.AttackType.Melee || myTarget.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
        {
            EditorGUILayout.PropertyField(isChargable);
            //Chargable
            if (myTarget.isChargable)
            {
                EditorGUILayout.PropertyField(maxDamage);
                EditorGUILayout.PropertyField(timeTillMaxDamage);
            }
        }
        //Ranged
        if (myTarget.attackType == BasePlayer.AttackType.Ranged_Basic || myTarget.attackType == BasePlayer.AttackType.Ranged_Burst_Fire || myTarget.attackType == BasePlayer.AttackType.Ranged_Semi_Auto
            || myTarget.attackType == BasePlayer.AttackType.Ranged_Split_Fire || myTarget.attackType == BasePlayer.AttackType.Boomerang)
        {
            EditorGUILayout.PropertyField(bullet);
            EditorGUILayout.PropertyField(firerate);
            EditorGUILayout.PropertyField(bulletSpeed);
            EditorGUILayout.PropertyField(timeTillDespawn);
            EditorGUILayout.PropertyField(canBounce);
        }
        if (myTarget.attackType == BasePlayer.AttackType.Ranged_Split_Fire)
        {
            EditorGUILayout.PropertyField(radius);
            EditorGUILayout.PropertyField(bulletsPerShot);
        }
        if (myTarget.attackType == BasePlayer.AttackType.Ranged_Burst_Fire)
        {
            EditorGUILayout.PropertyField(bulletsPerBurst);
            EditorGUILayout.PropertyField(timeBetweenBursts);
        }
        if (myTarget.attackType == BasePlayer.AttackType.Ranged_Basic || myTarget.attackType == BasePlayer.AttackType.Ranged_Burst_Fire || myTarget.attackType == BasePlayer.AttackType.Ranged_Semi_Auto
            || myTarget.attackType == BasePlayer.AttackType.Ranged_Split_Fire || myTarget.attackType == BasePlayer.AttackType.Boomerang)
        {
            EditorGUILayout.PropertyField(isNeedler);
            EditorGUILayout.PropertyField(isPinshot);
        }
        if (myTarget.isNeedler || myTarget.isPinshot)
        {
            EditorGUILayout.PropertyField(explosionDamage);
            EditorGUILayout.PropertyField(timeBeforeExplosion);
        }
        if (myTarget.attackType == BasePlayer.AttackType.Boomerang)
        {
            EditorGUILayout.PropertyField(timeBeforeReturning);
        }
        //Builder
        if (myTarget.attackType == BasePlayer.AttackType.Builder)
        {
            EditorGUILayout.PropertyField(drop);
            EditorGUILayout.PropertyField(dropRadius);
            EditorGUILayout.PropertyField(explosionForce);
        }
        if(EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
            GUI.FocusControl(null);
        }

    }

    

}
