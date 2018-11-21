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
    public int Price;                   //아이템 가격 


    public ItemInfoMation() { }

    public ItemInfoMation(ItemKind itemKind,string itemname,
        string Description, Sprite itemImg,int Price)
    {
        this.itemkind = itemKind;
        this.Itemname = itemname;
        this.Description = Description;
        this.ItemImg = itemImg;
        this.Price = Price;
    }



}
public class ItemSlot : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{

    public ItemInfoMation iteminfo = new ItemInfoMation();

    private Sprite childsprite;

    private void Awake()
    {
        childsprite = transform.GetChild(0).GetComponent<Image>().sprite;
    }

    //마우스 포인터가 들어왔을때
    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var item in Gamemanager.Instance.iteminfodic)
        {
            if (childsprite.name == item.Value.ItemImg.name)
            {
                Gamemanager.Instance.itemText_Panel.gameObject.SetActive(true);
                Gamemanager.Instance.ItemInfo(item.Value);
            }
        }
    }

    //마우스 포인터가 빠져나왔을때
    public void OnPointerExit(PointerEventData eventData)
    {
        Gamemanager.Instance.itemText_Panel.gameObject.SetActive(false);
    }

    //클릭햇을때
    public void OnPointerClick(PointerEventData eventData)
    {
        Gamemanager.Instance.item_temp.sprite = childsprite;


    }
}