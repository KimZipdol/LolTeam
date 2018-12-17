using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlueNexus : MonoBehaviour
{
    public bool isMinionReady = false;

    private void Start()
    {

    }

    public void Update()
    {

        if (isMinionReady == true && TowerMgr.instance.BluePool.Count > GameObject.FindGameObjectsWithTag("Minion_Blue").Length )
        {
            for (int i = 0; i < 10; i++)
            {
                MinionReady();

            }
            isMinionReady = false;
        }

    }

    public void MinionReady()
    {

        var blueMinion = TowerMgr.instance.GetBlueMinion(); //참조복사 /클래스의 인스턴스끼리 대입연산 하면 레퍼런스카피 일어남
        blueMinion.transform.position = transform.position;
        blueMinion.SetActive(true);
    }

}

