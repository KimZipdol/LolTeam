using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


                //대기상태 추척상태  공격상태   사망  
public enum State { Idle , Trace , Attack , Dead }
public class Minion_red : Unit {
    //public LayerMask minionColor;
    private Transform tr;
    private Quaternion FirstRot;
    private NavMeshAgent _agent;

    public State state = State.Idle;
    

    private RectTransform HpSprite;

    private void Start()
    {
        tr = GetComponent<Transform>();
        //HpSprite = Instantiate(Gamemanager.Instance.EnemyHpPrefeb) as RectTransform;
        //HpSprite.SetParent(GameObject.Find("EnemyHp_Canvas").transform);
        FirstRot = tr.rotation;
        
    }

    private void FixedUpdate()
    {
        tr.rotation = FirstRot;

        Vector2 pos = Camera.main.WorldToScreenPoint(tr.position);
        HpSprite.transform.position = pos;

        HpSprite.transform.position += new Vector3(0, 70, 0);
    }







}
