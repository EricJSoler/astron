using UnityEngine;
using System.Collections;

public static class ExperienceDistributor {

	// Use this for initialization
	public static void addExperience(Player killer, Player killed)
    {

    }

    public static void addExperience(Player killer, int killedLevel)
    {

        int killerLev = killer.sCharacterClass.level;
        int experienceGained;

        if (killerLev < killedLevel) {
            experienceGained = killedLevel - killerLev * 10 + 50;
        }
        else if (killerLev > killedLevel)
            experienceGained = (int)(50 - (10 * (killerLev - killedLevel)));
        else
            experienceGained = 50;

        killer.sCharacterClass.gainExperience(experienceGained);
    }
}
