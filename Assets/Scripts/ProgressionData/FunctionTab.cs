using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionTab : MonoBehaviour
{

    public GameObject tab1;
    public GameObject tab2;
    [SerializeField]
    private DataJeu dataJeu;
    public GameObject tab3;
    [SerializeField]
    private DataEquipement dataEquipement;
    [SerializeField]
    private LearningPathDisplay dataLeaning;
    public Image buttonTab1;
    public Image buttonTab2;
    public Image buttonTab3;
    private GameObject activateTab;
    private Image activateImage;
    private Color colorBackground;

    void Start(){
        activateTab = tab1;
        activateImage = buttonTab1;
        colorBackground = new Color(0.6627f,0.6627f,0.6627f);
        activateImage.color = colorBackground;
    }

    public void Tab1Clicked(){
        activateTab.SetActive(false);
        tab1.SetActive(true);
        activateTab=tab1;
        activateImage.color=Color.white;
        buttonTab1.color = colorBackground;
        activateImage = buttonTab1;
        dataLeaning.Initialised();
    }
    public void Tab2Clicked(){
        activateTab.SetActive(false);
        tab2.SetActive(true);
        activateTab=tab2;
        activateImage.color=Color.white;
        buttonTab2.color = colorBackground;
        activateImage = buttonTab2;
        dataJeu.Initialised();
    }
    public void Tab3Clicked(){
        activateTab.SetActive(false);
        tab3.SetActive(true);
        activateTab=tab3;
        activateImage.color=Color.white;
        buttonTab3.color = colorBackground;
        activateImage = buttonTab3;
        dataEquipement.Initialised();
    }

    public void ReloadCurrentTab(){
        if(activateTab==tab1)
            this.Tab1Clicked();
        if(activateTab==tab2)
            this.Tab2Clicked();
        if(activateTab==tab3)
            this.Tab3Clicked();
    }
}
