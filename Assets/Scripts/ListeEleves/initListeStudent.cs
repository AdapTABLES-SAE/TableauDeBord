using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class initListeStudent : MonoBehaviour
{
    public static bool isModModify;
    public Transform contentScrollView;
    public GameObject prefabLineEleve;
    public TMP_Text textClass;
    public GameObject popupDelete;
    public Button confirmDeleteButton;
    public Button cancelDeleteButton;
    public TMP_Text textDeleteEleve;


    void Start()
    {
        textClass.text = ClassesClass.classeChosen.name;

        foreach (EleveClass aEleve in EleveClass.studentsOfClassroom) {
            //Debug.Log("ajout d'une classe");
            GameObject line = Instantiate(prefabLineEleve, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            
            line.transform.SetParent(contentScrollView, false);

            StudentsDisplay display = line.GetComponent<StudentsDisplay>();
            display.SetStudent(aEleve);
            display.SetPopupDelete(popupDelete, confirmDeleteButton, cancelDeleteButton, textDeleteEleve);
        }
    }
}
