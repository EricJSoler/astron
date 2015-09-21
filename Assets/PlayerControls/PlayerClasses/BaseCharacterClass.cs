using UnityEngine;
using System.Collections;
[System.Serializable]
public abstract class BaseCharacterClass 
{
    public abstract void loseHealth(float amount);
    public abstract void gainExperience(int amount);
    private string characterClassName;
    private string characterClassDescription;
    protected Stats m_stats;

    public float currentHealth
    {
        get { return m_stats.currentHealth; }
    }
    public float maxHealth
    {
        get { return m_stats.maxHealth; }
    }

    public int level
    {
        get { return m_stats.level; }
    }

    public int experience
    {
        get { return m_stats.currentExperience; }
    }

    public int reqExperience
    {
        get { return m_stats.requiredExperience; }
    }

   // public void notifyOfLevelChangeForNonLocalPlayer();
}

[System.Serializable]
public struct Stats
{
    public string characterClassName;
    public string characterClassDescription;
    public float attackDamage;
    public float currentHealth;
    public float maxHealth;
    public int level;
    public int requiredExperience;
    public int currentExperience;
   
}
