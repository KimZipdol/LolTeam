using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    public delegate void ExpHandler(int n);         
    public static  event ExpHandler ExpEvent;



    //내멋대로 수식 
    void Start()
    {
        Lev = 1;
        Exp = 0;
        Maxhp = Mathf.RoundToInt((70 * Lev * 0.5f));
        Hp = Maxhp;
        print(Hp);
        Atk = Random.Range(10 * Lev, 20 * Lev);
        Def = 3;


    }

    private void OnEnable()
    {
        ExpEvent += AddExp;
    }



    //적 잡으면 이놈 호출 하셈. (PlayerController.ExpEvent)

    public void AddExp(int MinionExp)
    {
        Exp += MinionExp;                    //현재 경험치에 적이 가지고 있는 경험치를 더한다.
        if (MaxExp >= Exp)
        {
            MaxExp += 30;                    //일단은 맥스경험치를 현재에서 30으로  
            Exp = 0;
            ++Lev;
        }
    }


   
}