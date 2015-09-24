using UnityEngine;
using System.Collections;

public class WeaponBase : Photon.MonoBehaviour {

    WeaponI m_Weapon;
    public WeaponI Weapon
    {
        get
        {
            if (m_Weapon == null) {
                m_Weapon = GetComponent<WeaponI>();
            }
            return m_Weapon;
        }
    }

    WeaponVisualsI m_visuals;
    public WeaponVisualsI Visuals
    {
        get
        {
            if (m_visuals == null) {
                m_visuals = GetComponent<WeaponVisualsI>();
            }
            return m_visuals;
        }
    }
}
