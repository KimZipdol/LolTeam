using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
public class CameraFollow : MonoBehaviour {


    public Transform player_target;

    Vector3 offset;


    Vector2 screenpos;


    public RectTransform mapCameraland;


    public Map_spone apple;
   



    public enum pos
    {
        none,
        left,
        right,
        up,
        down
    }

    public pos Emousepos = pos.none;



    Vector3 firstPos;

    public Picking picking;

    public RectTransform SHOP;

    ChampStat shop_paenl;


    private float left;
    private float right;
    private float up;
    private float down;


    // Use this for initialization
    void Start () {
        firstPos = transform.position;

        apple = FindObjectOfType<Map_spone>();
        shop_paenl = FindObjectOfType<ChampStat>();


        Screen.SetResolution(Screen.width, Screen.height,true);
    }
	
	// Update is called once per frame
	void LateUpdate () {

        //스크린의 중앙 지점의 벡터2
        screenpos = new Vector2(Screen.width / 2, Screen.height / 2);
       // Debug.Log("중앙 지점 : " + screenpos);
        //마우스위치의 포지션위치를 스크린의 가운데 지점을 0,0 으로 만든다.
        Vector2 mousepos = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - screenpos;
        //Debug.Log("마우스위치 : " + mousepos);

        left = (-Screen.width / 2) + (Screen.width  * 0.2f);
        right = (Screen.width / 2) - (Screen.width  * 0.2f);
        down = (-Screen.height / 2) + (Screen.height * 0.2f);
        up = (Screen.height / 2) - (Screen.height * 0.2f);


        if (mousepos.x <= left)
            Emousepos = pos.left;

        else if (mousepos.x >= right)
            Emousepos = pos.right;

        else if (mousepos.y <= down)
            Emousepos = pos.down;

        else if (mousepos.y >= up)
            Emousepos = pos.up;
        else
            Emousepos = pos.none;


        //TODO : 카메라가 맵밖으로 나가지 않도록 하기.


        if (Emousepos != pos.none)
        {
            // Debug.Log(mousepos);
            if (!shop_paenl.shop_paenlMove)
            {
                Vector3 dir = (player_target.position - new Vector3(mousepos.x, 0, mousepos.y)).normalized;
                dir.y = 0;

                transform.position -= dir * Time.deltaTime * 15.0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = player_target.position + Vector3.up * firstPos.y;
        }

        
        mapCameraland.anchoredPosition = new Vector2(transform.position.x, transform.position.z);





        Vector2 poss = new Vector2(Input.mousePosition.x - (Screen.width - 250), Input.mousePosition.y);

       // Debug.Log(poss);
        if (poss.x >= 0.0f && poss.x <= 250 && poss.y >= 0 && poss.y <= 250)
        {
            if (Input.GetMouseButtonDown(1))
            {
                picking.moved(picking._Agent, new Vector3(poss.x, player_target.position.y, poss.y));
                mapCameraland.anchoredPosition = new Vector2(transform.position.x - 125, transform.position.z - 125);
            }

        }
    }
}
