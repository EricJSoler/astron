using UnityEngine;
using System.Collections;

public class AIStats : AIBase {

    //This should be assigned from the inspector
    public BaseAICharacterClass sCharacterStats;
    public string typeOfAiStats;
	// Use this for initialization
	void Start () {
        switch (typeOfAiStats) {
            case "MeleeEnemy":
                sCharacterStats = new AICharacterMelee();
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public float currentHealth
    {
        get { return sCharacterStats.currentHealth; }
    }
    public float maxHealth
    {
        get { return sCharacterStats.maxHealth; }
    }

    public int level
    {
        get { return sCharacterStats.level; }
        set { sCharacterStats.level = value; }
    }

    public float attackDamage
    {
        get { return sCharacterStats.attackDamage; }
    }

    public void loseHealth(float amount)
    {
        sCharacterStats.loseHealth(amount);
    }
}
