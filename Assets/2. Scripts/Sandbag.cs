using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//그냥 소환해서 데미지 넣는것 확인용 허수아비.
public class Sandbag : MonoBehaviour
{
	private Transform tr;
	private RectTransform HpSprite;
	private Image hpImage;
	public float maxHp = 300.0f;
	private float currHp;

	private void Start()
	{
		tr = GetComponent<Transform>();
		HpSprite = Instantiate(Gamemanager.Instance.EnemyHpPrefeb) as RectTransform;
		HpSprite.SetParent(GameObject.Find("EnemyHp_Canvas").transform);
		tr.rotation = Quaternion.Euler(Vector3.back);
		hpImage = HpSprite.GetComponentsInChildren<Image>()[1];
		currHp = maxHp;
	}

	private void FixedUpdate()
	{
		Vector2 pos = Camera.main.WorldToScreenPoint(tr.position);
		HpSprite.transform.position = pos;

		HpSprite.transform.position += new Vector3(0, 70, 0);
		hpImage.fillAmount = currHp / maxHp;
		if (currHp<=0)
		{
			currHp = maxHp;
		}
	}

	void GetDamage(float damage)
	{
		currHp -= damage;
	}

}
