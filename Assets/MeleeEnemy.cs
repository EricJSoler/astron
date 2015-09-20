using UnityEngine;
using System.Collections;

public class MeleeEnemy : Photon.MonoBehaviour {


    public int health;
	//public ParticleSystem hitParticle;
	bool playOnce;
	// Use this for initialization
	void Start () {

		//hitParticle = GetComponentInChildren<ParticleSystem> ();
        health = 25;
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
	
			PhotonNetwork.Instantiate("CartoonExplosion",this.transform.position,transform.rotation,0);
            Destroy(gameObject);
        }
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

}
