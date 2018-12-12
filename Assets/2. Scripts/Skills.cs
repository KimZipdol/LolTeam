using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
	public GameObject _Player;
	public Animator _Anim;
	public float abillityPower;
	public float atkDamage;
    public bool isPassiveOn = true;
    public Text passiveCoolText;
	public Text QCoolText;
	public Text WCoolText;
	public Text ECoolText;
	public Text RCoolText;
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

    private int SkillPoint = 1;
    private float passiveCool = 12.0f;
    private float QRadius = 3.0f;
	private float QCooltime = 5.0f;
	private float QTime = 5.0f;
	private int HashQ = Animator.StringToHash("Q");
	private GameObject QEffect;
	private float qDamage;
	private int qLvl = 1;
	private int HashW = Animator.StringToHash("W");
	private float WCooltime = 5.0f;
	private float WTime = 5.0f;
	private float wDamage;
	private int wLvl = 0;
	private bool isRolling = false;
    private float moved = 0.0f;
    private int HashE = Animator.StringToHash("E");
	private int HashR = Animator.StringToHash("R");

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
		obj.name = "QEffect";
		obj.SetActive(false);
		QEffect = obj;
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
        //RTime += Time.deltaTime;


    }

 
   
    IEnumerator QImgCool()
    {
        QFillAmount = 1;
        SkillTimes1 = 5;

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
		_Anim.SetTrigger(HashQ);
		QEffect.SetActive(true);
		QCoolText.gameObject.SetActive(true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
        {
            QEffect.transform.position = _hit.point;
            QEffect.transform.localScale = Vector3.one * QRadius;
        }
        Quaternion qRot = Quaternion.LookRotation(_hit.point - _Player.transform.position);
        _Player.transform.rotation = Quaternion.Slerp(_Player.transform.rotation, qRot, Time.deltaTime * 100.0f);
        Collider[] colls = Physics.OverlapSphere(QEffect.transform.position, QRadius/2);
        foreach (var item in colls)
        {
			if(item.gameObject.layer==9)
			{
				item.SendMessage("GetDamage", qDamage, SendMessageOptions.RequireReceiver);
			}
        }

        yield return new WaitForSeconds(1.3f);
		QEffect.SetActive(false);
	}
    IEnumerator ShowQEffect()
    {
        QEffect.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        QEffect.SetActive(false);
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



}
