using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ItemKind { NONE, WEAPON, ARMOR, ASR, POTION }

[System.Serializable]
public class ItemInfoMation
{
    public ItemKind itemkind;           //아이템 종류
    public string Itemname;             //아이템 이름 
    public string Description;          //아이템 설명
    public Sprite ItemImg;              //아이템 이미지
    public int itemfoIdx;               //아이템 능력치 하나 오름.
    public int Price;                   //아이템 가격 


    public ItemInfoMation() { }

    public ItemInfoMation(ItemKind itemKind,string itemname,
        string Description, Sprite itemImg, int itemfoIdx ,int Price)
    {
        this.itemkind = itemKind;
        this.Itemname = itemname;
        this.Description = Description;
        this.ItemImg = itemImg;
        this.itemfoIdx = itemfoIdx;
        this.Price = Price;
    }



}
public class ItemSlot : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{

    public ItemInfoMation iteminfo = new ItemInfoMation();

    [SerializeField]
    private Sprite childsprite;
    private ChampStat champ;

    private void Awake()
    {
        childsprite = transform.GetChild(0).GetComponent<Image>().sprite;
        champ = FindObjectOfType<ChampStat>();
    }

    //마우스 포인터가 들어왔을때
    public void OnPointerEnter(PointerEventData eventData)
    {
        //상점 데이터배이스 검사 
        foreach (var item in ItemDatabase.Instance.iteminfodic)
        {
            //아이템의 이미지이름과 딕셔너리에 저장되어있는 이미지이름으로 검사    
            if (childsprite.name == item.Value.ItemImg.name)
            {
                //같은 이미지가 저장되어있다면 아이템 정보 패널 켜준다.
                Gamemanager.Instance.itemText_Panel.gameObject.SetActive(true);
                //매니저스크립트의 함수호출
                Gamemanager.Instance.ItemInfo(item.Value);
            }
        }
    }

    //마우스 포인터가 빠져나왔을때
    public void OnPointerExit(PointerEventData eventData)
    {
        //아이템 정보 패넣 꺼준다.
        Gamemanager.Instance.itemText_Panel.gameObject.SetActive(false);
    }






    public static int iCount = 0;
    //클릭햇을때
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Gamemanager.Instance.InvenSlot.Count - 1 < iCount) return;


        foreach (var item in ItemDatabase.Instance.iteminfodic)
        {

            if (item.Value.ItemImg == childsprite)
            {
                
                // Gamemanager.Instance.Inventorydic.Add(childsprite.name, item.Value);
                if (Gamemanager.Instance.InvenSlot.Count > iCount)
                {


                    Gamemanager.Instance.InvenSlot[Gamemanager.Instance.InventoryTempIdx()].sprite = item.Value.ItemImg;
                    Gamemanager.Instance.inventoryData.Add(item.Value);
                    ++iCount;
                    // Debug.Log(iCount);


                    if (Gamemanager.Instance.inventoryData
                        [Gamemanager.Instance.inventoryData.Count - 1].itemkind == ItemKind.WEAPON)
                    {
                        champ.player.Atk += Gamemanager.Instance.inventoryData
                        [Gamemanager.Instance.inventoryData.Count - 1].itemfoIdx;
                    }

                    else if (Gamemanager.Instance.inventoryData
                        [Gamemanager.Instance.inventoryData.Count - 1].itemkind == ItemKind.ARMOR)
                    {
                        champ.player.Def += Gamemanager.Instance.inventoryData
                        [Gamemanager.Instance.inventoryData.Count - 1].itemfoIdx;
                    }


                    //foreach (var item2 in Gamemanager.Instance.inventoryData)
                    //{
                    //    if (item2.itemkind == ItemKind.WEAPON)
                    //    {
                    //        Gamemanager.Instance.player.Atk += item2.itemfoIdx;
                    //        break;

                    //    }
                    //    else if (item2.itemkind == ItemKind.ARMOR)
                    //    {
                    //        Gamemanager.Instance.player.Def += item2.itemfoIdx;
                    //        break;
                    //    }
                    //    else if (item2.itemkind == ItemKind.ASR)
                    //    {


                    //    }
                    //    else if (item2.itemkind == ItemKind.POTION)
                    //    {

                    //    }

                    //}


                    break;
                }
                //Debug.Log(Gamemanager.Instance.Inventorydic[item.Key].Itemname);
               
                
            }
        }

    }
}