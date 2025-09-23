using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControllerDataEnseignant : MonoBehaviour
{
    public TMP_InputField inputFieldID;
    public TMP_InputField inputFieldNom;
    public GameObject bandeauSucces;
    public GameObject bandeauFailure;
    public Button okButton;
    public Button cancelButton;

    void Start()
    {
        okButton.interactable = false;
        //cancelButton.interactable = false;
    }

    public void AddEnseignant()
    {
        StartCoroutine(APIManager.AddProf(inputFieldID.text, inputFieldNom.text, CheckSuccess));
    }


    public void CheckSuccess(bool result, ProfClass classAdded)
    {
        if (result)
        {
            bandeauSucces.SetActive(true);
        }
        else
            bandeauFailure.SetActive(true);

        StartCoroutine(Wait1Sec(result));

    }



    public void CancelAdd()
    {
        SceneManager.LoadScene("ListeProfScene");
    }


    IEnumerator Wait1Sec(bool result)
    {
        yield return new WaitForSeconds(1);

        bandeauFailure.SetActive(false);
        bandeauSucces.SetActive(false);
        inputFieldID.text = "";
        inputFieldNom.text = "";

        if (result)
        {
            SceneManager.LoadScene("ListeProfScene");
        }
    }

    public void OnValueChangedInputFields()
    {
        if (inputFieldNom.text.Length == 0 || inputFieldID.text.Length == 0)
        {
            okButton.interactable = false;
            return;
        }

        okButton.interactable = true;


    }
}
