using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XCharts.Runtime;

public class LevelDisplay : MonoBehaviour
{
    private ObjectifDisplay parent;
    public int numberindex;
    public LevelClass level = new LevelClass("", "");
    public TMP_Text Title;
    public LevelDisplayMore More;
    public Button up;
    public Button down;
    public GameObject objectLevel;
    public RingChart chartSeen;
    public RingChart chartSuccess;
    public List<Button> allButtons;

    private void OnEnable()
    {
        //Display();
    }

    public void SetParent(ObjectifDisplay learner){
        parent = learner;
    }

    public void SetMore(LevelDisplayMore More){
        this.More=More;
    }

    public void SetParameters(int numberindex, LevelClass level){
        this.numberindex=numberindex;
        this.level=level;
        if(level.name=="" || level.name == null)
            this.level.name = "L" + parent.numberindex + numberindex;
        else
            this.level.name = level.name;
        Debug.Log("Set parameters, rates are " + level.SuccessNumber + " and " + level.SeenNumber);
    }

    public void Display(){
        Debug.Log("Display Level " + level.name);
        if (!initListeStudent.isModModify) {
            foreach (Button button in allButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
        //Debug.Log(level.name);
        Title.text = level.name;
        if(initListeStudent.isModModify)
        {
            if (numberindex == 0)
                up.interactable = false;
            else
                up.interactable = true;
            //Debug.Log(numberindex);
            //Debug.Log(parent.numberChild);
            if (numberindex == parent.numberChild - 1)
                down.interactable = false;
            else
                down.interactable = true;
        }
        Debug.Log("%s (chart) : " + level.SuccessNumber + " et " + level.SeenNumber);
        chartSeen.GetSerie(0).data[0].data[0] = level.SeenNumber;
        chartSeen.GetSerie(0).data[0].data[1] = level.SeenWanted;
        chartSuccess.GetSerie(0).data[0].data[0] = level.SuccessNumber;
        chartSuccess.GetSerie(0).data[0].data[1] = level.SuccessWanted;
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

    public void MoreInfoButton(){
        More.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetActive(bool isbool){
        objectLevel.SetActive(isbool);
    }
    public void ViewTasks(){
        parent.ViewTasks(level);
    }
}
