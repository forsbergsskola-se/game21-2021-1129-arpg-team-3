using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObject/Player/Stats", fileName = "Stats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float health;
    [SerializeField] private float playerArmour;
    [SerializeField] private float gold;
    [SerializeField] private float score;
    [SerializeField] private float mana;
    [SerializeField] private float minHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float meeleeRange;
    [SerializeField] private float experience;
    [SerializeField] private float maxExperience;
    [SerializeField] private float playerLevel;
    [SerializeField] private float playerLevelMultiplier;
    [SerializeField] private float xPMultiplier;
    [SerializeField] private float attackDelay;
    [SerializeField] private float combatRotationSpeed;
    [SerializeField] private bool playerDied;

    public Attribute[] attributes;

    public bool PlayerDied => playerDied;
    public float CombatRotationSpeed => combatRotationSpeed;

    public float AttackDelay => attackDelay;
    public float Health {
        get => health;
        set {
            health = value;
            health = Mathf.Clamp(health, minHealth, maxHealth);
        }
    }
    
    public float MaxHealth {
        get => maxHealth;
        set => maxHealth = value;
    }
    
    public float PlayerArmour {
        get => playerArmour;
        set => playerArmour = value;
    }

    public float Gold {
        get => gold;
        set => gold = value;
    }
    
    public float Mana {
        get => mana;
        set => mana = value;
    }

    public float Score {
        get => score;
        set => score = value;
    }
    
    public float WeaponDamage {
        get => weaponDamage;
        set => weaponDamage = value;
    }
    
    public float MeleeRange {
        get => meeleeRange;
    }

    public float Experience {
        get => experience;
        set {
            experience = value;
            PlayerLeveling();
        }
    }
    private void PlayerLeveling() {

        if (experience >= MaxExperience) {
            experience -= MaxExperience;
            PlayerLevel++;
            MaxExperience += PlayerLevelMultiplier;
            WeaponDamage += 3;
            MaxHealth += 3;
            Health = MaxHealth;
        }
    }
    public float MaxExperience {
        get => maxExperience;
        set => maxExperience = value;

    }
    public float PlayerLevelMultiplier {
        get => playerLevelMultiplier;
        set => playerLevelMultiplier = value;
    }
    
    public float XPMultiplier {
        get => xPMultiplier;
        set => xPMultiplier = value;
    }
    public float PlayerLevel {
        get => playerLevel;
        set => playerLevel = value;
    }

    public void TakeDamage(float damage, GameObject player) {
        Health -= damage;
        if (Health <= 0) {
            KillPlayer(player);
        }
    }

    public void InitializePlayerStats() {
        health = maxHealth;
        playerDied = false;
    }

    public void KillPlayer(GameObject player) {
        playerDied = true;
        Debug.Log("Player is Dead");
        gold -= 50;
        SceneManager.LoadScene(1);
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " updated. Value is now ", attribute.value.ModifiedValue));
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized] public PlayerStats parent;
    public Attribute type;
    public ModifiableInt value;

    public void SetParent(PlayerStats _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}