using UnityEngine;
using System.Collections;

public abstract class WeapStateLoadedClip : WeaponStateI {

    public abstract override void shoot();

    public abstract override void aim();

    public abstract override void reload();

    public abstract override void drop();

    public abstract override void switchFrom();

}
