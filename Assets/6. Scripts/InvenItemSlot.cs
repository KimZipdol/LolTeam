using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenItemSlot : MonoBehaviour, IPointerClickHandler
{

    public Image childImg;

    private ItemInfoMation iteminfo;


    void Start () {
        childImg = transform.GetChild(0).GetComponent<Image>();
    }
	
	
    //포인터 클릭헸을때
    public void OnPointerClick(PointerEventData eventData)
    {
        --ItemSlot.iCount;
        foreach (var item in Gamemanager.Instance.inventoryData)
        {
            if (item.ItemImg == childImg.sprite)
            {
                childImg.sprite = Gamemanager.Instance.TempSprite;
                iteminfo = item;

                if (item.itemkind == ItemKind.WEAPON)
                    Gamemanager.Instance.player.Atk -= item.itemfoIdx;
                else if (item.itemkind == ItemKind.ARMOR)
                    Gamemanager.Instance.player.Def -= item.itemfoIdx;

                break;
            }
        }

        Gamemanager.Instance.inventoryData.Remove(iteminfo);




    }

}
