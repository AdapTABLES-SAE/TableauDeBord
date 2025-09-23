using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XCharts.Runtime;

public class LevelDisplayMore : MonoBehaviour
{
    public ObjectifDisplay parent;
    public int numberindex;
    public LevelClass level = new LevelClass("", "");
    public LevelClass leveltemp = new LevelClass("", "");
    public LevelDisplay Base;
    public TMP_Text Title;
    public Button up;
    public Button down;
    public GameObject objectLevel;
    public Toggle construTableft;
    public Toggle construTabletf;
    public Toggle construTablemx;
    public Toggle poseguall;
    public Toggle posegualr;
    public Toggle posegualm;
    public Slider MaxSlider;
    public Slider MinSlider;
    public RingChart chartSeen;
    public RingChart chartSuccess;
    public TMP_InputField inputSeenCritair;
    public TMP_InputField inputSuccessCritair;

    void Start()
    {
        Display();
    }

    public void SetParent(ObjectifDisplay learner){
        parent = learner;
    }

    public void SetBase(LevelDisplay Base){
        this.Base=Base;
    }

    public void SetParameters(int numberindex, LevelClass level){
        this.numberindex=numberindex;
        this.level=level;
    }

    public void Display(){
        if(level.name!="")
            Title.text = level.name;
        else
            Title.text = "L"+parent.numberindex+numberindex;
        if(numberindex==0)
            up.interactable = false;
        else
            up.interactable = true;
        if(numberindex == parent.numberChild-1)
            down.interactable = false;
        else
            down.interactable = true;
        leveltemp=level;
        chartSeen.GetSerie(0).data[0].data[0] = (float) level.SeenNumber;
        chartSeen.GetSerie(0).data[0].data[1] = (float) level.SeenWanted;
        chartSuccess.GetSerie(0).data[0].data[0] = (float) level.SuccessNumber;
        chartSuccess.GetSerie(0).data[0].data[1] = (float) level.SuccessWanted;
        inputSeenCritair.text=""+level.SeenWanted;
        inputSuccessCritair.text=""+level.SuccessWanted;
        switch(level.construTable){
            case LeftFacteurEnum.FACTEUR_TABLE : 
                construTableft.isOn=true;
                break;
            case LeftFacteurEnum.TABLE_FACTEUR :
                construTabletf.isOn=true;
                break;
            case LeftFacteurEnum.MIX :
                construTablemx.isOn=true;
                break;
        }
        switch(level.posEgal){
            case LeftEqualEnum.RIGHT_EQUAL : 
                posegualr.isOn=true;
                break;
            case LeftEqualEnum.LEFT_EQUAL :
                poseguall.isOn=true;
                break;
            case LeftEqualEnum.MIX :
                posegualm.isOn=true;
                break;
        }

        MaxSlider.value = (float) level.intervalMax;
        MinSlider.value = (float) level.intervalMin;
    }

    public void upButton(){
        parent.up(numberindex);
    }

    public void downButton(){
        parent.down(numberindex);
    }

    public void deleteButton(){
        parent.Delete(numberindex,this);
    }

    public void modifyButton(){
        parent.Modify(numberindex,this);
    }

    public void BaseInfoButton(){
        Base.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetActive(bool isbool){
        objectLevel.SetActive(isbool);
    }

    public void construTableftpress(){
        leveltemp.construTable=LeftFacteurEnum.FACTEUR_TABLE;
    }
    public void construTabletfpres(){
        leveltemp.construTable=LeftFacteurEnum.TABLE_FACTEUR;
    }
    public void construTablemxpress(){
        leveltemp.construTable=LeftFacteurEnum.MIX;
    }
    public void poseguallpress(){
        leveltemp.posEgal=LeftEqualEnum.LEFT_EQUAL;
    }
    public void posegualrpress(){
        leveltemp.posEgal=LeftEqualEnum.RIGHT_EQUAL;
    }
    public void posegualmpress(){
        leveltemp.posEgal=LeftEqualEnum.MIX;
    }
    public void wantedValuechangeSeen(){
        leveltemp.SeenWanted=int.Parse(inputSeenCritair.text);
    }
    public void wantedValuechangeSuccess(){
        leveltemp.SuccessWanted=int.Parse(inputSuccessCritair.text);
    }
    public void changeValueMinSlider(TMP_Text text){
        if(MinSlider.value>MaxSlider.value){
            MinSlider.value=MaxSlider.value;
        }
        text.text=""+MinSlider.value;
        level.intervalMin=(int) MinSlider.value;
    }
    public void changeValueMaxSlider(TMP_Text text){
        if(MaxSlider.value<MinSlider.value){
            MaxSlider.value=MinSlider.value;
        }
        text.text=""+MaxSlider.value;
        level.intervalMax=(int) MaxSlider.value;
    }
    public void trueSaveModification(){
        leveltemp.SeenNumber=0;
        leveltemp.SuccessNumber=0;
        level=leveltemp;
        this.Display();
    }

    public void ViewTasks(){
        parent.ViewTasks(level);
    }
}
