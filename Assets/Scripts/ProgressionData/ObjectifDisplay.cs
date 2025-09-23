using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Data;

public class ObjectifDisplay : MonoBehaviour
{
    public LearningPathDisplay parent;
    public int numberindex;
    public ObjectifsClass objectif;
    public GameObject prefabLevelClassPlus;
    public GameObject prefabLineLevelClass;
    public GameObject prefabLineLevelMoreClass;
    private LevelDisplay childSelected;
    private LevelDisplayMore childSelectedMore;
    private Dictionary<LevelClass, LevelDisplay> mapLevelModel2View;
    //private int nbLevelDetailed = 1;
    public Transform contentGrid;
    public GameObject selectObjectiveButton;
    public int numberChild;
    public TMP_Text title; 
    public TMP_Text Tables;
    public List<GameObject> allButtons;

    void Start()
    {
        mapLevelModel2View = new Dictionary<LevelClass, LevelDisplay>();
        Retract();

    }

    void Update()
    {
        if (!initListeStudent.isModModify)
            foreach (GameObject button in allButtons)
            {
                button.SetActive(false);
            }
    }

    public void SetParent(LearningPathDisplay parent){
        this.parent = parent;
    }

    public void SetParameters(int numberindex, ObjectifsClass objectif){
        this.numberindex=numberindex;
        this.objectif=objectif;
        numberChild=objectif.listeLevel.Count;
    }

    public void SelectObjective()
    {
        if(parent.objectiveSelected != null)
            parent.objectiveSelected.Retract();
        parent.objectiveSelected = this;

        contentGrid.gameObject.SetActive(true);
        selectObjectiveButton.SetActive(false);
    }

    public void Retract()
    {
        contentGrid.gameObject.SetActive(false);
        selectObjectiveButton.SetActive(true);
    }


    public void Display(bool refreshBackendData){
        contentGrid.gameObject.SetActive(true);
        selectObjectiveButton.SetActive(false);
        GameObject line;
        GameObject lineMore;
        //if(objectif.name!="")
            title.text = objectif.name;
        //else
          //  title.text = "O"+ numberindex;
        int i = 0;
        foreach (LevelClass aLevel in objectif.listeLevel) {
            line = Instantiate(prefabLineLevelClass, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            lineMore = Instantiate(prefabLineLevelMoreClass, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            line.transform.SetParent(contentGrid, false);
            lineMore.transform.SetParent(contentGrid, false);
            
            LevelDisplay display = line.GetComponent<LevelDisplay>();
            LevelDisplayMore displaymore = lineMore.GetComponent<LevelDisplayMore>();
            display.SetParent(this);
            display.SetParameters(i,aLevel);
            displaymore.SetParent(this);                  
            displaymore.SetParameters(i,aLevel);
            display.SetMore(displaymore);
            display.Display();
            displaymore.SetBase(display);
            displaymore.SetActive(false);
            i++;
        }
        if (initListeStudent.isModModify)
        {
            line = Instantiate(prefabLevelClassPlus, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            line.transform.SetParent(contentGrid, false);
            LevelDisplayPlus displayplus = line.GetComponent<LevelDisplayPlus>();
            displayplus.SetParent(this);
        }
        string resultTable="[ ";
        for (int j=0;j<objectif.tableUse.Count;j++){
            resultTable += objectif.tableUse[j];
            if(objectif.tableUse.Count-1!=j){
                resultTable+=" , ";
            }
        }
        resultTable+=" ]";
        Tables.text="Tables Travaillées : "+resultTable;
        //nbLevelDetailed = 1;
        if(refreshBackendData)
        {
            StartCoroutine(GetDetailedDataOfLevel(objectif.id));
        }
    }

    private IEnumerator GetDetailedDataOfLevel(String objectiveId)
    {
        foreach (LevelClass aLevel in objectif.listeLevel) {
            
            StartCoroutine(APIManager.GetProgressionLevel(aLevel, objectiveId, EleveClass.studentChosen.idStudent, SetDetailedDataOfLevel));
            
            yield return new WaitForSeconds(0.35f);
        }
    }

    private void SetDetailedDataOfLevel(LevelClass level, DataLevelJSON data)
    {
        if (data != null)
        {
            level.SuccessNumber = data.globalSuccess;
            level.SeenNumber = data.globalEncounters;
            Debug.Log("Fetch progress " + data.globalSuccess + " et " + data.globalEncounters);

            //mapLevelModel2View[level].Display(); 

            foreach (DataProgress datas in data.progresses)
            {
                int i = 0;
                if (datas.typeTask.Equals("C1"))
                {
                    while (level.tasks[i].typeTask!=TypeTaskEnum.C1)
                            i++;
                }
                if (datas.typeTask.Equals("C2"))
                {
                    while (level.tasks[i].typeTask != TypeTaskEnum.C2)
                        i++;
                }
                if (datas.typeTask.Equals("ID"))
                {
                    while (level.tasks[i].typeTask != TypeTaskEnum.ID)
                        i++;
                }
                if (datas.typeTask.Equals("REC"))
                {
                    while (level.tasks[i].typeTask != TypeTaskEnum.REC)
                        i++;
                }
                if (datas.typeTask.Equals("MEMB"))
                {
                    while (level.tasks[i].typeTask != TypeTaskEnum.MEMB)
                        i++;
                }
                level.tasks[i].seen = data.progresses[i].currentEncounters;
                level.tasks[i].success = data.progresses[i].currentSuccess;
            }
        }
    }

    public void PlusLevel(){
        objectif.listeLevel.Add(new LevelClass("L"+DateTime.Now.ToString("MMddyyyyHHmmss"),"L"+(numberChild+1)));
        numberChild++;
        Refresh();
    }

    public void Refresh(){
        for(int i=contentGrid.childCount;i>0;i--){
            Destroy(contentGrid.GetChild(i-1).gameObject);
        }
        contentGrid.DetachChildren();
        Display(false);
    }

    public void up(int index){
        LevelClass temp = objectif.listeLevel[index];
        objectif.listeLevel[index] = objectif.listeLevel[index-1];
        objectif.listeLevel[index-1] = temp;
        parent.Up(objectif,numberindex);
        Refresh();
    }

    public void down(int index){
        LevelClass temp = objectif.listeLevel[index];
        objectif.listeLevel[index] = objectif.listeLevel[index+1];
        objectif.listeLevel[index+1] = temp;
        parent.Down(objectif,numberindex);
        Refresh();
    }

    public void Modify(int index,LevelDisplay child){
        parent.Modify(objectif.listeLevel[index],this);
        childSelected=child;
    }

    public void Modify(int index,LevelDisplayMore child){
        parent.Modify(objectif.listeLevel[index],this);
        childSelectedMore=child;
    }

    public void ModifyConfirm(){
        Refresh();
    }

    public void Delete(int index, LevelDisplay child){
        parent.Delete(objectif.listeLevel[index],this);
        childSelected=child;
    }

    public void Delete(int index, LevelDisplayMore child){
        parent.Delete(objectif.listeLevel[index],this);
        childSelectedMore=child;
    }

    public void DeleteConfirm(){
        if(childSelected!=null)
            objectif.listeLevel.Remove(objectif.listeLevel[childSelected.numberindex]);
        else
            objectif.listeLevel.Remove(objectif.listeLevel[childSelectedMore.numberindex]);
        numberChild--;
        Refresh();
        childSelected=null;
        childSelectedMore=null;
    }

    public void DeleteObjectif(){
        parent.DeleteObjectif(objectif);
    }

    public void ModifyObjectif(){
        parent.ModifyObjectif(this);
    }

    public void ModifyTableConfirm(){
        Refresh();
    }

    public void ModifyTableObjectif(){
        parent.ModifyTableObjectif(this);
    }
    public void PrerequisSelect(){
        parent.SetObjectifPrerequis(this);
    }
    public void PrerequisSelected(ObjectifsClass objectif, LevelClass level, float Seen, float Success){
        this.objectif.Add(objectif.id,level.id,Seen,Success);
    }
    public void ViewPrerequis(){
        parent.ViewPrerequis(objectif);
    }
    public void ViewTasks(LevelClass level){
        parent.ViewTasks(level);
    }
}
