using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : Unit
{
    public delegate void ExpHandler(int n);         
    public static ExpHandler ExpEvent;

    private UnityStat unityStat;
  


    //내멋대로 수식 
    void Start()
    {
        Lev = 1;
        MaxExp = 30;
        Exp = 0;
        Maxhp = Mathf.RoundToInt((70 * Lev * 0.5f));
        Hp = Maxhp;
        print(Hp);
        Atk = Random.Range(10 * Lev, 20 * Lev);
        Def = 3;
        unityStat = GetComponent<UnityStat>();
    }

    private void OnEnable()
    {
        ExpEvent += AddExp;
    }

    private void Update()
    {
        //경험치바 게이지가 꽉 차 있다면 
        if (Gamemanager.Instance.Expslider.value == 1)
        {
            PlayerExpReset();
        }
    }
    void PlayerExpReset()
    {
        MaxExp += 30;                    //일단은 맥스경험치를 현재에서 30으로  
        Exp = 0;
        Gamemanager.Instance.Expslider.value = 0;
        Gamemanager.Instance.PlayerLevText.text = ++Lev + "";

        unityStat.LvlUp();


    }

    //적 잡으면 이놈 호출 하셈. (PlayerController.ExpEvent(획득경험치))

    public void AddExp(int MinionExp)
    {
        if (MaxExp > Exp)
        {
            Exp += MinionExp;                    //현재 경험치에 적이 가지고 있는 경험치를 더한다.
            Gamemanager.Instance.Expslider.DOValue(Exp / MaxExp, 1);
        }
    }

  



}

