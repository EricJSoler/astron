using UnityEngine;
using System.Collections;
[System.Serializable]
public class BaseCharacterClass
{
    private string characterClassName;
    private string characterClassDescription;
    [System.Serializable]
    struct Stats
    {
        public int stamina;
        public int endurance;
        public int strength;
        public int intellect;
        public int attackDamage;
        public int magicDamage;
        public int currentHealth;
        public int maxHealth;
        public string myPlayersName;
    }

    Stats m_stats;

    public string CharacterClassName
    {
        get { return characterClassName; }
        set { characterClassName = value; }
    }

    public string CharacterClassDescription
    {
        get { return characterClassDescription; }
        set { characterClassDescription = value; }
    }

    public int AttackDamage
    {
        get { return m_stats.attackDamage; }
        set { m_stats.attackDamage = value; }
    }
    public int CurrentHealth
    {
        get { return m_stats.currentHealth; }
        set { m_stats.currentHealth = value; }
    }

    public int MaxHealth
    {
        get { return m_stats.maxHealth; }
        set { m_stats.maxHealth = value; }
    }
}
