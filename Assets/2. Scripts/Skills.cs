using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
	public GameObject _Player;
	public Animator _Anim;
    public GameObject SkillRangeBound;
	public float abillityPower;
	public float atkDamage;
    public bool isPassiveOn = true;
    public Text passiveCoolText;
	public Text QCoolText;
	public Text WCoolText;
	public Text ECoolText;
	public Text RCoolText;
    public Text OutOfRangeText;
    public Image passiveCoolImage;
    public Image QCoolImage;
    public Image WCoolImage;
    public Image ECoolImage;
    public Image RCoolImage;
    public float WDist = 12.0f;
    public float WSpd = 30.0f;

    public Picking _picking;

	[Header("Object Pool")]
	public GameObject QExpEffect;
    public GameObject RTargetMark;

    private int SkillPoint = 1;
    private float passiveCool = 12.0f;
    private float QRadius = 3.0f;
	private float QCooltime = 5.0f;
	private float QTime = 5.0f;
	private int HashQERATK = Animator.StringToHash("QERATK");
	private GameObject ExpEffect;
	private float qDamage;
	private int qLvl = 1;
    private float qRange = 8.5f;
	private int HashW = Animator.StringToHash("W");
	private float WCooltime = 5.0f;
	private float WTime = 5.0f;
	private float wDamage;
	private int wLvl = 0;
	private bool isRolling = false;
    private float moved = 0.0f;
    private float rDamageClose;
    private float rDamageGeneral;
    private float rRadius = 4.0f;
    private float rCoolTime = 120.0f;
    private float rTime = 120.0f;
    private float rRange = /*53티모미터 = 53 * 1.5미터 = 79.5미터*/79.5f;
    private int rLvl = 1;

    private float QFillAmount = 0;
    private float WFillAmount = 0;
    private float EFillAmount = 0;
    private float RFillAmount = 0;

    private float SkillTimes1 = 10.0f;
    private float SkillTimes2 = 10.0f;
    private float SkillTimes3 = 10.0f;
    private float SkillTimes4 = 10.0f;

    private void Awake()
	{
		GameObject effectPools = new GameObject("EffectPools");

		var obj = Instantiate<GameObject>(QExpEffect, effectPools.transform);
		obj.name = "ExpEffect";
		obj.SetActive(false);
		ExpEffect = obj;
		SetSkillDmg();
	}

	private void Update()
	{
		

		if (Input.GetButtonDown("QSkill") && QFillAmount <=0)
		{
			_picking._isMove = false;
			
			StartCoroutine(QSkill());
            
             StartCoroutine(QImgCool());
        }
		WTime += Time.deltaTime;
        if (Input.GetButtonDown("WSkill") && WFillAmount <= 0)
        {
            _picking._isMove = false;
            StartCoroutine(WSkill());
            StartCoroutine(WImgCool());

            isRolling = true;
        }
        if (isRolling)
        {
            _Player.transform.Translate(Vector3.forward * Time.deltaTime * WSpd);
            moved += Vector3.forward.magnitude * Time.deltaTime * WSpd;
            if (moved >= WDist)
            {
                isRolling = false;
                moved = 0.0f;
            }

        }

        if (Input.GetButton("RSkill") && RFillAmount <= 0)
        {
            //_picking._isMove = false;
            //StartCoroutine(RSkill());
            //StartCoroutine(RImgCool());

            ShowSkillRange(rRange);
        }
    }

    void ShowSkillRange(float range)
    {
        SkillRangeBound.transform.localScale = Vector3.one * range * 0.65f; //0.65를 곱해야 사거리 1로 보이므로...
        SkillRangeBound.SetActive(true);

        //이후 WE스킬 추가예정.
        if (range == rRange)
            RTargetMark.transform.localScale = Vector3.one * rRange * 2.6f; //위와 동일.
        else(range==qRange)
            RTargetMark.transform.localScale = Vector3.one * qRange * 2.6f; //위와 동일.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if(Physics.Raycast(ray, out _hit, Mathf.Infinity))
        {
            RTargetMark.transform.position = _hit.point;
        }
                
        RTargetMark.SetActive(true);
        
    }

	void OnStatChange(float newAP, float newAD)
	{
		abillityPower = newAP;
		atkDamage = newAD;
		SetSkillDmg();
	}

	void SetSkillDmg()
	{
		qDamage = (int)(70 + ((qLvl - 1) * 35) + (abillityPower * 0.35));
        rDamageClose = (int)(300 + ((rLvl - 1) * 150) + (abillityPower * 1.1)) * 1 / 3;
        rDamageGeneral = (int)(300 + ((rLvl - 1) * 150) + (abillityPower * 1.1)) * 2 / 3;
    }


	void CoolTimeChk()
	{
        if (QTime > QCooltime)
        {
            QCoolText.gameObject.SetActive(false);
        }
        QTime += Time.deltaTime;
        QCoolText.text = (QCooltime - QTime).ToString("0.0") + 's';

        if (WTime > WCooltime)
        {
            WCoolText.gameObject.SetActive(false);
        }
        WTime += Time.deltaTime;
        WCoolText.text = (WCooltime - WTime).ToString("0.0") + 's';
        //ETime += Time.deltaTime;

        if(rTime>rCoolTime)
        {
            RCoolText.gameObject.SetActive(false);
        }
        rTime += Time.deltaTime;
        RCoolText.text = (rCoolTime - rTime).ToString("0.0") + 's';


    }
    
    void OutOfRange()
    {
        Debug.Log("사정거리를 벗어났습니다.");
    }
 
   
    IEnumerator QImgCool()
    {
        QFillAmount = 1;
        SkillTimes1 = QCooltime;

        QCoolText.gameObject.SetActive(true);
        while (true)
        {
            
            yield return new WaitForSeconds(0.02f);
            QCoolText.text = (SkillTimes1).ToString("0.0") + 's';
            QCoolImage.fillAmount = QFillAmount;

            SkillTimes1 -= Time.deltaTime*5;
            QFillAmount = SkillTimes1/5;
            Debug.Log(QFillAmount);
            if (QFillAmount <= 0.0f)
            {
                QCoolText.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }


    }



	IEnumerator QSkill()
	{
        QFillAmount = 1;
        QCoolImage.fillAmount = QFillAmount;


		QTime = 0.0f;
		_Anim.SetTrigger(HashQERATK);
		ExpEffect.SetActive(true);
		QCoolText.gameObject.SetActive(true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
        {
            ExpEffect.transform.position = _hit.point;
            ExpEffect.transform.localScale = Vector3.one * QRadius;
        }
        Quaternion qRot = Quaternion.LookRotation(_hit.point - _Player.transform.position);
        _Player.transform.rotation = Quaternion.Slerp(_Player.transform.rotation, qRot, Time.deltaTime * 100.0f);
        Collider[] colls = Physics.OverlapSphere(ExpEffect.transform.position, QRadius/2);
        foreach (var item in colls)
        {
			if(item.gameObject.layer==9)
			{
				item.SendMessage("GetDamage", qDamage, SendMessageOptions.RequireReceiver);
			}
        }

        yield return new WaitForSeconds(1.3f);
		ExpEffect.SetActive(false);
	}
    IEnumerator ShowQEffect()
    {
        ExpEffect.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        ExpEffect.SetActive(false);
    }



    IEnumerator WSkill()
    {
        WTime = 0.0f;
        WCoolText.gameObject.SetActive(true);
        _Anim.SetBool(HashW, true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        Quaternion rot;
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
        {
            rot = Quaternion.LookRotation(_hit.point - _Player.transform.position);
        }
        rot = Quaternion.LookRotation(_hit.point - _Player.transform.position);
        _Player.transform.rotation = rot;

        yield return new WaitForSeconds(0.6f);
        //yield return null;
        _Anim.SetBool(HashW, false);
    }

    IEnumerator WImgCool()
    {
        WFillAmount = 1;
        SkillTimes2 = 5;

        WCoolText.gameObject.SetActive(true);
        while (true)
        {

            yield return new WaitForSeconds(0.02f);
            WCoolText.text = (SkillTimes2).ToString("0.0") + 's';
            WCoolImage.fillAmount = WFillAmount;

            SkillTimes2 -= Time.deltaTime * 5;
            WFillAmount = SkillTimes2 / 5;
            Debug.Log(WFillAmount);
            if (WFillAmount <= 0.0f)
            {
                WCoolText.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }

    IEnumerator RImgCool()
    {
        RFillAmount = 1;
        SkillTimes1 = rCoolTime;

        QCoolText.gameObject.SetActive(true);
        while (true)
        {

            yield return new WaitForSeconds(0.02f);
            RCoolText.text = (SkillTimes1).ToString("0.0") + 's';
            RCoolImage.fillAmount = RFillAmount;

            SkillTimes1 -= Time.deltaTime * 5;
            RFillAmount = SkillTimes1 / 5;
            Debug.Log(RFillAmount);
            if (RFillAmount <= 0.0f)
            {
                RCoolText.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }



    IEnumerator RSkill()
    {
        RFillAmount = 1;
        RCoolImage.fillAmount = QFillAmount;


        rTime = 0.0f;
        _Anim.SetTrigger(HashQERATK);
        ExpEffect.SetActive(true);
        RCoolText.gameObject.SetActive(true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
        {
            ExpEffect.transform.position = _hit.point;
            ExpEffect.transform.localScale = Vector3.one * QRadius;
        }

        if((_hit.point - transform.position).magnitude>rRange)
        {
            OutOfRange();
            yield break;
        }

        Quaternion rRot = Quaternion.LookRotation(_hit.point - _Player.transform.position);
        _Player.transform.rotation = rRot;
        //원의 중앙에서 먼 적에게는 General데미지만, 가까운 적에게는 General+Close데미지(가급적 수치를 합쳐서 보낼 수 있게 해보자).
        Collider[] colls = Physics.OverlapSphere(ExpEffect.transform.position, rRadius / 2);
        foreach (var item in colls)
        {
            if (item.gameObject.layer == 9)
            {
                item.SendMessage("GetDamage", rDamageGeneral, SendMessageOptions.RequireReceiver);
            }
        }
        //해야 할 것 
        yield return new WaitForSeconds(1.3f);
        ExpEffect.SetActive(false);
    }

}
