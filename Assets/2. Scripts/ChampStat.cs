using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ChampStat : MonoBehaviour {



    public RectTransform SHOP_panel;
    

    public Text[] ChampStat_text;

    public PlayerController player;

    bool shop_paenlMove = false;

    private void Update()
    {
        ChampStat_text[0].text = "공격력 : " + player.Atk;
        ChampStat_text[1].text = "방어력 : " + player.Def;



        if (Input.GetKeyDown(KeyCode.P))
        {
            shop_paenlMove = !shop_paenlMove;

            
        }


        if (!shop_paenlMove)
        {
            SHOP_panel.DOMoveX(0, 1.0f);
        }
        else
        {
            SHOP_panel.DOMoveX(840, 1.0f);
        }




    }







}
