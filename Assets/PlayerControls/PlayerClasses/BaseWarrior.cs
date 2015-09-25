using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseWarrior : BaseCharacterClass
{

    public BaseWarrior()
    {
        m_stats.characterClassName = "Warrior";
        m_stats.characterClassDescription = "Specialize in battle";
        m_stats.attackDamage = 1;
        m_stats.currentHealth = 100;
        m_stats.movementSpeed = 12;
        m_stats.jumpPower = 20;
        m_stats.maxHealth = 100;
        m_stats.level += 1;
        m_stats.requiredExperience = 1000;
        m_stats.currentExperience = 0;
		m_stats.electronics = 10;
    }

    void levelUp()
    {
        m_stats.level += 1;
        m_stats.currentExperience = m_stats.currentExperience - m_stats.requiredExperience;
        m_stats.maxHealth += 50;
        m_stats.currentHealth += 50;
        m_stats.requiredExperience = 1000 * (m_stats.level*m_stats.level);
        m_stats.attackDamage += 1;
    }
    
    public override void loseHealth(float amount)
    {
        m_stats.currentHealth -= amount;
    }

    public override void gainExperience(int amount)
    {
        if (amount > 0) {
            m_stats.currentExperience += amount;
            if (m_stats.currentExperience >= m_stats.requiredExperience) {
                levelUp();
            }
        }

    }






 }
