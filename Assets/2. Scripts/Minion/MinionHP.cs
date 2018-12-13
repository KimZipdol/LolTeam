using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//미니언은 체력을 가지고 있고 일정이상 데미지 받으면 죽는다
//미니언 체력바 구현

public class MinionHP : MonoBehaviour
{
    public float hp = 100f;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }


}