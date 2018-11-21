using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{

    //내멋대로 수식 
    void Start()
    {
        Maxhp = Mathf.RoundToInt((70 * Lev * 0.5f));
        Hp = Maxhp;
        print(Hp);
        Atk = Random.Range(10 * Lev, 20 * Lev);
    }

   

}
