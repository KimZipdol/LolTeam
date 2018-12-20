using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TowerState { Idle, Attack };

    public LayerMask targetMinionColor;
    public const float attckRadius = 5.0f; //공격범위 타워반경 10m //const하니까 인스펙터뷰에 노출안되네
    private float createRateMin = 0.5f; //최소 생성주기
    private float createRateMax = 3f; //최대 생성주기

    public Transform target; //발사할 대상:미니언
    public Transform tempTarget; //(타겟-미니언)간의 거리를 구하기 위한 임시변수
    private float createRate; //생성주기
    private float timeAfterCreate; //최근 생성 시점에서 지난 시간

    public TowerState state = TowerState.Idle;
    private float dis; //타워와 미니언 사이의 거리

    private float BulletMoveSpeed=1.0f;

   

    public Transform firePos;

    void Start()
    {
       // towerMgr = FindObjectOfType<TowerMgr>();

        //timeAfterCreate = 0f; //타이머 리셋
        //createRate = Random.Range(createRateMin, createRateMax);
        StartCoroutine(FindingMinion());
        StartCoroutine(towerAction());
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attckRadius*2);
    }

    void Update()
    {
        //Time.deltaTime은 직전의 Update와 현재 Update 실행 시점 사이의 시간 간격
        timeAfterCreate = timeAfterCreate + Time.deltaTime;
        // 누적된 시간이 생성 주기보다 크거나 같다
        if (timeAfterCreate >= createRate)
        {
            timeAfterCreate = 0f; // 누적된 시간을 리셋

            // 다음번 생성까지 주기를 랜덤하게
            createRate = Random.Range(createRateMin, createRateMax);
        }

        if (target != null && state == TowerState.Attack)
        {
            Vector3 dir = target.gameObject.transform.position - transform.position;
            Quaternion attckDir = Quaternion.LookRotation(dir.normalized);
            transform.rotation = attckDir;


            if (AttackTIme >= 2)
            {
                AttackTIme = 0;

                GameObject bullet = TowerMgr.Instance.GetBullet();
                bullet.transform.position = firePos.position;
                bullet.SetActive(true);

                GameObject t = Bullet.bulletEvent(target.gameObject);

                StartCoroutine(BulletShot(bullet,t));

            }

        }
      




    }

    IEnumerator BulletShot(GameObject bullet,GameObject t)
    {
       
        while (true)
        {
            bullet.transform.position = Vector3.Lerp(
                bullet.transform.position,
                 t.transform.position, Time.deltaTime * BulletMoveSpeed);

            Vector3 dir = t.transform.position - bullet.transform.position;
            Quaternion rot = Quaternion.LookRotation(dir.normalized);
            bullet.transform.rotation = rot;




            yield return new WaitForSeconds(0.02f);
            yield return null;
        }
    }



    IEnumerator FindingMinion()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f); //타워 상태변화 하는데 걸리는 최소시간

            Collider[] colls = Physics.OverlapSphere(transform.position, attckRadius*2 , targetMinionColor);
            Debug.Log("몇명 : " + colls.Length);
            if (colls.Length < 1 )
            {
                state = TowerState.Idle;
                target = null;
            }
           
                foreach (var item in colls)
                {
                  //  Debug.Log(item.name);

                    state = TowerState.Attack;


                    //검출된 미니언과 타워자신과의 거리
                    dis = Vector3.Distance(transform.position, item.transform.position);
                    if (colls.Length == 1) //검출된 오브젝트가 한 명이라면
                    {
                        tempTarget = item.transform;

                    }
                    else  //2명 이상 들어오면 (타겟-미니언)간의 거리들을 비교해서 배열 순서를 재배치해야함
                    {
                        //i) 동시에 들어올경우
                        if (target == null)
                        {
                            tempTarget = colls[0].transform;

                        }
                        else if (target != null)
                        //ii) 각자 들어올경우
                        {
                            if (dis < Vector3.Distance(transform.position, target.transform.position))
                            {
                                tempTarget = item.transform;

                            }


                        }

                    }
                    target = tempTarget;

                }
            
            yield return null;
        }

    }



    public float AttackTIme = 0;


    IEnumerator towerAction()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            switch (state)
            {
                case TowerState.Idle:
                    target = null;
                    break;
                case TowerState.Attack:
                    AttackTIme += Time.deltaTime;
                    break;
                default:
                    break;
            }
        }
    }



    //트리거 충돌시 자동실행(상대방 콜라이더 감지)
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FindingMinion());
        if (other.gameObject.layer == targetMinionColor)
        {
            MinionHP minionHp = other.GetComponent<MinionHP>();
            float damage = 10;
            minionHp.TakeDamage(damage);
        }
    }

}
