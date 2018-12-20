using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    public delegate GameObject BulletHandler(GameObject target);
    public static BulletHandler bulletEvent;




    public float speed = 10f; //물리력 / 총알 이동 속도
    public float damage = 20.0f; //공격력

    private Rigidbody bulletRigidbody;


    public GameObject target;

    GameObject  bulletMoved(GameObject target)
    {
        this.target = target;
        return this.target;
    }



    void BulletMv(GameObject bullet, GameObject target)
    {
        bullet.transform.position = Vector3.Lerp(
              bullet.transform.position,
               target.transform.position, Time.deltaTime * 2);
    }


    private void Awake()
    {

        bulletEvent += bulletMoved;
       // bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
       // bulletRigidbody.velocity = transform.forward * speed;
        

    }


    





}


