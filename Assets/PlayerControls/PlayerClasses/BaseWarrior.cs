using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseWarrior : BaseCharacterClass
{

    public BaseWarrior()
    {
        CharacterClassName = "Warrior";
        CharacterClassDescription = "Specialize in battle";
        AttackDamage = 10;
        CurrentHealth = 100;
        MaxHealth = 100;

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
