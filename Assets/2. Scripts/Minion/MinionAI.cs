using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAI : MonoBehaviour {

    public enum State { IDLE, TRACE, ATTACK, DIE}
    public State _state = State.IDLE;
    public bool isDie = false;


    void Start () {
		
	}

	void Update () {
		
	}
}
