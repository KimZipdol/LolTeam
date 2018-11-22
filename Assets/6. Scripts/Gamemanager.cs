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


    public Sprite[] ITEMIMGS;
    //0 = 검 , 1 = 도끼 , 2 = 갑옷 , 3 = 반지 , 4 = 포션 , 5 = 장갑
    public Text[] ItemText;
    public List<Image> InvenSlot;
    public Transform itemText_Panel;
    public Image item_Paenl_img;


    private Coroutine moneysecond;

    public List<ItemInfoMation> inventoryData = new List<ItemInfoMation>();

    public PlayerController player;

    //상점 데이터배이스 딕셔너리
    public Dictionary<string, ItemInfoMation> iteminfodic = new Dictionary<string, ItemInfoMation>();
    //상점 아이템 구입시 
    public Dictionary<string, ItemInfoMation> Inventorydic = new Dictionary<string, ItemInfoMation>();


    public Sprite TempSprite;


    //인자값을 클래스로 받아와서 
    
    public void ItemInfo(ItemInfoMation info)
    {
        //아이템 정보  = 이름
        ItemText[0].text = "아이템 이름 : " + info.Itemname;
        //아이템 정보  = 설명
        ItemText[1].text = "아이템 설명 : " + info.Description;
        //아이템 정보  = 가격
        ItemText[2].text = "아이템 가격 : " + info.Price.ToString();
        //아이템 정보  = 이미지sprite
        item_Paenl_img.sprite = info.ItemImg;
        //아이템정보 패널의 위치를 마우스주위로 조정...
        //itemText_Panel.transform.position = Input.mousePosition + Vector3.right * 8 - Vector3.up*280;
    }

    float width;
    float height;

    private void Update()
    {

        //활성화상태일때
        if (itemText_Panel.gameObject.activeSelf)
            itemText_Panel.transform.position = Input.mousePosition + 
                    Vector3.right * (width * 0.1f) - Vector3.up * (height*1.3f);

    }


    private void Start()
    {
        //인벤 아이템 슬롯 이미지컴포넌트에 접근해서 리스트에 저장
        InvenSlot.AddRange(GameObject.Find("Champ_Inventory").GetComponentsInChildren<Image>());
        //0번째 제거
        InvenSlot.RemoveAt(0);
        itemText_Panel.gameObject.SetActive(false);
        gameState = GameState.Start;
        moneysecond = StartCoroutine(MoneyCorutine());

        ObjectfullInit();

        width = itemText_Panel.GetComponent<RectTransform>().rect.width;
        height = itemText_Panel.GetComponent<RectTransform>().rect.height;




        //  StartCoroutine(objectfullinpect());
    }
    private void OnEnable()
    {
        #region 아이템 상점 아이템 데이터베이스 Adds
        iteminfodic.Add(
            ITEMIMGS[0].name,                                         //  아이템 이름 키값          
            new ItemInfoMation(
            ItemKind.WEAPON,                                          //  아이템 종류 = 무기
            "검",                                                     //   아이템 이름
            "이가 다 낡아 빠진 Sword 구입하면 후회할거야...",              //   아이템 설명 
            ITEMIMGS[0],                                              //   아이템 스프라이트 
            7,                                                        //   아이템 챔피언 능력상승치
            10));                                                     //   아이템 구입가격
        //========================================================================================//
        iteminfodic.Add(
            ITEMIMGS[1].name,                                        //  아이템 이름 키값           
            new ItemInfoMation(
            ItemKind.WEAPON,                                         //  아이템 종류 = 무기
            "도끼",                                                   //   아이템 이름
            "이가 다 낡아 빠진 Axe 구입하면 후회할거야...",               //   아이템 설명 
            ITEMIMGS[1],                                             //   아이템 스프라이트 
            9,                                                       //   아이템 챔피언 능력상승치
            20));                                                    //   아이템 구입가격
        //========================================================================================//
        iteminfodic.Add(
           ITEMIMGS[2].name,                                         //  아이템 이름 키값           
           new ItemInfoMation(
           ItemKind.ARMOR,                                           //  아이템 종류 = 무기
           "갑옷",                                                    //   아이템 이름
           "구멍이 슝슝 빠져있는 Armor",                                //   아이템 설명 
           ITEMIMGS[2],                                              //   아이템 스프라이트 
           8,                                                        //   아이템 챔피언 능력상승치
           25));                                                     //   아이템 구입가격
        //========================================================================================//
        iteminfodic.Add(
           ITEMIMGS[3].name,                                         //  아이템 이름 키값           
           new ItemInfoMation(
           ItemKind.ASR,                                             //  아이템 종류 = 무기
           "반지",                                                    //   아이템 이름
           "아무 능력도 없는 반지...",                                  //   아이템 설명 
           ITEMIMGS[3],                                              //   아이템 스프라이트 
           3,                                                        //   아이템 챔피언 능력상승치
           15));                                                     //   아이템 구입가격
        //========================================================================================//
        iteminfodic.Add(
           ITEMIMGS[4].name,                                        //  아이템 이름 키값        
           new ItemInfoMation(
           ItemKind.POTION,                                         //  아이템 종류 = 무기
           "체력회복약",                                              //   아이템 이름
           "체력을 조금 회복해준다.",                                  //   아이템 설명 
           ITEMIMGS[4],                                             //   아이템 스프라이트 
           30,                                                      //   아이템 챔피언 능력상승치
           30));                                                    //   아이템 구입가격
        //========================================================================================//
        iteminfodic.Add(
           ITEMIMGS[5].name,                                        //  아이템 이름 키값        
           new ItemInfoMation(
           ItemKind.ARMOR,                                          //  아이템 종류 = 무기
           "장갑",                                                   //   아이템 이름
           "손이 전혀 방어가 안된다..",                                //   아이템 설명 
           ITEMIMGS[5],                                             //   아이템 스프라이트 
           4,                                                       //   아이템 챔피언 능력상승치
           12));                                                    //   아이템 구입가격
        //========================================================================================//
        iteminfodic.Add(
          ITEMIMGS[6].name,                                        //  아이템 이름 키값        
          new ItemInfoMation(
          ItemKind.ARMOR,                                          //  아이템 종류 = 무기
          "투구",                                                   //   아이템 이름
          "머리를 보호해주는 강철깡통",                                //   아이템 설명 
          ITEMIMGS[6],                                             //   아이템 스프라이트 
          7,                                                       //   아이템 챔피언 능력상승치
          20));
        //========================================================================================//
        iteminfodic.Add(
          ITEMIMGS[7].name,                                        //  아이템 이름 키값        
          new ItemInfoMation(
          ItemKind.ARMOR,                                          //  아이템 종류 = 무기
          "나무 방패",                                              //   아이템 이름
          "나무로 만들어졌는데 방어가되니...",                          //   아이템 설명 
          ITEMIMGS[7],                                             //   아이템 스프라이트 
          10,                                                       //   아이템 챔피언 능력상승치
          35));
        //========================================================================================//
        iteminfodic.Add(
          ITEMIMGS[8].name,                                        //  아이템 이름 키값        
          new ItemInfoMation(
          ItemKind.ASR,                                            //  아이템 종류 = 무기
          "네크리스 목걸이",                                          //   아이템 이름
          "목걸이니라",                                              //   아이템 설명 
          ITEMIMGS[8],                                             //   아이템 스프라이트 
          5,                                                       //   아이템 챔피언 능력상승치
          15));
        //========================================================================================//
        iteminfodic.Add(
          ITEMIMGS[9].name,                                        //  아이템 이름 키값        
          new ItemInfoMation(
          ItemKind.ARMOR,                                          //  아이템 종류 = 무기
          "pant",                                                  //   아이템 이름
          "바지...",                                                //   아이템 설명 
          ITEMIMGS[9],                                             //   아이템 스프라이트 
          3,                                                       //   아이템 챔피언 능력상승치
          10));






        #endregion
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


    public int InventoryTempIdx()
    {
        int idx =0;
        for (int i = 0; i < InvenSlot.Count; i++)
        {
            if (InvenSlot[i].sprite == TempSprite)
            {
                idx = i;
                break;
            }
            else
            {
                continue;
            }
        }
        return idx;
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
