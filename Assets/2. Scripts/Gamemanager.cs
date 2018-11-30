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

    
    public Text[] ItemText;
    public List<Image> InvenSlot;
    public Transform itemText_Panel;
    public Image item_Paenl_img;


    private Coroutine moneysecond;

    public List<ItemInfoMation> inventoryData = new List<ItemInfoMation>();

    

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
                    Vector3.right * (width * 0.1f) - Vector3.up * (height*1.0f);

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
