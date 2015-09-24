using UnityEngine;
using System.Collections;

//I think functions like reload might need to be updated with the current time so might add a new function
//that sends the current 
public class WeaponStateI  {


    protected WeaponI myWeapon;
    public WeaponStateI()
    {
        Debug.Log("MakeSure to passin an WeaponI taht this is attached to");
    }
    public WeaponStateI(WeaponI passed)
    {
        myWeapon = passed;
    }

    public virtual void shoot()
    {
        Debug.Log("not implemented");
    }
    public virtual void reload(){
    Debug.Log("not implemented");
}
    public virtual void drop()
    {
        Debug.Log("not implemented");
    }
    public virtual void switchFrom()
    {
        Debug.Log("not implemented");
    }
    public virtual void switchTo()
    {
        Debug.Log("not implemented");
    }
    public virtual void scope()
    {
        Debug.Log("not implemented");
    }
    public virtual void aim()
    {
        Debug.Log("not implemented");
    }
    public virtual void pickup() //This probably needs the player's script but idk yet
    {
        Debug.Log("not implemented");
    }
}
