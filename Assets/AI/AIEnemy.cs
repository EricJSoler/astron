using UnityEngine;
using System.Collections;

public class AIEnemy : AIBase {
    XpTimer myXpTimer;
	// Use this for initialization
	void Start () {
        myXpTimer = new XpTimer();
	}
	
	// Update is called once per frame
    void Update(){
        if (aiStats.currentHealth <= 0) {
            photonView.RPC("destroyThisPlayer", PhotonTargets.All);
            //visual update here of stuff blowong up
        }
        
    }

     void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        aiPosition.SerializeState(stream, info);
    }

     [PunRPC]
     public void recieveDamage(float amount)
     {
         if (photonView.isMine) {
             aiStats.loseHealth(amount);
         }
     }

     [PunRPC]
     public void destroyThisPlayer()
     {
         Destroy(gameObject);
     }

     public void requestXPOnDeath(Player shotMe)
     {
         myXpTimer.addComponent(shotMe);
         myXpTimer.getRidOfExtras();
     }
}
