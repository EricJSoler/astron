using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    [PunRPC]
    public void shootFoward(int bulletSpeed, Vector3 direction)
    {
        Rigidbody rb;
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = direction * bulletSpeed;


    }

    void OnCollisionEnter(Collision col)
    {

        //if (col.gameObject.tag == "GunItem") {
        //	myInventory.photonView.RPC("setGun",PhotonTargets.All,(col.gameObject.GetComponent<Gun>().name));
        //}

        //on your own game?
        if (col.gameObject.tag == "Player") {

            Player hitPlayer = col.gameObject.GetComponent<Player>();
            hitPlayer.recieveDamage(25);

        }
        else if (col.gameObject.tag == "Enemy") {
            MeleeEnemy enemy = col.gameObject.GetComponent<MeleeEnemy>();
            enemy.health -= 25;
        }

        Destroy(gameObject);

    }


}
