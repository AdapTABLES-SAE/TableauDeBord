using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class InitSceneClassRooms : MonoBehaviour
{
    public Transform contentScrollView;
    public GameObject prefabLineClassRoom;
    public TMP_Text teacherNameText;
    public GameObject popupDelete;
    public Button confirmDeleteButton;
    public Button cancelDeleteButton;
    public TMP_Text textDeleteClass;

    void Start()
    {
        Display();
    }

    public void Display()
    {
        //Transform[] allChildren = contentScrollView.GetComponentsInChildren<Transform>();
        //foreach (Transform child in allChildren)
        //{
        //    Debug.Log("Child " + child.name);
        //    Destroy(child);
        //}

        teacherNameText.text = ProfClass.loggedTeacher.name;
        foreach (ClassesClass aClassRoom in ProfClass.loggedTeacher.classes) {
            Debug.Log("ajout d'une classe");
            GameObject line = Instantiate(prefabLineClassRoom, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            
            line.transform.SetParent(contentScrollView, false);

            ClassroomDisplay display = line.GetComponent<ClassroomDisplay>();
            display.SetClassRoom(aClassRoom);
            display.SetPopupDelete(popupDelete, confirmDeleteButton, cancelDeleteButton, textDeleteClass);
            display.init = this;
        }
    }    
}
