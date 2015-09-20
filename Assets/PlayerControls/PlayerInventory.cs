using UnityEngine;
using System.Collections;

public class PlayerInventory : Photon.MonoBehaviour {

	public GameObject[] guns;
	int currentWeoponIndex = 0;
	// Use this for initialization
	void Start () {

		loadOwnedWeapons ();

	}

	public void loadOwnedWeapons()
	{
		for (int i = 0; i < guns.Length; i++) {
			if(photonView.isMine)
				guns[i].GetComponent<Gun>().setOwned(true);
			if(guns[i].GetComponent<Gun>().name != "DefaultGun")
			{
				guns[i].SetActive(false);
				guns[i].GetComponent<Gun>().crossHair.SetActive(false);
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
		this.photonView.RPC("setGun",PhotonTargets.All,(guns[currentWeoponIndex].GetComponent<Gun>().name));
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
				guns[i].SetActive(false);
				guns[i].GetComponent<Gun>().crossHair.SetActive(false);
			}
			else{
				guns[i].SetActive(true);
				guns[i].GetComponent<Gun>().crossHair.SetActive(true);
				//guns[i].GetComponent<Gun>().setOwned(true);
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
