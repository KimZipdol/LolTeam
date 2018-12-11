using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemDatabase : MonoBehaviour {

    private static ItemDatabase _instance = null;
    public static ItemDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(ItemDatabase)) as ItemDatabase;

                if (_instance == null)
                {
                    Debug.LogError("There's no active ItemDatabase Object");
                }
            }
            return _instance;
        }


    }


    //상점 데이터배이스 딕셔너리
    public Dictionary<string, ItemInfoMation> iteminfodic = new Dictionary<string, ItemInfoMation>();
    //상점 아이템 구입시 
    public Dictionary<string, ItemInfoMation> Inventorydic = new Dictionary<string, ItemInfoMation>();

    public Sprite[] ITEMIMGS;
    //0 = 검 , 1 = 도끼 , 2 = 갑옷 , 3 = 반지 , 4 = 포션 , 5 = 장갑
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






}
