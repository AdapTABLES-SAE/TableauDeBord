using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Globalization;
using System.Text;

public class ControllerDataClassRoom: MonoBehaviour
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

    public void AddClassroom(){
        ClassesClass newClass = new ClassesClass();
        newClass.name = inputFieldNom.text;
        newClass.id = inputFieldID.text;
        newClass.nbStudents = 0;
        StartCoroutine(APIManager.AddClasse(ProfClass.loggedTeacher.idProf, newClass, CheckSuccess));
    }

    
    public void CheckSuccess(bool result, ClassesClass classAdded)
    {
        if (result)
        {
            bandeauSucces.SetActive(true);
            ProfClass.loggedTeacher.classes.Add(classAdded);
        }
        else
            bandeauFailure.SetActive(true);

        StartCoroutine(Wait1Sec(result));
        
    }



    public void CancelAdd(){
        SceneManager.LoadScene("ListeClassesScene");
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
            SceneManager.LoadScene("ListeClassesScene");
        }
    }

    public void OnValueChangedInputFields()
    {
        if (inputFieldNom.text.Length == 0)
            return;

        string condensedName = "";

        string nameProf = ProfClass.loggedTeacher.name;
        nameProf = Removeaccents(nameProf);
        if (nameProf.Length > 2)
            condensedName += Removeaccents(nameProf.Substring(0,3).ToLower());
        else
            condensedName += Removeaccents(nameProf.ToLower());

        if (inputFieldNom.text.Length > 2)
            condensedName += Removeaccents(inputFieldNom.text.Substring(0, 3).ToLower());
        else
            condensedName += Removeaccents(inputFieldNom.text);

        condensedName += Random.Range(10,9999);
        inputFieldID.text = condensedName.ToLower();

        if (inputFieldNom.text.Length > 1)
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
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC).Replace(" ", "");
    }

}
