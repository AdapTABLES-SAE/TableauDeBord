using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using XCharts.Runtime;
using Unity.Collections.LowLevel.Unsafe;

public class LearningPathDisplay : MonoBehaviour
{
    private List<EleveClass> selectedStudent = new List<EleveClass>();
    private List<EleveClass> studentCopyFail = new List<EleveClass>();
    private List<TaskRepartiionDisplay> repartitions;
    private int nbSuccessCopy;
    public TextAsset fileError;
    public LearningPathClass learningPath;
    public Transform contentScrollView;
    public Transform contentScrollViewObjectifList;
    public Transform contentScrollViewLevelList;
    public Transform contentScrollViewPrerequisList;
    public Transform scrollViewListEleve;
    public Transform scrollViewListFail;
    public GameObject prefabLineObjectifClass;
    public GameObject prefabObjectifClassPlus;
    public GameObject prefabChoiseObjectif;
    public GameObject prefabChoiseLevel;
    public GameObject prefabViewPrerequis;
    public GameObject popupModifyLevel;
    public GameObject popupDeleteLevel;
    public GameObject popupModifyObjectif;
    public GameObject popupDeleteObjectif;
    public GameObject popupSelectSeenSuccess;
    public GameObject popupViewPrerequis;
    public GameObject popupViewTasks;
    public GameObject popupCreateTask;
    public GameObject popupModifyC1Task;
    public GameObject popupModifyC2Task;
    public GameObject popupModifyRECTask;
    public GameObject popupModifyIDTask;
    public GameObject popupModifyMEMBTask;
    public TMP_Text textDeleteLevel;
    public TMP_Text textModifyLevel;
    public TMP_Text textInputModifyLevel;
    public TMP_Text textDeleteObjectif;
    public TMP_Text textModifyObjectif;
    public TMP_Text textInputModifyObjectif;
    private ObjectifDisplay childSelected;
    private LevelClass temporaryLevel;
    private ObjectifsClass tempObjectif;
    public GameObject popupModifyLevelTable;
    public GameObject popupValidateChanges;
    public GameObject popupSelectObjectif;
    public GameObject popupSelectLevel;
    public GameObject popupSaveLP;
    public GameObject popupCopy;
    public GameObject popupCopyConfirm;
    public GameObject popupListFail;
    public GameObject prefabSelectStudent;
    public GameObject prefabStudentListFail;
    public GameObject bandeauSuccessSave;
    public GameObject bandeauFailureSave;
    public GameObject bandeauSuccessCopy;
    public Button buttonConfirmCopy;
    public GameObject bandeauSuccessCopyRetry;
    public TMP_Text textConfirmCopy;
    public ObjectifDisplay objectiveSelected;
    public GameObject popupRepartiion;
    public GameObject prefabRepartition;
    public TMP_Text textTotalRepart;
    public Transform scrollViewRepartition;
    public Button confirmRepartition;
    [SerializeField]
    private Slider Seen;
    [SerializeField]
    private Slider Success;
    [SerializeField]
    private Toggle t1;
    [SerializeField]
    private Toggle t2;
    [SerializeField]
    private Toggle t3;
    [SerializeField]
    private Toggle t4;
    [SerializeField]
    private Toggle t5;
    [SerializeField]
    private Toggle t6;
    [SerializeField]
    private Toggle t7;
    [SerializeField]
    private Toggle t8;
    [SerializeField]
    private Toggle t9;
    [SerializeField]
    private Toggle t10;
    [SerializeField]
    private Toggle t11;
    [SerializeField]
    private Toggle t12;
    public GameObject save;
    public GameObject copy;
    public GameObject repartTask;
    public GameObject createTask;

    void Start()
    {
        if (!initListeStudent.isModModify) {
            save.SetActive(false);
            copy.SetActive(false);
            repartTask.SetActive(false);
            createTask.SetActive(false);
        }
    }
    public void Initialised()
    {
        StartCoroutine(APIManager.GetProgression(ProfClass.loggedTeacher.idProf,EleveClass.studentChosen.idClasse,EleveClass.studentChosen.idStudent,ChargeObjectif, fileError));
    }

    public void ChargeObjectif(LearningPathClass lpclass){
        learningPath = lpclass;
        Reload(true);
    }

    public void Reload(bool refreshLevels)
    {
        for(int i=contentScrollView.childCount;i>0;i--){
            Destroy(contentScrollView.GetChild(i-1).gameObject);
        }
        contentScrollView.DetachChildren();
        Display(refreshLevels);
    }

    public void Display(bool refreshLevels){
        GameObject line;
        ObjectifDisplay display;
        int i = 0;
        foreach (ObjectifsClass anObjectif in learningPath.objectifs) {
            line = Instantiate(prefabLineObjectifClass, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            line.transform.SetParent(contentScrollView, false);
            display = line.GetComponent<ObjectifDisplay>();
            display.SetParent(this);
            //Debug.Log(JsonConvert.SerializeObject(anObjectif, Formatting.Indented));
            display.SetParameters(i,anObjectif);
            display.Display(refreshLevels);
            i++;
            objectiveSelected = display;
        }
        if (initListeStudent.isModModify)
        {
            line = Instantiate(prefabObjectifClassPlus, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            line.transform.SetParent(contentScrollView, false);
            ObjectifDisplayPlus displayPlus = line.GetComponent<ObjectifDisplayPlus>();
            displayPlus.SetParent(this);
        }
    }

    public void AddObjectif(){
        learningPath.objectifs.Add(new ObjectifsClass("O"+DateTime.Now.ToString("MMddyyyyHHmmss"),"O"+(learningPath.objectifs.Count+1)));
        Reload(false);
        objectiveSelected.SelectObjective();
    }

    public void Up(ObjectifsClass objectif, int index){
        learningPath.objectifs[index] = objectif;
    }

    public void Down(ObjectifsClass objectif, int index){
        learningPath.objectifs[index] = objectif;
    }

    public void Modify(LevelClass toModify,ObjectifDisplay child){
        popupModifyLevel.SetActive(true);
        textInputModifyLevel.text = toModify.name;
        textModifyLevel.text = "Modifier " + toModify.name;
        childSelected=child;
        temporaryLevel=toModify;
    }

    public void ModifyConfirm(){
        temporaryLevel.name = textInputModifyLevel.text;
        popupModifyLevel.SetActive(false);
        childSelected.ModifyConfirm();
        childSelected=null;
    }
    public void ModifyCancel(){
        popupModifyLevel.SetActive(false);
        childSelected=null;
    }

    public void Delete(LevelClass toDelete, ObjectifDisplay child){
        popupDeleteLevel.SetActive(true);
        textDeleteLevel.text = "Êtes-vous sûr de vouloir supprimer le niveau : " +  toDelete.name;
        childSelected = child;
    }

    public void DeleteConfirm(){
        childSelected.DeleteConfirm();
        popupDeleteLevel.SetActive(false);
        childSelected =null;
    }

    public void DeleteCancel(){
        popupDeleteLevel.SetActive(false);
        childSelected=null;
    }

    public void DeleteObjectif(ObjectifsClass objectif){
        textDeleteObjectif.text = "Êtes-vous sûr de vouloir supprimer le niveau : " + objectif.name;
        tempObjectif = objectif;
        popupDeleteObjectif.SetActive(true);
    }

    public void DeleteObjectifConfirm(){
        List<ObjectifsClass> allObjectiveExceptSelf = new List<ObjectifsClass>(learningPath.objectifs);
        allObjectiveExceptSelf.Remove(tempObjectif);
        foreach(ObjectifsClass objif in allObjectiveExceptSelf)
        {
            if(objif.prerequis.ContainsKey(tempObjectif.id))
                objif.prerequis.Remove(tempObjectif.id);
        }
        learningPath.objectifs.Remove(tempObjectif);
        popupDeleteObjectif.SetActive(false);
        Reload(false);
    }

    public void DeleteObjectifCancel(){
        popupDeleteObjectif.SetActive(false);
    }

    public void ModifyObjectif( ObjectifDisplay child){
        textModifyObjectif.text = "Modifier " +  child.objectif.name;
        textInputModifyObjectif.text = child.objectif.name;
        popupModifyObjectif.SetActive(true);
        childSelected=child;
    }

    public void ModifyObjectifCancel(){
        popupModifyObjectif.SetActive(false);
    }

    public void ModifyObjectifConfirm(){
        childSelected.objectif.name = textInputModifyObjectif.text;
        popupModifyObjectif.SetActive(false);
        childSelected.Refresh();
        childSelected=null;
    }

    public void ModifyTableObjectif(ObjectifDisplay ObjectifUsed){
        childSelected=ObjectifUsed;
        popupModifyLevelTable.SetActive(true);
        t1.isOn=false;
        t2.isOn=false;
        t3.isOn=false;
        t4.isOn=false;
        t5.isOn=false;
        t6.isOn=false;
        t7.isOn=false;
        t8.isOn=false;
        t9.isOn=false;
        t10.isOn=false;
        t11.isOn=false;
        t12.isOn=false;
        foreach(string table in ObjectifUsed.objectif.tableUse){
            switch(table){
                case "1":
                    t1.isOn=true;
                    break;
                case "2":
                    t2.isOn=true;
                    break;
                case "3":
                    t3.isOn=true;
                    break;
                case "4":
                    t4.isOn=true;
                    break;
                case "5":
                    t5.isOn=true;
                    break;
                case "6":
                    t6.isOn=true;
                    break;
                case "7":
                    t7.isOn=true;
                    break;
                case "8":
                    t8.isOn=true;
                    break;
                case "9":
                    t9.isOn=true;
                    break;
                case "10":
                    t10.isOn=true;
                    break;
                case "11":
                    t11.isOn=true;
                    break;
                case "12":
                    t12.isOn=true;
                    break;
            }
        }
    }
    public void ModifyTableObjectifConfirm(){
        List<string> newTableObjectif = new List<string>();
        if(t1.isOn==true)
            newTableObjectif.Add("1");
        if(t2.isOn==true)
            newTableObjectif.Add("2");
        if(t3.isOn==true)
            newTableObjectif.Add("3");
        if(t4.isOn==true)
            newTableObjectif.Add("4");
        if(t5.isOn==true)
            newTableObjectif.Add("5");
        if(t6.isOn==true)
            newTableObjectif.Add("6");
        if(t7.isOn==true)
            newTableObjectif.Add("7");
        if(t8.isOn==true)
            newTableObjectif.Add("8");
        if(t9.isOn==true)
            newTableObjectif.Add("9");
        if(t10.isOn==true)
            newTableObjectif.Add("10");
        if(t11.isOn==true)
            newTableObjectif.Add("11");
        if(t12.isOn==true)  
            newTableObjectif.Add("12");
        if(newTableObjectif.Count==0)
            newTableObjectif.Add("2");
        childSelected.objectif.tableUse=newTableObjectif;
        childSelected.ModifyTableConfirm();
        childSelected=null;
        popupModifyLevelTable.SetActive(false);
    }

    public void ModifyTableObjectifCancel(){
        popupModifyLevelTable.SetActive(false);
    }

    public void SaveLPConfirmation()
    { 
        popupSaveLP.SetActive(true); 
    }

    public void CancelSave()
    {
        popupSaveLP.SetActive(false);
    }
    
    public void ConfirmSave()
    {
        SaveObjectifs();
    }

    public void CheckSuccessSave(bool result)
    {
        if (result)
        {
            bandeauSuccessSave.SetActive(true);
        }
        else
            bandeauFailureSave.SetActive(true);

        StartCoroutine(Wait1SecSave(result));

    }

    IEnumerator Wait1SecSave(bool result)
    {
        yield return new WaitForSeconds(1);

        bandeauFailureSave.SetActive(false);
        bandeauSuccessSave.SetActive(false);
        popupSaveLP.SetActive(false);
    }

    public void SaveObjectifs(){
        learningPath.CheckAndAdjustTasksRepartition();
        StartCoroutine(APIManager.SaveObjectifs(learningPath, EleveClass.studentChosen, CheckSuccessSave)); 
    }

    public void SetObjectifPrerequis(ObjectifDisplay objectifDisplay){
        GameObject line;
        ObjectifPrerequisDisplay display;
        popupSelectObjectif.SetActive(true);
        List<ObjectifsClass> listtemp = new List<ObjectifsClass>(learningPath.objectifs);
        tempObjectif=objectifDisplay.objectif;
        childSelected=objectifDisplay;
        listtemp.Remove(tempObjectif);
        for(int i=contentScrollViewObjectifList.childCount;i>0;i--){
            Destroy(contentScrollViewObjectifList.GetChild(i-1).gameObject);
        }
        contentScrollViewObjectifList.DetachChildren();
        foreach (ObjectifsClass anObjectif in listtemp) {
            line = Instantiate(prefabChoiseObjectif, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            
            line.transform.SetParent(contentScrollViewObjectifList, false);

            display = line.GetComponent<ObjectifPrerequisDisplay>();
            display.SetParameters(this,anObjectif);
            display.Display(tempObjectif.prerequis.ContainsKey(anObjectif.id));
        }
    }
    public void SetObjectifPrerequisSelected(ObjectifsClass objectif){
        popupSelectObjectif.SetActive(false);
        this.SetLevelPrerequis(objectif);
    }
    public void SetObjectifPrerequisCancel(){
        popupSelectObjectif.SetActive(false);
        tempObjectif=null;
        childSelected=null;
    }
    public void SetLevelPrerequis(ObjectifsClass objectif){
        GameObject line;
        LevelPrerequisDisplay display;
        popupSelectLevel.SetActive(true);
        List<LevelClass> listtemp = objectif.listeLevel;
        for(int i=contentScrollViewLevelList.childCount;i>0;i--){
            Destroy(contentScrollViewLevelList.GetChild(i-1).gameObject);
        }
        contentScrollViewLevelList.DetachChildren();
        foreach (LevelClass aLevel in listtemp) {
            line = Instantiate(prefabChoiseLevel, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            
            line.transform.SetParent(contentScrollViewLevelList, false);

            display = line.GetComponent<LevelPrerequisDisplay>();
            display.SetParameters(this,aLevel,objectif);
            bool idin;
            if(tempObjectif.prerequis.ContainsKey(objectif.id))
                idin=tempObjectif.prerequis[objectif.id].levelId==aLevel.id;
            else
                idin=false;
            Debug.Log(idin);
            Debug.Log(tempObjectif.prerequis.ContainsKey(objectif.id));
            display.Display(idin);
        }
    }
    public void SetLevelPrerequisSelected(ObjectifsClass objectif, LevelClass level){
        popupSelectLevel.SetActive(false);
        tempObjectif=null;
        this.SetFloorForSeenSuccess(objectif,level);
    }
    public void SetLevelPrerequisCancel(){
        popupSelectLevel.SetActive(false);
        tempObjectif=null;
        childSelected=null;
    }
    public void SetFloorForSeenSuccess(ObjectifsClass objectif, LevelClass level){
        popupSelectSeenSuccess.SetActive(true);
        float seenWantedVal = 0;
        float successWantedVal = 0;
        Debug.Log(objectif.name);
        foreach(KeyValuePair<string, PrerequisObject> keyValue in childSelected.objectif.prerequis)
        {
            if (keyValue.Value.levelId.Equals(level.id))
            {
                seenWantedVal = keyValue.Value.seenFloor;
                successWantedVal = keyValue.Value.successfloor;
            }
        }
        Seen.value= seenWantedVal;
        Success.value= successWantedVal;
        tempObjectif=objectif;
        temporaryLevel=level;
    }
    public void SetFloorForSeenSuccessConfirm(){
        childSelected.PrerequisSelected(tempObjectif,temporaryLevel, Seen.value, Success.value);
        tempObjectif=null;
        temporaryLevel=null;
        childSelected=null;
        popupSelectSeenSuccess.SetActive(false);

    }
    public void SetFloorForSeenSuccessCancel(){
        popupSelectSeenSuccess.SetActive(false);
        childSelected=null;
    }
    public void changeValueSeenSlider(TMP_Text text){
        text.text=""+Seen.value;
    }
    public void changeValueSuccessSlider(TMP_Text text){
        text.text=""+Success.value;
    }
    public void ViewPrerequis(ObjectifsClass objectif){
        GameObject line;
        ViewPrerequisDisplay display;
        popupViewPrerequis.SetActive(true);
        for(int i=contentScrollViewPrerequisList.childCount;i>0;i--){
            Destroy(contentScrollViewPrerequisList.GetChild(i-1).gameObject);
        }
        contentScrollViewPrerequisList.DetachChildren();
        foreach (KeyValuePair<string,PrerequisObject> keyValue in objectif.prerequis) {
            line = Instantiate(prefabViewPrerequis, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            line.transform.SetParent(contentScrollViewPrerequisList, false);
            display = line.GetComponent<ViewPrerequisDisplay>();
            ObjectifsClass objectifPrerequis = learningPath.objectifs.Find(x => x.id==keyValue.Key);
            LevelClass levelPrerequis = objectifPrerequis.listeLevel.Find(x => x.id==keyValue.Value.levelId);
            display.SetParameters(objectifPrerequis,levelPrerequis,keyValue.Value.seenFloor,keyValue.Value.successfloor);
            display.Display();
        }
    }
    public void HideVisuPrerequis(){
        popupViewPrerequis.SetActive(false);
    }

    public Transform contentScrollViewTasks;
    public GameObject prefabTask;
    public void ViewTasks(LevelClass level){
        temporaryLevel=level;
        GameObject line;
        ViewTasksDisplay display;
        popupViewTasks.SetActive(true);
        for(int i=contentScrollViewTasks.childCount;i>0;i--){
            Destroy(contentScrollViewTasks.GetChild(i-1).gameObject);
        }
        contentScrollViewTasks.DetachChildren();
        foreach (TaskParameter task in level.tasks) {
            line = Instantiate(prefabTask, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            line.transform.SetParent(contentScrollViewTasks, false);
            display = line.GetComponent<ViewTasksDisplay>();
            display.SetParameters(this, task);
            display.Display();
        }
    }

    public void HideTasks(){
        popupViewTasks.SetActive(false);
        temporaryLevel=null;
    }

    public void CreateTask(){
        popupViewTasks.SetActive(false);
        popupCreateTask.SetActive(true);
    }

    public void CreateTaskC1(){
        popupCreateTask.SetActive(false);
        temporaryLevel.AddT1();
        ViewTasks(temporaryLevel);
    }

    public void CreateTaskC2(){
        popupCreateTask.SetActive(false);
        temporaryLevel.AddT2();
        ViewTasks(temporaryLevel);
    }

    public void CreateTaskREC(){
        popupCreateTask.SetActive(false);
        temporaryLevel.AddT3();
        ViewTasks(temporaryLevel);
    }

    public void CreateTaskID(){
        popupCreateTask.SetActive(false);
        temporaryLevel.AddT4();
        ViewTasks(temporaryLevel);
    }

    public void CreateTaskMEMB(){
        popupCreateTask.SetActive(false);
        temporaryLevel.AddT5();
        ViewTasks(temporaryLevel);
    }

    public void CancelCreate(){
        popupCreateTask.SetActive(false);
        ViewTasks(temporaryLevel);
    }

    
    private T1Parameters C1Task;
    private T2Parameters C2Task;
    private T3Parameters RECTask;
    private T4Parameters IDTask;
    private T5Parameters MEMBTask;
    public void ModifyTask(TaskParameter task){
        popupViewTasks.SetActive(false);
        if(task is T1Parameters){
            C1Task=(T1Parameters) task;
            popupModifyC1Task.SetActive(true);
            C1Result.SetIsOnWithoutNotify(false);
            C1Operand.SetIsOnWithoutNotify(false);
            C1Table.SetIsOnWithoutNotify(false);
            foreach(TargetT1Enum target in C1Task.targets){
                if(target==TargetT1Enum.RESULT)
                    C1Result.SetIsOnWithoutNotify(true);   
                if(target==TargetT1Enum.OPERAND)
                    C1Operand.SetIsOnWithoutNotify(true);
                if(target==TargetT1Enum.TABLE)
                    C1Table.SetIsOnWithoutNotify(true);
            }
            if(C1Task.answerModality==AnswerModalityEnum.CHOICE){
                C1ResultChoise.isOn=true;
                C1ResultChoiseSlider.value=C1Task.nbIncorrectChoices;
            }
            else{
                C1ResultSaisi.isOn=true;
            }
            Debug.Log("Time in sec.: " + C1Task.timeMaxSecond);
            C1Time.value = C1Task.timeMaxSecond;
            C1Success.value = C1Task.successiveSuccessesToReach;
            C1TimeText.text = "" + C1Task.timeMaxSecond;
            C1SuccessText.text = "" + C1Task.successiveSuccessesToReach;
        }
        if(task is T2Parameters){
            popupModifyC2Task.SetActive(true);
            C2Task=(T2Parameters) task;
            C2OperandResult.isOn=false;
            C2TableOperand.isOn=false;
            C2ResultTable.isOn=false;
            foreach(TargetT2Enum target in C2Task.targets){
                if(target==TargetT2Enum.OPERAND_TABLE)
                    C2TableOperand.SetIsOnWithoutNotify(true);   
                if(target==TargetT2Enum.OPERAND_RESULT)
                    C2OperandResult.SetIsOnWithoutNotify(true);
                if(target==TargetT2Enum.TABLE_RESULT)
                    C2ResultTable.SetIsOnWithoutNotify(true);
            }
            C2nbIncorrectAnswerSlider.value=C2Task.nbIncorrectChoices;
            C2timeMaxSecondSlider.value=C2Task.timeMaxSecond;
            C2Success.value=C2Task.successiveSuccessesToReach;
            C2timeMaxSecondSliderText.text = "" + C2Task.timeMaxSecond;
            C2SuccessText.text = "" + C2Task.successiveSuccessesToReach;
        }
        if(task is T3Parameters){
            popupModifyRECTask.SetActive(true);
            RECTask = (T3Parameters) task;
            RECnbIncorrectAnswerSlider.value=RECTask.nbIncorrectChoices;
            RECtimeMaxSecondSlider.value=RECTask.timeMaxSecond;
            RECSuccess.value=RECTask.successiveSuccessesToReach;
            RECtimeMaxSecondSliderText.text = "" + RECTask.timeMaxSecond;
            RECSuccessText.text = "" + RECTask.successiveSuccessesToReach;
        }
        if(task is T4Parameters){
            IDTask = (T4Parameters) task;
            popupModifyIDTask.SetActive(true);
            IDnbFactsSlider.value=IDTask.nbFacts;
            IDSuccess.value=IDTask.successiveSuccessesToReach;
            IDtimeMaxSecond.value=IDTask.timeMaxSecond;
            if(IDTask.sourceVariation==SourceVariationEnum.RESULT)
                IDsourceVariationResult.isOn=true;
            if(IDTask.sourceVariation==SourceVariationEnum.OPERAND)
                IDsourceVariationOperand.isOn=true;
            IDtimeMaxSecondText.text = "" + IDTask.timeMaxSecond;
            IDSuccessText.text = "" + IDTask.successiveSuccessesToReach;
        }
        if(task is T5Parameters){
            popupModifyMEMBTask.SetActive(true);
            MEMBTask = (T5Parameters) task;
            MEMBnbresultatCorrect.value=MEMBTask.nbCorrectChoices;
            MEMBnbresultatIncorrect.value=MEMBTask.nbIncorrectChoices;
            if(MEMBTask.target==TargetT3Enum.CORRECT)
                MEMBCorrectAnswer.isOn=true;
            if(MEMBTask.target==TargetT3Enum.INCORRECT)
                MEMBIncorrectAnswer.isOn=true;
            MEMBSuccess.value=MEMBTask.successiveSuccessesToReach;
            MEMBtimeMaxSecond.value=MEMBTask.timeMaxSecond;
            MEMBtimeMaxSecondText.text = "" + MEMBTask.timeMaxSecond;
            MEMBSuccessText.text = "" + MEMBTask.successiveSuccessesToReach;
        }
    }
    public Toggle C1Result;
    public Toggle C1Operand;
    public Toggle C1Table;
    public Toggle C1ResultChoise;
    public Slider C1ResultChoiseSlider;
    public TMP_Text C1ResultChoiseSliderText;
    public Toggle C1ResultSaisi;
    public Slider C1Time;
    public TMP_Text C1TimeText;
    public Slider C1Success;
    public TMP_Text C1SuccessText;
    public void ChangeAnswerModalityC1(){
        if(C1ResultChoise.isOn==true){
            C1ResultChoiseSlider.value=2;
        }
    }
    public void ChangeAnswerModalityNbIncorrectC1(){
        C1ResultChoiseSliderText.text=""+(int) C1ResultChoiseSlider.value;
        
    }
    public void ChangeTimeTextC1(){
        C1TimeText.text=""+(int) C1Time.value;
    }
    public void ChangeSuccessTextC1(){
        C1SuccessText.text=""+(int) C1Success.value;
    }
    public void CancelModifyC1(){
        popupModifyC1Task.SetActive(false);
        ViewTasks(temporaryLevel);
        C1Task=null;
    }
    public void ConfirmModifyC1(){
        int nbOn=0;
        if(C1Result.isOn==true) {nbOn++;};
        if(C1Operand.isOn==true) {nbOn++;};
        if(C1Table.isOn==true) {nbOn++;};
        C1Task.targets = new TargetT1Enum[nbOn];
        int index=0;
        if(C1Result.isOn==true) {C1Task.targets[index]=TargetT1Enum.RESULT; index++;};
        if(C1Operand.isOn==true) {C1Task.targets[index]=TargetT1Enum.OPERAND; index++;};
        if(C1Table.isOn==true) {C1Task.targets[index]=TargetT1Enum.TABLE;};
        if(C1ResultChoise.isOn==true){
            C1Task.answerModality=AnswerModalityEnum.CHOICE;
            C1Task.nbIncorrectChoices=(int) C1ResultChoiseSlider.value;
        }
        else{
            C1Task.answerModality=AnswerModalityEnum.INPUT;
            C1Task.nbIncorrectChoices=2;
        }
        C1Task.timeMaxSecond = (int) C1Time.value;
        popupModifyC1Task.SetActive(false);
        ViewTasks(temporaryLevel);
        C1Task=null;
    }

    public Toggle C2ResultTable;
    public Toggle C2OperandResult;
    public Toggle C2TableOperand;
    public Slider C2nbIncorrectAnswerSlider;
    public TMP_Text C2nbIncorrectAnswerSliderText;
    public Slider C2timeMaxSecondSlider;
    public TMP_Text C2timeMaxSecondSliderText;
    public Slider C2Success;
    public TMP_Text C2SuccessText;
    public void ChangeIncorrectTextC2(){
        C2nbIncorrectAnswerSliderText.text=""+(int) C2nbIncorrectAnswerSlider.value;
    }
    public void ChangeTimeTextC2(){
        C2timeMaxSecondSliderText.text=""+(int) C2timeMaxSecondSlider.value;
    }
    public void ChangeSuccessTextC2(){
        C2SuccessText.text=""+(int) C2Success.value;
    }
    public void CancelModifyC2(){
        popupModifyC2Task.SetActive(false);
        ViewTasks(temporaryLevel);
        C2Task=null;
    }
    public void ConfirmModifyC2(){
        int nbOn=0;
        if(C2ResultTable.isOn==true) {nbOn++;};
        if(C2OperandResult.isOn==true) {nbOn++;};
        if(C2TableOperand.isOn==true) {nbOn++;};
        C2Task.targets = new TargetT2Enum[nbOn];
        int index=0;
        if(C2ResultTable.isOn==true) {C2Task.targets[index]=TargetT2Enum.TABLE_RESULT; index++;};
        if(C2OperandResult.isOn==true) {C2Task.targets[index]=TargetT2Enum.OPERAND_RESULT; index++;};
        if(C2TableOperand.isOn==true) {C2Task.targets[index]=TargetT2Enum.OPERAND_TABLE;};
        C2Task.nbIncorrectChoices=(int) C2nbIncorrectAnswerSlider.value;
        C2Task.timeMaxSecond=(int) C2timeMaxSecondSlider.value;
        C2Task.successiveSuccessesToReach=(int) C2Success.value;
        C2Task=null;
        popupModifyC2Task.SetActive(false);
        ViewTasks(temporaryLevel);
    }

    public Slider RECnbIncorrectAnswerSlider;
    public Slider RECSuccess;
    public Slider RECtimeMaxSecondSlider;
    public TMP_Text RECnbIncorrectAnswerSliderText;
    public TMP_Text RECSuccessText;
    public TMP_Text RECtimeMaxSecondSliderText;
    public void ChangeIncorrectTextREC(){
        RECnbIncorrectAnswerSliderText.text=""+(int) RECnbIncorrectAnswerSlider.value;
    }
    public void ChangeTimeTextREC(){
        RECtimeMaxSecondSliderText.text=""+(int) RECtimeMaxSecondSlider.value;
    }
    public void ChangeSuccessTextREC(){
        RECSuccessText.text=""+(int) RECSuccess.value;
    }
    public void CancelModifyREC(){
        RECTask=null;
        popupModifyRECTask.SetActive(false);
        ViewTasks(temporaryLevel);
    }
    public void ConfirmModifyREC(){
        RECTask.nbIncorrectChoices=(int) RECnbIncorrectAnswerSlider.value;
        RECTask.timeMaxSecond=(int) RECtimeMaxSecondSlider.value;
        RECTask.successiveSuccessesToReach=(int) RECSuccess.value;
        RECTask=null;
        popupModifyRECTask.SetActive(false);
        ViewTasks(temporaryLevel);
    }

    public Slider IDnbFactsSlider;
    public TMP_Text IDnbFactsSliderText;
    public Toggle IDsourceVariationResult;
    public Toggle IDsourceVariationOperand;
    public Slider IDSuccess;
    public TMP_Text IDSuccessText;
    public Slider IDtimeMaxSecond;
    public TMP_Text IDtimeMaxSecondText;
    public void CancelModifyID(){
        popupModifyIDTask.SetActive(false);
        ViewTasks(temporaryLevel);
        IDTask=null;
    }
    public void ConfirmModifyID(){
        if(IDsourceVariationOperand.isOn==true)
            IDTask.sourceVariation=SourceVariationEnum.OPERAND;
        if(IDsourceVariationResult.isOn==true)
            IDTask.sourceVariation=SourceVariationEnum.RESULT;
        IDTask.nbFacts=(int) IDnbFactsSlider.value;
        IDTask.timeMaxSecond=(int) IDtimeMaxSecond.value;
        IDTask.successiveSuccessesToReach=(int) IDSuccess.value;
        popupModifyIDTask.SetActive(false);
        ViewTasks(temporaryLevel);
        IDTask=null;
    }
    public void ChangeNbFactsText(){
        IDnbFactsSliderText.text=""+(int) IDnbFactsSlider.value;
    }
    public void ChangeSuccessTextID(){
        IDSuccessText.text=""+(int) IDSuccess.value;
    }
    public void ChangeTimeTextID(){
        IDtimeMaxSecondText.text=""+(int) IDtimeMaxSecond.value;
    }

    public Slider MEMBnbresultat;
    public TMP_Text MEMBnbresultatText;
    public Slider MEMBnbresultatCorrect;
    public TMP_Text MEMBnbresultatCorrectText;
    public Slider MEMBnbresultatIncorrect;
    public TMP_Text MEMBnbresultatIncorrectText;
    public Toggle MEMBCorrectAnswer;
    public Toggle MEMBIncorrectAnswer;
    public Slider MEMBSuccess;
    public TMP_Text MEMBSuccessText;
    public Slider MEMBtimeMaxSecond;
    public TMP_Text MEMBtimeMaxSecondText;
    public void ChangeNBresultat(){
        MEMBnbresultat.value=MEMBnbresultatCorrect.value+MEMBnbresultatIncorrect.value;
    }
    public void ChangeNBresultatText(){
        MEMBnbresultatText.text=""+(int) MEMBnbresultat.value;
    }
    public void ChangeNBresultatCorrectText(){
        MEMBnbresultatCorrectText.text=""+(int) MEMBnbresultatCorrect.value;
        ChangeNBresultat();
    }
    public void ChangeNBresultatIncorrectText(){
        MEMBnbresultatIncorrectText.text=""+(int) MEMBnbresultatIncorrect.value;
        ChangeNBresultat();
    }
    public void ChangeTimeTextMEMB(){
        MEMBtimeMaxSecondText.text=""+(int) MEMBtimeMaxSecond.value;
    }
    public void ChangeSuccessTextMEMB(){
        MEMBSuccessText.text=""+(int) MEMBSuccess.value;
    }
    public void CancelModifyMEMB(){
        popupModifyMEMBTask.SetActive(false);
        ViewTasks(temporaryLevel);
        MEMBTask=null;
    }
    public void ConfirmModifyMEMB(){
        MEMBTask.nbIncorrectChoices=(int) MEMBnbresultatIncorrect.value;
        MEMBTask.nbCorrectChoices=(int) MEMBnbresultatCorrect.value;
        MEMBTask.timeMaxSecond=(int) MEMBtimeMaxSecond.value;
        MEMBTask.successiveSuccessesToReach=(int) MEMBSuccess.value;
        if(MEMBCorrectAnswer.isOn==true)
            MEMBTask.target=TargetT3Enum.CORRECT;
        if(MEMBIncorrectAnswer.isOn==true)
            MEMBTask.target=TargetT3Enum.INCORRECT;
        popupModifyMEMBTask.SetActive(false);
        ViewTasks(temporaryLevel);
        MEMBTask=null;
    }

    public GameObject popupDeleteTask;
    private TaskParameter taskToDelete;
    public void DeleteTask(TaskParameter task){
        taskToDelete=task;
        popupViewTasks.SetActive(false);
        popupDeleteTask.SetActive(true);
    }
    public void CancelDeleteTask(){
        popupDeleteTask.SetActive(false);
        ViewTasks(temporaryLevel);
        taskToDelete=null;
    }
    public void ConfirmDeleteTask(){
        temporaryLevel.tasks.Remove(taskToDelete);
        ViewTasks(temporaryLevel);
        popupDeleteTask.SetActive(false);
        taskToDelete=null;
    }

    public void CopyLPPopup()
    {
        buttonConfirmCopy.interactable = false;
        selectedStudent = new List<EleveClass>();
        popupCopy.SetActive(true);
        scrollViewListEleve.DetachChildren();
        List<EleveClass> allStudentExceptCurrent = new List<EleveClass>(EleveClass.studentsOfClassroom);
        allStudentExceptCurrent.Remove(EleveClass.studentChosen);
        GameObject line;
        foreach (EleveClass student in allStudentExceptCurrent)
        {
            line = Instantiate(prefabSelectStudent, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            line.transform.SetParent(scrollViewListEleve, false);

            SelectStudentDisplay display = line.GetComponent<SelectStudentDisplay>();
            display.SetStudent(student);
            display.SetFunction(AddStudentInSelect,RemoveStudentInSelect);
            display.Display();
        }
    }

    public void AddStudentInSelect(EleveClass student)
    {
        selectedStudent.Add(student);
        buttonConfirmCopy.interactable = true;
    }

    public void RemoveStudentInSelect(EleveClass student)
    {

        if(selectedStudent.Contains(student))
        { 
            selectedStudent.Remove(student);
            if(selectedStudent.Count==0)
                buttonConfirmCopy.interactable = false;
        }
    }
    public void ToConfirmCopy()
    {
        for (int i = scrollViewListEleve.childCount; i > 0; i--)
        {
            Destroy(scrollViewListEleve.GetChild(i - 1).gameObject);
        }
        scrollViewListEleve.DetachChildren();
        popupCopy.SetActive(false);
        popupCopyConfirm.SetActive(true);
        textConfirmCopy.text = "Êtes-vous sûr de vouloir dupliquer le parcours de " 
            + EleveClass.studentChosen.nomEleve + " " 
            + EleveClass.studentChosen.prenomEleve + " à " 
            + selectedStudent.Count + " élèves sélectionnés ?";
    }
    public void CancelCopy()
    {
        for (int i = scrollViewListEleve.childCount; i > 0; i--)
        {
            Destroy(scrollViewListEleve.GetChild(i - 1).gameObject);
        }
        scrollViewListEleve.DetachChildren();
        popupCopy.SetActive(false);
    }
    public void CancelCopyConfirm()
    {
        popupCopyConfirm.SetActive(false);
    }
    public void ConfirmCopy()
    {
        nbSuccessCopy = 0;
        studentCopyFail = new List<EleveClass>();
        StartCoroutine(SaveForAnotherStudent(selectedStudent));
    }
    IEnumerator SaveForAnotherStudent(List<EleveClass> students)
    {
        foreach (EleveClass studentInList in selectedStudent)
        {
            StartCoroutine(APIManager.SaveObjectifs(LearningPathClass.Copy(studentInList, learningPath), studentInList, CheckSuccessCopy));
            yield return new WaitForSeconds(0.35f);
        }
    }
    public void SelectAll(Toggle toggle)
    {
        if (toggle.isOn)
        {
            for (int i = 0; i < scrollViewListEleve.childCount; i++)
            {
                scrollViewListEleve.GetChild(i).gameObject.GetComponent<SelectStudentDisplay>().select.isOn = true;
            }
        }
        else
        {
            for (int i = 0; i < scrollViewListEleve.childCount; i++)
            {
                scrollViewListEleve.GetChild(i).gameObject.GetComponent<SelectStudentDisplay>().select.isOn = false;
            }
        }
    }
    public void CheckSuccessCopy(bool result, EleveClass studentPosiblyFail)
    {
        if (result)
        {
            nbSuccessCopy++;
        }
        else
            studentCopyFail.Add(studentPosiblyFail);

        if (nbSuccessCopy == selectedStudent.Count)
        {
            bandeauSuccessCopy.SetActive(true);
            StartCoroutine(Wait1SecCopy(result));
        }
        else if (selectedStudent.Count == (nbSuccessCopy + studentCopyFail.Count))
        {
            popupCopyConfirm.SetActive(false);
            popupListFail.SetActive(true);

            while (scrollViewListFail.childCount > 0)
            {
                Destroy(scrollViewListFail.GetChild(0).gameObject);
            }
            scrollViewListFail.DetachChildren();
            GameObject line;
            foreach (EleveClass student in studentCopyFail)
            {
                line = Instantiate(prefabStudentListFail, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                line.transform.SetParent(scrollViewListFail, false);

                StudentFailureDisplay display = line.GetComponent<StudentFailureDisplay>();
                display.SetStudent(student);
                display.Display();
            }
        }

    }

    IEnumerator Wait1SecCopy(bool result)
    {
        yield return new WaitForSeconds(1);

        bandeauSuccessCopy.SetActive(false);
        popupCopyConfirm.SetActive(false);
    }
    public void RetryCopy()
    {
        nbSuccessCopy = 0;
        selectedStudent = new List<EleveClass>(studentCopyFail);
        studentCopyFail = new List<EleveClass>();
        StartCoroutine(SaveForAnotherStudent(selectedStudent));
    }
    public void CancelRetry()
    {
        popupListFail.SetActive(false);
    }
    public void CheckSuccessCopyRetry(bool result, EleveClass studentPosiblyFail)
    {
        if (result)
        {
            nbSuccessCopy++;
        }
        else
            studentCopyFail.Add(studentPosiblyFail);

        if (nbSuccessCopy == selectedStudent.Count)
        {
            bandeauSuccessCopyRetry.SetActive(true);
            StartCoroutine(Wait1SecCopy(result));
        }
        else if (selectedStudent.Count == (nbSuccessCopy + studentCopyFail.Count))
        {
            while (scrollViewListFail.childCount > 0)
            {
                Destroy(scrollViewListFail.GetChild(0).gameObject);
            }
            scrollViewListFail.DetachChildren();
            GameObject line;
            foreach (EleveClass student in studentCopyFail)
            {
                line = Instantiate(prefabSelectStudent, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                line.transform.SetParent(scrollViewListFail, false);

                StudentFailureDisplay display = line.GetComponent<StudentFailureDisplay>();
                display.SetStudent(student);
                display.Display();
            }
        }

    }

    IEnumerator Wait1SecCopyRetry(bool result)
    {
        yield return new WaitForSeconds(1);

        bandeauSuccessCopyRetry.SetActive(false);
        popupListFail.SetActive(false);
    }

    public void ToRepartitionTask()
    {
        repartitions = new List<TaskRepartiionDisplay>();
        popupRepartiion.SetActive(true);
        GameObject line;
        foreach (TaskParameter task in temporaryLevel.tasks)
        {
            line = Instantiate(prefabRepartition, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            line.transform.SetParent(scrollViewRepartition, false);
            Debug.Log("Ajout Task");
            TaskRepartiionDisplay display = line.GetComponent<TaskRepartiionDisplay>();
            repartitions.Add(display.SetParameters(this,task));
            display.Display();
        }
    }
    public void ValueChangerepartition()
    {
        float valueTotal = 0;
        foreach(TaskRepartiionDisplay task in repartitions)
        {
            valueTotal += float.Parse(task.inputRepartition.text);
        }
        textTotalRepart.text = valueTotal.ToString("")+"%";
        if (valueTotal == 100) confirmRepartition.interactable = true;
        else confirmRepartition.interactable = false;
    }
    public void ConfirmRepartition()
    {
        foreach (TaskRepartiionDisplay task in repartitions)
        {
            task.task.repartitionPercent = int.Parse(task.inputRepartition.text);
        }
        for (int i = scrollViewRepartition.childCount; i > 0; i--)
        {
            Destroy(scrollViewRepartition.GetChild(i - 1).gameObject);
        }
        scrollViewRepartition.DetachChildren();
        popupRepartiion.SetActive(false);
    }
    public void CancelRepartition()
    {
        for (int i = scrollViewRepartition.childCount; i > 0; i--)
        {
            Destroy(scrollViewRepartition.GetChild(i - 1).gameObject);
        }
        scrollViewRepartition.DetachChildren();
        popupRepartiion.SetActive(false);
    }

}
