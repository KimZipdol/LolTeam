using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    private static Gamemanager _instance = null;
    public static Gamemanager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(Gamemanager)) as Gamemanager;

                if (_instance == null)
                {
                    Debug.LogError("There's no active GameManager Object");
                }
            }
            return _instance;
        }
    }

    //경험치 수식 =  280 + (현재 레벨 - 1) x 100

    #region Money 프로퍼티
        private int money;
        public int Money
        {
            get { return money; }
            set { money = value;}
        }
    #endregion

    #region Money 증가메소드 (돈 증가 이벤트 발생할때 호출!)
        public void Moneyfersecond(int MONEY)
        {
            this.Money += MONEY;
        }
    #endregion

    public enum GameState { Start , End}
    public GameState gameState = GameState.End;
    public GameObject Minisoldier = null;
    public int soldireMaxCount;
    public List<GameObject> SoldireList = new List<GameObject>();

    public Dictionary<string, ItemInfoMation> iteminfodic = new Dictionary<string, ItemInfoMation>();
    public Sprite[] ITEMIMGS;
    //0 = 검 , 1 = 도끼 , 2 = 갑옷 , 3 = 반지 , 4 = 포션 , 5 = 장갑
    public Text[] ItemText;
    public Transform itemText_Panel;
    public Image item_Paenl_img;
    public Image item_temp;



    private Coroutine moneysecond;



    public void ItemInfo(ItemInfoMation info)
    {
        ItemText[0].text = "아이템 이름 : " + info.Itemname;
        ItemText[1].text = "아이템 설명 : " + info.Description;
        ItemText[2].text = "아이템 가격 : " + info.Price.ToString();
        item_Paenl_img.sprite = info.ItemImg;
        itemText_Panel.transform.position = Input.mousePosition + Vector3.right * 10 - Vector3.up*300;
    }
    

    private void Start()
    {
        
        itemText_Panel.gameObject.SetActive(false);
        gameState = GameState.Start;
        moneysecond = StartCoroutine(MoneyCorutine());

        ObjectfullInit();

        #region 아이템 딕셔너리 Add
        iteminfodic.Add(
            ITEMIMGS[0].name, 
            new ItemInfoMation(
            ItemKind.WEAPON,
            "검",
            "이가 다 낡아 빠진 Sword 구입하면 후회할거야...",
            ITEMIMGS[0], 10));

        iteminfodic.Add(
            ITEMIMGS[1].name,
            new ItemInfoMation(
            ItemKind.WEAPON,
            "도끼",
            "이가 다 낡아 빠진 Axe 구입하면 후회할거야...",
            ITEMIMGS[1], 20));

        iteminfodic.Add(
           ITEMIMGS[2].name,
           new ItemInfoMation(
           ItemKind.ARMOR,
           "갑옷",
           "구멍이 슝슝 빠져있는 Armor",
           ITEMIMGS[2], 25));

        iteminfodic.Add(
           ITEMIMGS[3].name,
           new ItemInfoMation(
           ItemKind.ASR,
           "반지",
           "아무 능력도 없는 반지...",
           ITEMIMGS[3], 15));

        iteminfodic.Add(
           ITEMIMGS[4].name,
           new ItemInfoMation(
           ItemKind.POTION,
           "체력회복약",
           "체력을 조금 회복해준다.",
           ITEMIMGS[4], 30));

        iteminfodic.Add(
           ITEMIMGS[5].name,
           new ItemInfoMation(
           ItemKind.ARMOR,
           "장갑",
           "손이 전혀 방어가 안된다..",
           ITEMIMGS[5], 12));



        #endregion





        //  StartCoroutine(objectfullinpect());
    }


    public void ObjectfullInit()
    {
        //max 수치만큼 반복문
        for (int i = 0; i < soldireMaxCount; i++)
        {
            //병사 생성
            GameObject unit = Instantiate(Minisoldier, this.transform);
            //병사 비활성 
            unit.SetActive(false);
            //병사들 리스트에 Add
            SoldireList.Add(unit);
        }
    }

    //돌격병 나오는 코루틴
    IEnumerator objectfullinpect()
    {
        yield return new WaitForSeconds(Random.Range(1.0f,2.0f));

        for (int i = 0; i < SoldireList.Count; i++)
        {
            if (!SoldireList[i].gameObject.activeSelf)
            {
                SoldireList[i].gameObject.SetActive(true);
                break;
            }
            else
            {
                break;
            }
        }
        yield return null;
    }




    //돈 
    IEnumerator MoneyCorutine()
    {
        while (gameState == GameState.Start)    
        {
            Moneyfersecond(1);
            yield return  new WaitForSeconds(1.5f);
          //  print(Money);
            
            yield return null;
        }
    }








}
