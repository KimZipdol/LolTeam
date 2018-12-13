using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10f; //물리력 / 총알 이동 속도
    public float damage = 20.0f; //공격력

    private Rigidbody bulletRigidbody;


	void Start () {
        bulletRigidbody = GetComponent<Rigidbody>();

        bulletRigidbody.velocity = transform.forward * speed;


    }




}


