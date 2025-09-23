using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text;
using System.Globalization;
using System;

public class ControllerDataEtu : MonoBehaviour
{
    public TMP_InputField inputFieldID;
    public TMP_InputField inputFieldNom;
    public TMP_InputField inputFieldPrenom;
    public GameObject bandeauSucces;
    public GameObject bandeauFailure;
    public Button okButton;
    public Button cancelButton;

    private EleveClass eleveAdded;

    void Start()
    {
        okButton.interactable = false;
        //cancelButton.interactable = false;
        eleveAdded = null;
    }

    public void AddStudent(){
        eleveAdded = new EleveClass(inputFieldID.text, inputFieldNom.text, inputFieldPrenom.text, ClassesClass.classeChosen.id);
        
        StartCoroutine(APIManager.AddStudent(eleveAdded,Success));
    }

    public void CancelAdd(){
        SceneManager.LoadScene("ListeElevesScene");
    }

    public void Success(bool result){
        Debug.Log(result);

        if (result)
        {
            bandeauSucces.SetActive(true);
            EleveClass.studentsOfClassroom.Add(eleveAdded);
        }
        else
            bandeauFailure.SetActive(true);

        StartCoroutine(Wait1Sec(result));
    }

    IEnumerator Wait1Sec(bool result)
    {
        yield return new WaitForSeconds(1);

        bandeauFailure.SetActive(false);
        bandeauSucces.SetActive(false);
        inputFieldID.text = "";
        inputFieldNom.text = "";
        inputFieldPrenom.text = "";

        if (result)
        {
            SceneManager.LoadScene("ListeElevesScene");
        }
    }

    public void OnValueChangedInputFields()
    {
        if (inputFieldPrenom.text.Length == 0)
            return;
        if (inputFieldNom.text.Length == 0)
            return;

        string firstLettername = "";
        if (inputFieldPrenom.text.Length > 0)
            firstLettername = Removeaccents(inputFieldPrenom.text.Substring(0, 1).ToLower());
        string condensedName = "";
        if (inputFieldNom.text.Length > 5)
            condensedName = Removeaccents(inputFieldNom.text.Substring(0, 5).ToLower());
        else
            condensedName = Removeaccents(inputFieldNom.text);

        inputFieldID.text = firstLettername + condensedName + UnityEngine.Random.Range(100,999);
        inputFieldID.text = inputFieldID.text.ToLower();

        if (inputFieldPrenom.text.Length > 0 && inputFieldNom.text.Length > 0)
        {
            okButton.interactable = true;
            //cancelButton.interactable = true;
        } else
        {
            okButton.interactable = false;
            //cancelButton.interactable = false;
        }
    }

    private string Removeaccents(string inputText)
    {
        var normalizedString = inputText.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

        for (int i = 0; i < normalizedString.Length; i++)
        {
            char c = normalizedString[i];
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if(unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC).Replace(" ","");
    }
    
}
