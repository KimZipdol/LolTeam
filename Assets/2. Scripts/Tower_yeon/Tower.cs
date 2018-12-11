using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [Header("오브젝트풀_Bullet Object Pool")]
    public GameObject bulletPrefab; // 생성할 총알 원본 프리팹
    public int maxBulletPool = 10;
    public List<GameObject> bulletPool = new List<GameObject>();

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

        CreateBullet(transform.position);

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
            StartCoroutine(FindingMinion());
        if (timeAfterCreate >= createRate)
        {
            timeAfterCreate = 0f; // 누적된 시간을 리셋

            // 다음번 생성까지 주기를 랜덤하게
            createRate = Random.Range(createRateMin, createRateMax);
        }
    }


    IEnumerator FindingMinion()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attckRadius);
        foreach (var item in colls)
        {
            Debug.Log("태그검출 if문 진입전");
            if(item.gameObject.CompareTag("Minion_Red"))
            {
                Debug.Log("태그검출 if문 진입");
                Vector3 dir = item.gameObject.transform.position - transform.position;
                Quaternion attckDir = Quaternion.LookRotation(dir, Vector3.forward);

                transform.rotation = attckDir;

            }


        }
        yield return null;
    }

    public void CreateBullet(Vector3 pos)
    {
        GameObject objectPools = new GameObject("BulletObjectPools");

        for (int i = 0; i < maxBulletPool; i++)
        {
            var obj = Instantiate<GameObject>(bulletPrefab, objectPools.transform);
            obj.name = "Bullet_" + i.ToString("00"); //ToString(출력형식) >> i값을 출력하는데 두자리 숫자로 출력해
            obj.SetActive(false); //해당오브젝트를 비활성화
            bulletPool.Add(obj);
        }
    }
}
