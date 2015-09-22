using UnityEngine;
using System.Collections;

public class AICharacterMelee : BaseAICharacterClass{


    public AICharacterMelee(int level)
    {
        m_stats.level = level;
        //this might be a bug if i dont own the player
        m_stats.maxHealth = m_stats.currentHealth = 50 * m_stats.level;
        m_stats.attackDamage = m_stats.level * 1;
    }

    public override void loseHealth(float amount)
    {
        m_stats.currentHealth -= amount;
    }
    public override void gainExperience(int amount)
    {
        Debug.Log("enemys dont gain experience");
    }
}
