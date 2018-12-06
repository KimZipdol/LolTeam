using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
	public GameObject _Player;
	public Animator _Anim;
	public Text QCoolText;
	public Text WCoolText;
	public Text ECoolText;
	public Text RCoolText;
    public Image QCoolImage;
    public Image WCoolImage;
    public Image ECoolImage;
    public Image RCoolImage;
    public Picking _picking;

	[Header("Object Pool")]
	public GameObject QExpEffect;

	private float QRadius = 3.0f;
	private float QCooltime = 5.0f;
	private float QTime = 5.0f;
	private int HashQ = Animator.StringToHash("Q");
	private GameObject QEffect;
	private int HashW = Animator.StringToHash("W");
	private float WCooltime = 8.0f;
	private float WTime = 8.0f;
	private int HashE = Animator.StringToHash("E");
	private int HashR = Animator.StringToHash("R");

    private float QFillAmount = 1;
    private float SkillTimes = 10.0f;

    private void Awake()
	{
		GameObject effectPools = new GameObject("EffectPools");

		var obj = Instantiate<GameObject>(QExpEffect, effectPools.transform);
		obj.name = "QEffect";
		obj.SetActive(false);
		QEffect = obj;
	}

	private void Update()
	{
		CoolTimeChk();

		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//RaycastHit _hit;
		//if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
		//{
		//	QEffect.transform.position = _hit.point;
		//	QEffect.transform.localScale = Vector3.one * QRadius;
		//}
		//Quaternion qRot = Quaternion.LookRotation(_hit.point - _Player.transform.position);
		//_Player.transform.rotation = Quaternion.Slerp(_Player.transform.rotation, qRot, Time.deltaTime * 5.0f);
		//Debug.Log(_hit.point.ToString());

		if (Input.GetButtonDown("QSkill") && QTime >= QCooltime)
		{
			_picking._isMove = false;
			StartCoroutine(SkillRot());
			StartCoroutine(QSkill());
            
             StartCoroutine(QImgCool());
        }
		WTime += Time.deltaTime;
		if (Input.GetButtonDown("WSkill") && WTime >= WCooltime)
		{
			_picking._isMove = false;
			StartCoroutine(SkillRot());
			StartCoroutine(WSkill());
		}
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
		//ETime += Time.deltaTime;
		//RTime += Time.deltaTime;


	}

	IEnumerator SkillRot()
	{
		float rotTime = 0.0f;
		while (true)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit _hit;
			if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
			{
				QEffect.transform.position = _hit.point;
				QEffect.transform.localScale = Vector3.one * QRadius;
			}
			Quaternion qRot = Quaternion.LookRotation(_hit.point - _Player.transform.position);
			_Player.transform.rotation = Quaternion.Slerp(_Player.transform.rotation, qRot, Time.deltaTime * 100.0f);
			rotTime += Time.deltaTime;
			if (rotTime >= 0.1f)
				break;
		}
		yield return null;
	}
   
    IEnumerator QImgCool()
    {
        

        while (true)
        {
            if (QFillAmount <= 0.0f) break;
            yield return new WaitForSeconds(0.02f);
            SkillTimes -= Time.deltaTime*5;
            QFillAmount = SkillTimes * 0.1f;

            QCoolImage.fillAmount = QFillAmount;
            Debug.Log(QFillAmount);

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
		//Collider[] colls = Physics.OverlapSphere(QEffect.transform.position, QRadius);
		//foreach (var item in colls)
		//{
		//
		//}
		yield return new WaitForSeconds(1.3f);
		QEffect.SetActive(false);
	}

	IEnumerator WSkill()
	{
		WTime = 0.0f;
		_Anim.SetTrigger(HashW);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit _hit;

		if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
		{
			
		}
		
		yield return new WaitForSeconds(1.8f);
	}

}
