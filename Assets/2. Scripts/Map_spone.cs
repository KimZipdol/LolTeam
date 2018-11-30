using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_spone : MonoBehaviour {

    public GameObject player;

    public Vector2 MyPlayerPos;

    public RectTransform myposimg;


    private void Awake()
    {
        

        
    }

    private void Update()
    {
       //MyPlayerPos = Camera.main.WorldToScreenPoint(player.transform.position + Vector3.up * 1.5f);

       // myposimg.transform.position = MyPlayerPos;


        MyPlayerPos = new Vector2(player.transform.position.x - 250.0f, player.transform.position.z - 250.0f);

        myposimg.anchoredPosition = MyPlayerPos;

    }


}
