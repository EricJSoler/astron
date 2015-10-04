using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : PlayerBase {

    List<WeaponI> weapons = new List<WeaponI>();
    int activeWeaponIndex = 0;
    /// <summary>
    /// 
    /// </summary>
    
    public GameObject[] guns;
	int currentWeoponIndex = 0;
	// Use this for initialization
	void Start (){
	}

	public void loadOwnedWeapons()
	{


	}

	void Update () {

	}

    [PunRPC]
    public void attachGun(string id) ///EH i need a good algo for this or something but i dont wanna write it right now
    {
        WeaponI[] allWeapons = GameObject.FindObjectsOfType<WeaponI>();
        foreach (WeaponI element in allWeapons) {
            if (element.GunID == id) {
                //Debug.Log("Found a match");
                element.attachWeapon(player, true);//change this bool to just add it to inventory
                weapons.Add(element);
                break;
            }
        }
    }
	[PunRPC]
	public void switchGun()
	{

    }


	public GameObject getCurrentWeapon()
	{
		return guns [currentWeoponIndex];

	}

	public int getCurrentWeaponIndex()
	{
		return currentWeoponIndex;
	}


	[PunRPC]
	public void setGun(string gunName)
	{

	}

	public void listenCurrentGunControl()
	{
        if (weapons.Count > 0) {
            weapons[activeWeaponIndex].listenForInput();
        }
	}

    [PunRPC]
    public void reloadWeapon()
    {
        if (weapons.Count > 0) {
            weapons[activeWeaponIndex].reload();
        }
    }




}
