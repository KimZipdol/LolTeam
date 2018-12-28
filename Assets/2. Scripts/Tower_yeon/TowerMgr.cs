using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMgr : MonoBehaviour {
    public static TowerMgr instance = null;

    [Header("***Bullet Object Pool***")]
    public GameObject bulletPrefab; // 생성할 총알 원본 프리팹
    public int maxBulletPool = 20;
    public List<GameObject> bulletPool = new List<GameObject>();


    [Header("***Minion Object Pool***")]
    public int maxMinionPool = 30;
    public GameObject MinionPrefabRed;
    public GameObject MinionPrefabBlue;
    public Transform spawnPoint; //미니언 생성될 위치
    public float createTime = 1.0f;
    public List<GameObject> RedPool = new List<GameObject>();
    public List<GameObject> BluePool = new List<GameObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //인스턴스에 자기자신을 넣어라
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        spawnPoint = GameObject.FindWithTag("Nexus_Blue").GetComponent<Transform>();
        CreateMinionPool();
        CreateBulletPool();
        //Debug.Log(spawnPoint.name);

    }


    void Start()
    {
        StartCoroutine(this.CreateBlueMinion());
        StartCoroutine(this.CreateRedMinion());

    }

    IEnumerator CreateBlueMinion()
    {

        //태그말고 레이어로 찾고싶은데 모르겠다....
        int minionCount = (int)GameObject.FindGameObjectsWithTag("Minion_Blue").Length;
        Debug.Log("블루 에너미카운트!:" + minionCount);
        if (minionCount < maxMinionPool)
        {
            yield return new WaitForSeconds(createTime);
        }
        yield return null;
    }
    /// <summary>
    /// Creates the red minion.
    /// </summary>
    /// <returns>The red minion.</returns>
    IEnumerator CreateRedMinion()
    {

        int minionCount = (int)GameObject.FindGameObjectsWithTag("Minion_Blue").Length;
        Debug.Log("레드 에너미카운트!:" + minionCount);
        if (minionCount < maxMinionPool)
        {
            yield return new WaitForSeconds(createTime);
        }
        yield return null;
    }


    /// <summary>
    /// Gets the minion.
    /// </summary>
    /// <returns>The minion.</returns>
    /// <param name="bluered">If set to <c>true</c> bluered.</param>
    public GameObject GetMinion(bool bluered)
    {
        if ( bluered )
        {
            for (int i = 0; i < BluePool.Count; i++)
            {
                if (BluePool[i].activeSelf == false)
                {
                    return BluePool[i]; //한개만 리턴하면 바로 종료된다.
                }
            }
        }
        else
        {
            for (int i = 0; i < RedPool.Count; i++)
            {
                if (RedPool[i].activeSelf == false)
                {
                    return RedPool[i]; //한개만 리턴하면 바로 종료된다.
                }
            }
        }
        return null;
    }
    /// <summary>
    /// Gets the blue minion.
    /// </summary>
    /// <returns>The blue minion.</returns>
    public GameObject GetBlueMinion()
    {
        for (int i = 0; i < BluePool.Count; i++)
        {
            if (BluePool[i].activeSelf == false)
            {
                return BluePool[i]; //한개만 리턴하면 바로 종료된다.
            }
        }
        return null;
    }
    public GameObject GetRedMinion()
    {
        for (int i = 0; i < RedPool.Count; i++)
        {
            if (RedPool[i].activeSelf == false)
            {
                return RedPool[i]; //한개만 리턴하면 바로 종료된다.
            }
        }
        return null;
    }



    public void CreateMinionPool()
    {
        GameObject RedMinionPools = new GameObject("RedMinionPools");
        GameObject BlueMinionPools = new GameObject("BlueMinionPools");

        for (int i = 0; i < maxMinionPool; i++)
        {
            var obj = Instantiate<GameObject>(MinionPrefabRed, RedMinionPools.transform);
            obj.name = "RedMinion_" + i.ToString("00");
            obj.SetActive(false);
            RedPool.Add(obj);

            var obj2 = Instantiate<GameObject>(MinionPrefabBlue, BlueMinionPools.transform);
            obj2.name = "BlueMinion_" + i.ToString("00");
            obj2.SetActive(false);
            BluePool.Add(obj2);
        }

       
        for (int i = 0; i < maxMinionPool; i++)
        {
           
        }
    }

    public void CreateBulletPool()
    {
        GameObject objectPools = new GameObject("Bullets Pools"); //빈게임오브젝트는 타워 개수만큼 생성된다

        for (int i = 0; i < maxBulletPool; i++)
        {
            var obj = Instantiate<GameObject>(bulletPrefab, objectPools.transform);
            obj.name = "Bullet_" + i.ToString("00"); //ToString(출력형식) >> i값을 출력하는데 두자리 숫자로 출력해
            obj.SetActive(false); //해당오브젝트를 비활성화
            bulletPool.Add(obj);
        }
    }
}
