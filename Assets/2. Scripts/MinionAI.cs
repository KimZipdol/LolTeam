using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinionState { IDLE, TRACE, ATTACK, DIE }
public class MinionAI : MonoBehaviour {

    public MinionState state = MinionState.IDLE;
    public bool isDie = false;

    void Update() {
        switch (state)
        {
            case MinionState.IDLE:

                break;
            case MinionState.TRACE:


                break;
            case MinionState.ATTACK:


                break;
            case MinionState.DIE:


                break;
        }
    }	
}
