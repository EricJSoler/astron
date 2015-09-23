using UnityEngine;
using System.Collections;


//THe position classes depend on this for slowing their movement speed
// or gravity based on the enviroment
public class EnviromentSettings : MonoBehaviour {

    public static EnviromentSettings enviroment;
    public float downAccel;
    public float movementSpeedSlow;

    void Awake()
    {
        enviroment = this;
    }
}
