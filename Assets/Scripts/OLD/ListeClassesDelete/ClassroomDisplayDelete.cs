using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ClassroomDisplayDelete : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text nbStudentsText;
    [SerializeField]
    private Selectable toggle;
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

    public void ClickToggle(bool selected){
        if(selected)
            FunctionButtonDelete.idClassesSelected.Add(representedClassRoom.id);
        else 
            FunctionButtonDelete.idClassesSelected.Remove(representedClassRoom.id);
    }

}
