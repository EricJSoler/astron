﻿using UnityEngine;
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

    public virtual int level
    {
        get { return m_stats.level; }
        set { Debug.Log("you are not allowed to set this charactersLevel"); }//do nothing on purpose
    }

    public float attackDamage
    {
        get { return m_stats.attackDamage; }
    }

    public float electronics
    {
        get { return m_stats.electronics; }
    }

    public float movementSpeed
    {
        get { return m_stats.movementSpeed; }
    }
    public float jumpPower
    {
        get { return m_stats.jumpPower; }
    }
    public int experience
    {
        get { return m_stats.currentExperience; }
    }

    public int reqExperience
    {
        get { return m_stats.requiredExperience; }
    }





	public virtual void increaseAttackDamage(int inDamage)
	{
		m_stats.attackDamage += inDamage;
	}
	
	public virtual void increaseMoveSpeed(int inMove)
	{
		m_stats.movementSpeed += inMove;
	}
	
	public virtual void increaseJump(int inJump)
	{
		m_stats.jumpPower += inJump;
	}
	
	public virtual void increaseElectronics(int inElectronics)
	{
		m_stats.electronics += inElectronics;
	}



   // public void notifyOfLevelChangeForNonLocalPlayer();
}

[System.Serializable]
public struct Stats
{
    public string characterClassName;
    public string characterClassDescription;
    public float attackDamage;
    public float electronics;
    public float movementSpeed;
    public float jumpPower;
    public float currentHealth;
    public float maxHealth;
    public int level;
    public int requiredExperience;
    public int currentExperience;
   
}
