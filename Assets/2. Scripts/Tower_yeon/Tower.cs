using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public LayerMask targetMinionColor;
    public const float attckRadius = 10.0f; //공격범위 타워반경 10m //const하니까 인스펙터뷰에 노출안되네
    public float createRateMin = 0.5f; //최소 생성주기
    public float createRateMax = 3f; //최대 생성주기

    private Transform target; //발사할 대상
    private float createRate; //생성주기
    private float timeAfterCreate; //최근 생성 시점에서 지난 시간

    void Start()
    {
        timeAfterCreate = 0f; //타이머 리셋
        createRate = Random.Range(createRateMin, createRateMax);

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attckRadius);
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
    }


    IEnumerator FindingMinion()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attckRadius, targetMinionColor);
        foreach (var item in colls)
        {
            //Debug.Log(item.name);

            Vector3 dir = item.gameObject.transform.position - transform.position;
            Quaternion attckDir = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = attckDir;

        }
        yield return null;
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
