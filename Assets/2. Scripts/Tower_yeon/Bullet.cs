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

    //트리거 충돌시 자동실행(상대방 콜라이더 감지)
    private void OnTriggerEnter(Collider other)
    {
        // ****** Minion 태그 만들어 달 예정 _yeon1123
        if(other.gameObject.tag == "Minion")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            
            //****** PlayerController스크립에 Die 함수 만들예정 _yeon 1123
            //playerController.Die();
        }
    }


}


