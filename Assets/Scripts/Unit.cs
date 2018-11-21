using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {


    public int Maxhp;

    #region Hp 프로퍼티

    private int hp;
    public int Hp
    {
        get { return hp; }
        set { if (hp >= 0) hp = value; }
    }



    #endregion

    #region Mp 프로퍼티
    private int mp;
    public int Mp
    {
        get { return mp; }
        set { if (mp >= 0) mp = value; }
    }
    #endregion

    #region Atk 프로퍼티
    private int atk;
    public int Atk
    {
        get { return atk; }
        set { atk = value; }
    }
    #endregion


    protected int Lev = 1;
    protected int MaxLev = 18;

    



}
