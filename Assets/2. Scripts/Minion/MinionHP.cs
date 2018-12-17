using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//미니언은 체력을 가지고 있고 일정이상 데미지 받으면 죽는다
//미니언 체력바 구현

public class MinionHP : MonoBehaviour
{
    private const string bulletTag = "BULLET"; 
    public float hp = 100.0f;
    private float initHp = 100.0f;

    private Canvas uiCanvas;
    private Image hpBarImage;
    private GameObject hpBar;
    public GameObject hpBarPrefab;

    private void Start()
    {
        SetHpBar();
    }

    void SetHpBar()
    {
        //uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
 //       hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        //hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];//0번방에는 까만색 
        //var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        //_hpBar.targetTr = this.gameObject.transform;
        //_hpBar.offset = hpBarOffset;

        ShowHpBar();
    }

    public void ShowHpBar()
    {
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];//0번방에는 까만색 
        hpBarImage.GetComponentsInParent<Image>()[1].color = Color.black; //'s' 붙이기
        hpBarImage.fillAmount = hp / initHp;
       // var _hpBar = hpBar.GetComponent<EnemyHpBar>();

        //_hpBar.targetTr = this.gameObject.transform;
        //_hpBar.offset = hpBarOffset;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == bulletTag)
        {
            
            //Destroy(coll.gameObject);
            coll.gameObject.SetActive(false);
            //hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
 //          hpBarImage.fillAmount = hp / initHp; //현재 깎인 hp를 초기hp로 나누면 그만큼만 채워서 보여줘
            if (hp <= 0.0f)
            {
                GetComponent<MinionAI>().state = MinionState.DIE;
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                //clear는 (0,0,0,0) 완전 투명하게 된다.
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }


}