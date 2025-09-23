using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StudentsDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text lastNameText;
    [SerializeField]
    private TMP_Text firstNameText;
    [SerializeField]
    private TMP_Text identifiantText;
    [SerializeField]
    private TMP_InputField inputFieldLastname;
    [SerializeField]
    private TMP_InputField inputFieldFirstname;

    private TMP_Text textDelete;
    private Button confirmDelete;
    private Button cancelDelete;
    private GameObject popupDelete;

    //[SerializeField]
    //private TMP_Text progressText;

    private EleveClass eleveRepresented;
    
    void Start()
    {
        inputFieldFirstname.transform.parent.gameObject.SetActive(false);
        inputFieldLastname.transform.parent.gameObject.SetActive(false);
    }

    public void DeleteStudent()
    {
        StartCoroutine(APIManager.DeleteStudent(eleveRepresented, ProfClass.loggedTeacher.idProf, DeleteSuccess));
    }

    public void DeleteSuccess(bool Succeded)
    {
        Debug.Log("delete student successful " + Succeded);
        EleveClass.studentsOfClassroom.Remove(eleveRepresented);
        SceneManager.LoadScene("ListeClassesScene");
    }

    public void SetStudent(EleveClass eleve) {
        eleveRepresented = eleve;
        Display();
    }

    private void Display() {
        lastNameText.text = eleveRepresented.nomEleve;
        firstNameText.text = eleveRepresented.prenomEleve;
        identifiantText.text = eleveRepresented.idStudent;
        //progressText.text = "indéfinie";
    }

    public void ClickOnSeeStats(){
        EleveClass.studentChosen = eleveRepresented;
        SceneManager.LoadScene("ProgressionEleveScene");
        initListeStudent.isModModify = false;
    }
    public void ClickOnModify(){
        EleveClass.studentChosen = eleveRepresented;
        SceneManager.LoadScene("ProgressionEleveScene");
        initListeStudent.isModModify = true;
    }

    public void SetPopupDelete(GameObject popup, Button Confirm, Button Cancel, TMP_Text textDelete)
    {
        popupDelete = popup;
        confirmDelete = Confirm;
        cancelDelete = Cancel;
        this.textDelete = textDelete;
    }

    public void RemoveDeletePopup()
    {
        popupDelete.SetActive(false);
    }

    public void DeleteConfirmation()
    {
        popupDelete.SetActive(true);
        confirmDelete.onClick.RemoveAllListeners();
        cancelDelete.onClick.RemoveAllListeners();
        confirmDelete.onClick.AddListener(DeleteStudent);
        cancelDelete.onClick.AddListener(delegate
        {
            RemoveDeletePopup();
        });
        textDelete.text = eleveRepresented.prenomEleve + " " + eleveRepresented.nomEleve;
    }

    public void EditStudentNames()
    {
        EleveClass.studentChosen = eleveRepresented;
        firstNameText.gameObject.SetActive(false);
        inputFieldFirstname.transform.parent.gameObject.SetActive(true);
        inputFieldFirstname.text = eleveRepresented.prenomEleve;

        lastNameText.gameObject.SetActive(false);
        inputFieldLastname.transform.parent.gameObject.SetActive(true);
        inputFieldLastname.text = eleveRepresented.nomEleve;
    }

    public void ChangeStudentnames()
    {
        eleveRepresented.nomEleve = inputFieldLastname.text;
        eleveRepresented.prenomEleve = inputFieldFirstname.text;
        inputFieldFirstname.transform.parent.gameObject.SetActive(false);
        inputFieldLastname.transform.parent.gameObject.SetActive(false);
        lastNameText.gameObject.SetActive(true);
        firstNameText.gameObject.SetActive(true);
        StartCoroutine(APIManager.ModifStudent(eleveRepresented, ModifySuccess));
        Display();
    }

    public void ModifySuccess(bool Succeded)
    {
        Debug.Log("change student name successful " + Succeded);
    }
}
