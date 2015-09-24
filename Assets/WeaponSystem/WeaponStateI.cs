using UnityEngine;
using System.Collections;

//I think functions like reload might need to be updated with the current time so might add a new function
//that sends the current 
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
