using UnityEngine;
using System.Collections;

public abstract class BaseAICharacterClass : BaseCharacterClass {

   override public int level
    {
        get { return m_stats.level; }
        set { m_stats.level = value; }
    }    
}
