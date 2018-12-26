using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAtk : MonoBehaviour 
{
    public GameObject atkThrowPrefab;
    public GameObject damageText;
    public UnityStat unityStat;
    public Skills skills;
    public Material passiveMat;
    public float maxThrow = 3f;
    public Animator _anim;

    int HashAtk = Animator.StringToHash("BasicAtk");
    GameObject[] throwPools = new GameObject[3];
    int throwIdx = 0;

    GameObject pThrow;
    float passiveDamage;
    float atkDamage;
    float abillityPower = 0.0f;
    int lvl = 1;

    private void Start()
    {
        atkDamage = unityStat.initAtk;
        GameObject atkPools = new GameObject("AtkPools");
        for(int i=0;i<maxThrow;i++)
        {
            var obj = Instantiate<GameObject>(atkThrowPrefab, atkPools.transform);
            obj.name = "Throw_" + i.ToString("0");
            obj.SetActive(false);
            throwPools[i] = obj;
        }
        pThrow = Instantiate<GameObject>(atkThrowPrefab);
        pThrow.name = "PassiveThrow";
        pThrow.SetActive(false);
        pThrow.GetComponent<MeshRenderer>().material = passiveMat;

    }

    private void Update()
    {
        if(lvl!=unityStat.level)
        {
            lvl++;
            RefreshSpec();
        }
    }

    public void RefreshSpec()
    {
        atkDamage = unityStat.initAtk;
        abillityPower = unityStat.abillityPower;
        passiveDamage = 20 + ((unityStat.level - 1) * 4) + (abillityPower * 0.4f);
    }

    void BasicAttack(GameObject target)
    {
        //target에 따른 데미지 전. 패시브 온이면 패시브 데미지 추가, 건물이면 주문력에따른 추가뎀 추가, 데미지텍스트
        if(target.CompareTag("Red_Minion"))
        {
            if(skills.isPassiveOn)
            {
                target.SendMessage("GetDamage", atkDamage + passiveDamage);
                skills.isPassiveOn = false;
            }
            else
            {
                target.SendMessage("GetDamage", atkDamage);
            }
        }
        else if(target.CompareTag("Red_Building"))
        {
            if (skills.isPassiveOn)
            {
                target.SendMessage("GetDamage", atkDamage + passiveDamage + (abillityPower * 0.6));
                skills.isPassiveOn = false;
            }
            else
            {
                target.SendMessage("GetDamage", atkDamage + (abillityPower * 0.6));
            }
        }

        StartCoroutine(AtkAnim());

        //target에 미사일 발사. 가급적 포물선, 패시브 온이면 색바꿔서 티나게, 타겟에 데미지 텍스트
        if(skills.isPassiveOn)
        {
            pThrow.SetActive(true);
            pThrow.SendMessage("flyToTarget", target);
        }
        else
        {
            throwPools[throwIdx].SetActive(true);
            throwPools[throwIdx++].SendMessage("flyToTarget", target);
        }
    }

    IEnumerator AtkAnim()
    {
        _anim.SetBool(HashAtk, true);
        yield return new WaitForSeconds(0.4f); //평타애니의 최소값. 수정 필요할 수 있음
        _anim.SetBool(HashAtk, false);
    }

}
