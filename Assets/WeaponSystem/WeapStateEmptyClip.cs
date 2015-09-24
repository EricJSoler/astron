using UnityEngine;
using System.Collections;

public abstract class WeapStateEmptyClip : WeaponStateI {
    public abstract override void reload();
    public abstract override void aim();
    public abstract override void switchFrom();
    public abstract override void drop();
	
}
