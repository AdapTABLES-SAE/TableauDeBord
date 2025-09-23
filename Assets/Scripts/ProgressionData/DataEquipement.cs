using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class DataEquipement : MonoBehaviour
{
    public GameObject boughtArmure;
    public GameObject boughtCasque;
    public GameObject boughtPantalon;
    public GameObject boughtBouclier;
    public GameObject boughtEpaulière;
    public GameObject boughtSac;
    public GameObject boughtLanterne;
    public GameObject boughtBousole;
    public GameObject boughtBottes;
    public GameObject boughtFiole;
    public GameObject boughtBracForce;
    public GameObject boughtCeintForce;
    public GameObject boughtGantForce;
    public GameObject boughtPassePartout;
    public GameObject boughtManteau;
    public GameObject boughtCarotte;
    public GameObject activateArmure;
    public GameObject activateCasque;
    public GameObject activatePantalon;
    public GameObject activateBouclier;
    public GameObject activateEpaulière;
    public GameObject activateSac;
    public GameObject activateLanterne;
    public GameObject activateBousole;
    public GameObject activateBottes;
    public GameObject activateFiole;
    public GameObject activateBracForce;
    public GameObject activateCeintForce;
    public GameObject activateGantForce;
    public GameObject activatePassePartout;
    public GameObject activateManteau;
    public GameObject activateCarotte;
    public TMP_Text textQte;

    public void Initialised(){
        activateArmure.SetActive(false);
        activateBottes.SetActive(false);
        activateBouclier.SetActive(false);
        activateBousole.SetActive(false);
        activateBracForce.SetActive(false);
        activateCarotte.SetActive(false);
        activateCasque.SetActive(false);
        activateCeintForce.SetActive(false);
        activateEpaulière.SetActive(false);
        activateFiole.SetActive(false);
        activateGantForce.SetActive(false);
        activateLanterne.SetActive(false);
        activateManteau.SetActive(false);
        activatePantalon.SetActive(false);
        activatePassePartout.SetActive(false);
        activateSac.SetActive(false);
        boughtArmure.SetActive(true);
        boughtBottes.SetActive(true);
        boughtBouclier.SetActive(true);
        boughtBousole.SetActive(true);
        boughtBracForce.SetActive(true);
        boughtCarotte.SetActive(true);
        boughtCasque.SetActive(true);
        boughtCeintForce.SetActive(true);
        boughtEpaulière.SetActive(true);
        boughtFiole.SetActive(true);
        boughtGantForce.SetActive(true);
        boughtLanterne.SetActive(true);
        boughtManteau.SetActive(true);
        boughtPantalon.SetActive(true);
        boughtPassePartout.SetActive(true);
        boughtSac.SetActive(true);
        StartCoroutine(APIManager.FetchEquipments(SetText));
    }

    private void SetText(EquipmentsJSON equipments){
        List<ItemJSON> items = new List<ItemJSON>(equipments.items);
        foreach(ItemJSON itemJSON in items){
            Item item = new Item(itemJSON);
            switch(item.id)
            {
                case Item_ID.ARMOR:
                {
                        if (item.isBought)
                        {
                            boughtArmure.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateArmure.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.HELMET:
                {
                        if (item.isBought)
                        {
                            boughtCasque.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateCasque.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.PANTS:
                {
                        if (item.isBought)
                        {
                            boughtPantalon.SetActive(false);
                            if (!item.isActivated)
                            {
                                activatePantalon.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.SHIELD:
                {
                        if (item.isBought)
                        {
                            boughtBouclier.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateBouclier.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.SHOULDERS:
                {
                        if (item.isBought)
                        {
                            boughtEpaulière.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateEpaulière.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.BAG:
                {
                        if (item.isBought)
                        {
                            boughtSac.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateSac.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.LANTERN:
                {
                        if (item.isBought)
                        {
                            boughtLanterne.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateLanterne.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.COMPAS:
                {
                        if (item.isBought)
                        {
                            boughtBousole.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateBousole.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.BOOTS:
                {
                        if (item.isBought)
                        {
                            boughtBottes.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateBottes.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.HP:
                {
                        if (item.isBought)
                        {
                            boughtFiole.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateFiole.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.BRACELET:
                {
                    if (item.isBought)
                    {
                            boughtBracForce.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateBracForce.SetActive(true);
                            }
                    }
                    break;
                }
                case Item_ID.BELT:
                {
                        if (item.isBought) 
                        { 
                            boughtCeintForce.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateCeintForce.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.GLOVE:
                {
                        if (item.isBought)
                        {
                            boughtGantForce.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateGantForce.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.KEYS:
                {
                        if (item.isBought)
                        {
                            boughtPassePartout.SetActive(false);
                            if (!item.isActivated)
                            {
                                activatePassePartout.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.COAT:
                {
                        if (item.isBought)
                        {
                            boughtManteau.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateManteau.SetActive(true);
                            }
                        }
                    break;
                }
                case Item_ID.CARROT:
                {
                        if (item.isBought)
                        {
                            boughtCarotte.SetActive(false);
                            if (!item.isActivated)
                            {
                                activateCarotte.SetActive(true);
                            }
                        }
                    break;
                }
            }
        }
    }
}
