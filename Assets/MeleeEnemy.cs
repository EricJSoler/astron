using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeEnemy : Photon.MonoBehaviour {

    XpTimer myXpTimer;
    public int health;
    public int level = 1;
	//public ParticleSystem hitParticle;
	bool playOnce;
	// Use this for initialization
	void Start () {
		//hitParticle = GetComponentInChildren<ParticleSystem> ();
        health = 50;
        myXpTimer = new XpTimer();
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            myXpTimer.iDied(level, Time.time);
			PhotonNetwork.Instantiate("CartoonExplosion",this.transform.position,transform.rotation,0);
            Destroy(gameObject);
        }
	}

    void LateUpdate()
    {
        //Maybe delete the references to the players here 
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


//uh i think il probably redo this
public class XpTimer
{
    struct TimerContainer{
        public Player player;
        public float time;

    }
    List<TimerContainer> myList;
    public XpTimer() 
    {
        myList = new List<TimerContainer>();
    }

    public void addComponent(Player shot)
    {
        TimerContainer added = new TimerContainer();
        added.player = shot;
        added.time = Time.time;
        myList.Add(added);
    }

    public void iDied(int level, float timeOfDeath)
    {
        Debug.Log("this is working");
        foreach(TimerContainer element in myList)
        {
            if(5 >= timeOfDeath - element.time)
            {
                if(element.player != null)
                    ExperienceDistributor.addExperience(element.player, level + 1);
            }
            else if(30 >= timeOfDeath - element.time)
            {
                if (element.player != null)
                    ExperienceDistributor.addExperience(element.player, level);
            }
        }
    }
    
}
