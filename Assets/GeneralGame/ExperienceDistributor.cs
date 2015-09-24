using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExperienceDistributor
{

    // Use this for initialization
    public static void addExperience(Player killer, Player killed)
    {

    }

    public static void addExperience(Player killer, int killedLevel)
    {

        int killerLev = killer.sCharacterClass.level;
        int experienceGained;

        if (killerLev < killedLevel) {
            experienceGained = (killedLevel - killerLev) * 10 + 50;
        }
        else if (killerLev > killedLevel)
            experienceGained = (int)(50 - (10 * (killerLev - killedLevel)));
        else
            experienceGained = 50;

        killer.sCharacterClass.gainExperience(experienceGained);
    }
}

//uh i think il probably redo this
public class XpTimer
{
    int timeForAssist = 30;
    int timeForKill = 5;
    struct TimerContainer
    {
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
        int updateTimeHere = -1;
        int i = 0;
        foreach (TimerContainer element in myList) {
            if (element.player == shot) {
                updateTimeHere = i;
                break;
            }
            i++;
        }
        if (updateTimeHere != -1) {
            myList.RemoveAt(updateTimeHere);
        }
        TimerContainer added = new TimerContainer();
        added.player = shot;
        added.time = Time.time;
        myList.Add(added);
    }

    public void getRidOfExtras()
    {
        List<TimerContainer> updatedList = new List<TimerContainer>();
        foreach (TimerContainer element in myList) {
            if (Time.time - element.time < timeForAssist) {
                updatedList.Add(element);
            }
        }
        myList = updatedList;
    }

    public void iDied(int level, float timeOfDeath)
    {
        foreach (TimerContainer element in myList) {
            if (timeForKill >= timeOfDeath - element.time) {
                if (element.player != null)
                    ExperienceDistributor.addExperience(element.player, level + 1);
            }
            else if (timeForAssist >= timeOfDeath - element.time) {
                if (element.player != null)
                    ExperienceDistributor.addExperience(element.player, level);
            }
        }
    }
}
