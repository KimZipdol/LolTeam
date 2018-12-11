using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Skills : MonoBehaviour
{
	public GameObject _Player;
	public Animator _Anim;
	public Text QCoolText;
	public Text WCoolText;
	public float WDist = 12.0f;
	public float WSpd = 30.0f;
	public Text ECoolText;
	public Text RCoolText;
	public Picking _picking;

	[Header("Object Pool")]
	public GameObject QExpEffect;

	private int SillPoint = 1;
	private float QRadius = 3.0f;
	private float QCooltime = 5.0f;
	private float QTime = 5.0f;
	private int HashQ = Animator.StringToHash("Q");
	private GameObject QEffect;
	private int QLvl = 0;
	private int HashW = Animator.StringToHash("W");
	private float WCooltime = 8.0f;
	private float WTime = 8.0f;
	private int WLvl = 0;
	private bool isRolling = false;
	private float moved = 0.0f;
	private int HashE = Animator.StringToHash("E");
	private int HashR = Animator.StringToHash("R");

	

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

		if (Input.GetButtonDown("QSkill") && QTime >= QCooltime)
		{
			_picking._isMove = false;
			StartCoroutine(QSkill());
		}
		if (Input.GetButtonDown("WSkill") && WTime >= WCooltime)
		{
			_picking._isMove = false;
			StartCoroutine(WSkill());
			isRolling = true;
		}

		if(isRolling)
		{
			_Player.transform.Translate(Vector3.forward * Time.deltaTime * WSpd);
			moved += Vector3.forward.magnitude * Time.deltaTime * WSpd;
			if (moved >=WDist)
			{
				isRolling = false;
				moved = 0.0f;
			}

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
		WCoolText.text = (WCooltime - WTime).ToString("0.0") + 's';
		//ETime += Time.deltaTime;
		//RTime += Time.deltaTime;


	}

	IEnumerator QSkill()
	{
		QTime = 0.0f;
		_Anim.SetTrigger(HashQ);
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
		//Collider[] colls = Physics.OverlapSphere(QEffect.transform.position, QRadius);
		//foreach (var item in colls)
		//{
		//
		//}
		yield return new WaitForSeconds(1.3f);
		//QEffect.SetActive(false);
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
}
