using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class InitSceneEnseignants : MonoBehaviour
{
    public Transform contentScrollView;
    public GameObject prefabLineEnseignant;
    public GameObject popupDelete;
    public Button ConfirmDeleteButton;
    public Button CancelDeleteButton;
    public TMP_Text TextDeleteProf;

    void Start()
    {
        StartCoroutine(APIManager.GetProfs(Display));
    }

    public void Display(List<ProfClass> profs)
    {
        //Transform[] allChildren = contentScrollView.GetComponentsInChildren<Transform>();
        //foreach (Transform child in allChildren)
        //{
        //    Debug.Log("Child " + child.name);
        //    Destroy(child);
        //}

        foreach (ProfClass aEnseignant in profs) {
            Debug.Log("ajout d'une classe");
            GameObject line = Instantiate(prefabLineEnseignant, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            
            line.transform.SetParent(contentScrollView, false);

            EnseignantDisplay display = line.GetComponent<EnseignantDisplay>();
            display.SetEnseignant(aEnseignant);
            display.SetPopupDelete(popupDelete, ConfirmDeleteButton, CancelDeleteButton, TextDeleteProf);
            display.init = this;
        }
    }    
}
