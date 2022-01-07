using UnityEngine;

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
    [SerializeField] private int deathCount;
    [SerializeField] private float vulnerability = 0.5f;
    public int zone;
    public bool secondary;
    public bool tertiary;

    public float Vulnerability => vulnerability;
    public int DeathCount => deathCount;
    public bool PlayerDied 
    {
        get => playerDied;
        set => playerDied = value;
    }
    public float CombatRotationSpeed => combatRotationSpeed;

    public float AttackDelay => attackDelay;
    public float Health 
    {
        get => health;
        set {
            health = value;
            health = Mathf.Clamp(health, minHealth, maxHealth);
        }
    }
    
    public float MaxHealth 
    {
        get => maxHealth;
        set => maxHealth = value;
    }
    
    public float PlayerArmour 
    {
        get => playerArmour;
        set => playerArmour = value;
    }

    public float Gold 
    {
        get => gold;
        set => gold = value;
    }
    
    // public float Mana {
    //     get => mana;
    //     set => mana = value;
    // }
    //
    // public float Score {
    //     get => score;
    //     set => score = value;
    // }
    
    public float WeaponDamage
    {
        get => weaponDamage;
        set => weaponDamage = value;
    }
    
    public float MeleeRange => meeleeRange;

    public float Experience 
    {
        get => experience;
        set => experience = value;
    }

    public float MaxExperience 
    {
        get => maxExperience;
        set => maxExperience = value;

    }
    public float PlayerLevelMultiplier 
    {
        get => playerLevelMultiplier;
        set => playerLevelMultiplier = value;
    }
    
    public float XPMultiplier 
    {
        get => xPMultiplier;
        set => xPMultiplier = value;
    }
    public float PlayerLevel 
    {
        get => playerLevel;
        set => playerLevel = value;
    }

    public void TakeDamage(float damage) 
    {
        Health -= damage;
        if (Health <= 0) {
            KillPlayer();
        }
    }

    public void InitializePlayerStats()
    {
        maxHealth = 100;
        health = maxHealth;
        playerDied = false;
        maxExperience = 50;
        experience = 0;
        gold = 0;
        playerLevel = 0;
        weaponDamage = 20;
        secondary = false;
        tertiary = false;
        deathCount = 0;
        zone = 0;
    }

    private void KillPlayer() 
    {
        playerDied = true;
        Debug.Log("Player is Dead");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerDeath");
        deathCount++;
        if (deathCount <= 25 && (deathCount == 1 || deathCount % 5 == 0)) 
        {
            FMODUnity.RuntimeManager.PlayOneShot($"event:/Vox/death counter/{deathCount}");
        }
        // gold -= 50;
        // SceneManager.LoadScene(1);
    }
}

