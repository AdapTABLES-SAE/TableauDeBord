using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ClassroomDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text nbStudentsText;
    private string idClasse;
    [SerializeField]
    private TMP_InputField inputFieldName;
    [SerializeField]
    private TMP_Text idClass;

    private ClassesClass representedClassRoom;

    private TMP_Text textDelete;
    private Button confirmDelete;
    private Button cancelDelete;
    private GameObject popupDelete;

    public InitSceneClassRooms init;

    void Start()
    {
        inputFieldName.transform.parent.gameObject.SetActive(false);
    }

    public void SetClassRoom(ClassesClass classRoom) {
        representedClassRoom = classRoom;
        Display();
    }

    public void DeleteClass()
    {
        StartCoroutine(APIManager.DeleteClasse(representedClassRoom.id, ProfClass.loggedTeacher.idProf, DeleteSuccess));
    }

    public void EditClassname()
    {
        ClassesClass.classeChosen = representedClassRoom;
        nameText.gameObject.SetActive(false);
        inputFieldName.transform.parent.gameObject.SetActive(true);
        inputFieldName.text = representedClassRoom.name;
    }

    public void ChangeClassname()
    {
        representedClassRoom.name = inputFieldName.text;
        inputFieldName.transform.parent.gameObject.SetActive(false);
        nameText.gameObject.SetActive(true);
        StartCoroutine(APIManager.ModifClasse(representedClassRoom.id, representedClassRoom.name, ModifySuccess));
        Display();
    }

    public void ModifySuccess(bool Succeded)
    {
        Debug.Log("change class name successful " + Succeded);
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
        confirmDelete.onClick.AddListener(DeleteClass);
        cancelDelete.onClick.AddListener(delegate
        {
            RemoveDeletePopup();
        });
        textDelete.text = representedClassRoom.name;
    }

    public void DeleteSuccess(bool Succeded)
    {
        Debug.Log("delete class successful " + Succeded);
        ProfClass.loggedTeacher.classes.RemoveAll(r => r.id == idClasse);
        SceneManager.LoadScene("ListeClassesScene");
    }

    private void Display() {
        nameText.text = representedClassRoom.name;
        nbStudentsText.text = "" + representedClassRoom.nbStudents;
        idClasse = representedClassRoom.id;
        idClass.text = idClasse;
        Debug.Log(idClasse);
    }

    public void ClickOnClass(){
        ClassesClass.classeChosen = representedClassRoom;
        StartCoroutine(APIManager.GetStudents(idClasse,ProfClass.loggedTeacher.idProf,ChangeScene));
    }

    private void ChangeScene(List<EleveClass> eleves){
        EleveClass.studentsOfClassroom=eleves;
        SceneManager.LoadScene("ListeElevesScene");
    }

}
