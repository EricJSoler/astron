using UnityEngine;
using System.Collections;

//WeaponStateI is the interface that all weapon states should inherit from
//It allows for teh states to be used interchangeably. Different weapon states
//may choose to implement the methods or not depending on what type of weapon
//they are built for
public class WeaponStateI  {


    protected WeaponI myWeapon;
    public WeaponStateI()
    {
        
    }
    public WeaponStateI(WeaponI passed)
    {
        myWeapon = passed;
    }

    public virtual void shoot()
    {
        
    }
    public virtual void reload(){
    
    }
    public virtual void drop()
    {
        
    }
    public virtual void switchFrom()
    {
        
    }
    public virtual void switchTo()
    {
        
    }
    public virtual void scope()
    {
        
    }
    public virtual void aim()
    {
        
    }
    public virtual void pickup() //This probably needs the player's script but idk yet
    {
        
    }
}
