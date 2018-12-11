using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityStat : MonoBehaviour {

    public GameObject LvlUpParticle;
    public Text atkText;
    public Text defText;
    public Text hpText = null;
    public Slider hpImg;
    public Text mpText = null;
    public Slider mpImg;

    //롤 캐릭터 '직스'의 스탯을 가져옴. 상황에 맞게 수정해야함.
    float currHp;
    float currMp;
    public float initHp = 536.0f;
    private float hpGrowth = 92.0f;
    public float initMp = 480.0f;
    private float mpGrowth = 23.5f;
    public float initAtk = 54.2f;
    private float atkGrowth = 3.1f;
    private float initASpd = 0.656f;
    private float aSpdGrowthRate = 1.017f;
    //hp, mp 회복은 5초당 회복량임.
    private float initHpRegen = 6.26f;
    private float hpRegenGrowth = 0.6f;
    private float initMpRegen = 8.0f;
    private float mpRegenGrowth = 0.8f;
    //방어력, 마법 저항력 절대수치에 따른 데미지 감소량 계산 필요.
    public float initDef = 21.54f;
    private float defGrowth = 3.3f;
    private float initRegi = 30.0f;
    private float regiGrowth = 0.5f;
    //엔진 에디터에 맞게 공격속도와 사거리 수정 필요.
    private float moveSpd = 325.0f;
    private float atkRange = 550.0f;

    [HideInInspector]
    public ParticleSystem[] particles;

    private void Awake()
    {
        currHp = initHp;
        currMp = initMp;
        RefreshStat();
        RefreshHpMp();

        particles = LvlUpParticle.GetComponentsInChildren<ParticleSystem>();
      

        LvlUpParticle.SetActive(false);

    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    LvlUp();
        //}
    }

    public void LvlUp()
    {
        initHp += hpGrowth;
        currHp += hpGrowth;
        initMp += mpGrowth;
        currMp += mpGrowth;
        initHpRegen += hpRegenGrowth;
        initMpRegen += mpRegenGrowth;
        initAtk += atkGrowth;
        initASpd *= aSpdGrowthRate;
        initDef += defGrowth;
        initRegi += regiGrowth;
        RefreshStat();
        RefreshHpMp();
        StartCoroutine(LvlUpEffect());
    }

    void RefreshStat()
    {
        atkText.text = "공격력 : " + (int)initAtk;
        defText.text = "방어력 : " + (int)initDef;
    }

    void RefreshHpMp()
    {
        hpText.text = (int)currHp + " / " + (int)initHp + "  ( + " + initHpRegen + ")";
        hpImg.value = currHp / initHp;
        mpText.text = (int)currMp + " / " + (int)initMp + "  ( + " + initMpRegen + ")";
        mpImg.value = currMp / initMp;
    }

    IEnumerator LvlUpEffect()
    {
        LvlUpParticle.SetActive(true);
        //LvlUpParticle.GetComponentsInChildren<ParticleSystem>()[0].Play();
        //LvlUpParticle.GetComponentsInChildren<ParticleSystem>()[1].Play();
        yield return new WaitForSeconds(3.0f);
        LvlUpParticle.SetActive(false);
    }

}
