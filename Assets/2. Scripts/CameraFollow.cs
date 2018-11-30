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
	// Use this for initialization
	void Start () {
        firstPos = transform.position;

        apple = FindObjectOfType<Map_spone>();

    }
	
	// Update is called once per frame
	void LateUpdate () {


        screenpos = new Vector2(Screen.width / 2, Screen.height / 2);

        Vector2 mousepos = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - screenpos;

        if (mousepos.x <= -800.0f)
            Emousepos = pos.left;
        if (mousepos.x >= 800.0f)
            Emousepos = pos.right;
        if (mousepos.y <= -400.0f)
            Emousepos = pos.down;
        if (mousepos.y >= 400.0f)
            Emousepos = pos.up;

        if (mousepos.y <= -400.0f || mousepos.y >= 400.0f ||
            mousepos.x <= -800.0f || mousepos.x >= 800.0f)
        {
            // Debug.Log(mousepos);

            Vector3 dir = (player_target.position - new Vector3(mousepos.x, 0, mousepos.y)).normalized;
            dir.y = 0;
            transform.position -= dir * Time.deltaTime * 15.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = player_target.position + Vector3.up * firstPos.y;
        }

        mapCameraland.anchoredPosition = new Vector2(transform.position.x-250, transform.position.z-250);




        //맵 클릭 하면 그 위치포인트 받아와서 플레이어 이동시키기



        //if (Input.GetMouseButtonDown(1))
        //{
        //    Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Ray2D ray = new Ray2D(wp, Vector2.zero);
        //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //   // if (hit.collider != null)
        //    {
        //        //Debug.Log(hit.collider);
        //    }
        //}





        Vector2 poss = new Vector2(Input.mousePosition.x - (Screen.width - 500), Input.mousePosition.y);
        Debug.Log(poss);
        if (poss.x >= 0.0f && poss.x <= 500 && poss.y >= 0 && poss.y <= 500)
        {
            if (Input.GetMouseButtonDown(1))
            {
                
                picking.moved(picking._Agent, new Vector3(poss.x, player_target.position.y, poss.y));
               
                //picking._Agent.destination = new Vector3(poss.x ,player_target.position.y,poss.y);


            }

        }





    }
}
