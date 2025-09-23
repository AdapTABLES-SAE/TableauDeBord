using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine;

public class FunctionButtonListeStudentDelete : MonoBehaviour
{
    public static List<EleveClass> elevesSelected = new List<EleveClass>();
    public GameObject panelPopup;
    public static ClassesClass classesSelected;


    public void ClickConfirmButton(){
        panelPopup.SetActive(true);
    }

    public void ClickReturnButton(){
        SceneManager.LoadScene("ListeElevesScene");
    }

    public void ClickOutPopup(){
        panelPopup.SetActive(false);
    }

    public void ClickTrueConfimButton(){
        foreach(EleveClass eleve in elevesSelected){
            
            //StartCoroutine(APIManager.DeleteStudent(eleve,ProfClass.loggedTeacher.idProf,Success));
        }
        
    }

    //public void Success(bool Succeded){
    //    if(Succeded){
    //        EleveClass.studentsOfClassroom.Remove(eleve);
    //    }
    //    elevesSelected.Remove(eleve);
    //    if(elevesSelected.Count==0)
    //        SceneManager.LoadScene("ListeElevesScene");
    //}

    public void ClickCancelButton(){
        panelPopup.SetActive(false);
    }
}
