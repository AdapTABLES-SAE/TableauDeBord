using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClassroomDisplayModif : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text nbStudentsText;
    private string idClasse;

    private ClassesClass representedClassRoom;
    
    void Start()
    {
        
    }

    public void SetClassRoom(ClassesClass classRoom) {
        representedClassRoom = classRoom;
        Display();
    }

    private void Display() {
        nameText.text = representedClassRoom.name;
        nbStudentsText.text = "" + representedClassRoom.nbStudents;
        idClasse = representedClassRoom.id;
        Debug.Log(idClasse);
    }

    public void ClickOnClass(){
        ClassesClass.classeChosen = representedClassRoom;
        SceneManager.LoadScene("ModificationClassesScene");
    }

}
