using UnityEngine;
using System.Collections;

public class PlayerInventory : PlayerBase {

	public GameObject[] guns;
	int currentWeoponIndex = 0;
	// Use this for initialization
	void Start () {
		loadOwnedWeapons ();
	}

	public void loadOwnedWeapons()
	{
		for (int i = 0; i < guns.Length; i++) {
            if (photonView.isMine) {
                guns[i].GetComponent<Gun>().setActive(true);
                guns[i].GetComponent<Gun>().setOwned(true, this.player);
            }
			if(guns[i].GetComponent<Gun>().name != "DefaultGun")
			{
				guns[i].GetComponent<Gun>().setActive(false);
                guns[i].GetComponent<Gun>().setOwned(true, this.player);
			}
		}

	}

	void Update () {

	}


	[PunRPC]
	public void switchGun()
	{
		currentWeoponIndex++;
		currentWeoponIndex %= guns.Length;
        string nameToSend = guns[currentWeoponIndex].GetComponent<Gun>().name;
		photonView.RPC("setGun",PhotonTargets.All,nameToSend);
        //this.setGun(nameToSend);
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
		for (int i = 0; i < guns.Length; i++) {
			if(guns[i].GetComponent<Gun>().name != gunName)
			{
				guns[i].GetComponent<Gun>().setActive(false);
			}
			else{
				guns[i].GetComponent<Gun>().setActive(true);
			}
		}
	}

    public void recieveInput(float fireInput)
    {
        if (fireInput > .5f) {
            getCurrentWeapon().GetComponent<Gun>().fireShot();
        }
    }
}
