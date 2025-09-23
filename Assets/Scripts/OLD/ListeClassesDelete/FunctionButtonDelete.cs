using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FunctionButtonDelete : MonoBehaviour
{
    public static List<string> idClassesSelected= new List<string>();
    public GameObject panelPopup;
    public static ClassesClass classesSelected;

    public void ClickConfirmButton(){
        panelPopup.SetActive(true);
    }

    public void ClickReturnButton(){
        SceneManager.LoadScene("ListeClassesScene");
    }

    public void ClickOutPopup(){
        panelPopup.SetActive(false);
    }

    //public void ClickTrueConfimButton(){
    //    foreach(string idClasse in idClassesSelected){
    //        StartCoroutine(APIManager.DeleteClasse(idClasse,ProfClass.loggedTeacher.idProf,Success));
    //    }
    //}

    //public void Success(bool Succeded,string idClasse){
    //    if(Succeded){
    //        ProfClass.loggedTeacher.classes.RemoveAll(r => r.id == idClasse);
    //    }
    //    idClassesSelected.Remove(idClasse);
    //    if(idClassesSelected.Count==0)
    //        SceneManager.LoadScene("ListeClassesScene");
    //}

    public void ClickCancelButton(){
        panelPopup.SetActive(false);
    }
}
