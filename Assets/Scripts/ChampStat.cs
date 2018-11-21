using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChampStat : MonoBehaviour {


    public Text[] ChampStat_text;


    private void Update()
    {
        ChampStat_text[0].text = "공격력 : " + Gamemanager.Instance.player.Atk;
        ChampStat_text[1].text = "방어력 : " + Gamemanager.Instance.player.Def;
    }



}
