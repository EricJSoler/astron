using UnityEngine;
using System.Collections;

public class BurstShotFireControl : GunFireControl {


	private int burstRate;

	// Use this for initialization


	public BurstShotFireControl(int inRate, Gun myGun)
	{
		burstRate = inRate;
		myCurrentGun = myGun;
	}



	public override void gunControl()
	{
		if (Input.GetMouseButtonDown (0)) {

			for(int i = 0; i < burstRate; i++)
			{
				//PlayerInventory.getCurrentWeapon().GetComponent<Gun>().fireShot();
				fire++;
				//StartCoroutine(waitTime(0.5f));
			}
			
		}

	}


	public override int fireNow()
	{
		return fire;
	}



	public override void setFire(int set)
	{
		fire = set;
		
	}
	
	//public IEnumerator waitTime(float wait)
	//{
	//	yield return new WaitForSeconds(wait);

	//}


}
