using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeEnemy : Photon.MonoBehaviour {

    XpTimer myXpTimer;
    public int health = 100;
    public int level = 1;
	bool playOnce;
	void Start () {
        myXpTimer = new XpTimer();
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            photonView.RPC("destroyMe", PhotonTargets.All);
        }
	}

    void LateUpdate()
    {
        //Maybe delete the references to the players here 
    }

    [PunRPC]
    void destroyMe()
    {
        myXpTimer.iDied(level, Time.time);
        if(photonView.isMine)
            PhotonNetwork.Instantiate("CartoonExplosion", this.transform.position, transform.rotation, 0);
        Destroy(gameObject);

    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") {

            Player hitPlayer = col.gameObject.GetComponent<Player>();
            hitPlayer.recieveDamage(25);
        }
    }

	[PunRPC]
	public void recieveDamage(float amount)
	{
		
		health -= (int)amount;

	}

    public void iShotYou(Player ishotyou)
    {
        myXpTimer.addComponent(ishotyou);
    }

}


