using UnityEngine;
using System.Collections;

public class ExplosionCleanUp : MonoBehaviour {

    float startingTime;
    float lifespan;
    void Start()
    {
        startingTime = Time.time;
        lifespan = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (startingTime + lifespan <= Time.time) {
            Destroy(gameObject);
        }
    }
}
