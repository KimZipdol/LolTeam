using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public GameObject bulletPrefab; // 생성할 총알 원본 프리팹
    public float createRateMin = 0.5f; //최소 생성주기
    public float createRateMax = 3f; //최대 생성주기

    private Transform target; //발사할 대상
    private float createRate; //생성주기
    private float timeAfterCreate; //최근 생성 시점에서 지난 시간

    void Start()
    {
        timeAfterCreate = 0f; //타이머 리셋
        createRate = Random.Range(createRateMin, createRateMax);
        //************************타겟 수정할예정_yeon
        target = FindObjectOfType<PlayerController>().transform; //수정

    }


    void Update()
    {
        //Time.deltaTime은 직전의 Update와 현재 Update 실행 시점 사이의 시간 간격
        timeAfterCreate = timeAfterCreate + Time.deltaTime;
        // 누적된 시간이 생성 주기보다 크거나 같다
        if (timeAfterCreate >= createRate)
        {
            timeAfterCreate = 0f; // 누적된 시간을 리셋

            // bulletPrefab의 복제본을 생성
            // 위치와 회전은 총알 생성기 자신의 위치와 회전으로 지정.
            // 생성된 총알 복제본을 bullet 이라는 변수로 다루기
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            // 총알이 target을 바라보도록 회전
            bullet.transform.LookAt(target);

            // 다음번 생성까지 주기를 랜덤하게
            createRate = Random.Range(createRateMin, createRateMax);
        }
    }
}
