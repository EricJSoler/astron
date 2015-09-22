using UnityEngine;
using System.Collections;

public abstract class AIBase : Photon.MonoBehaviour {

    AIEnemy m_AIEnemy;
    public AIEnemy aiEnemy 
    {
        get
        {
            if (m_AIEnemy == null) {
                m_AIEnemy = GetComponent<AIEnemy>();
            }
            return m_AIEnemy;
        }
    }
    
    AINavigation m_AINavigation;
    public AINavigation aiNavigation
    {
        get
        {
            if (m_AINavigation == null) {
                m_AINavigation = GetComponent<AINavigation>();
            }
            return m_AINavigation;
        }
    }


    AIPosition m_AIPosition;
    public AIPosition aiPosition
    {
        get
        {
            if (m_AIPosition == null) {
                m_AIPosition = GetComponent<AIPosition>();
            }
            return m_AIPosition;
        }
    }

    AIStats m_AIStats;
    public AIStats aiStats
    {
        get
        {
            if (m_AIStats == null) {
                m_AIStats = GetComponent<AIStats>();
            }
            return m_AIStats;
        }
    }

    AIVisuals m_AIVisuals;
    public AIVisuals aiVisual
    {
        get
        {
            if (m_AIVisuals == null) {
                m_AIVisuals = GetComponent<AIVisuals>();
            }
            return m_AIVisuals;
        }
    }
}
