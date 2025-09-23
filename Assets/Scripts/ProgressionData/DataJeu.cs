using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class DataJeu : MonoBehaviour
{
    public TMP_Text nbLevelsGeneratedText;
    public TMP_Text nbCorrectAnswersText;
    public TMP_Text nbLevelsPlayedUntilEndText;
    public TMP_Text totalTimeText;
    public TMP_Text nbDeathsText;
    public TMP_Text totalCoinsText;
    public TMP_Text nbQuestionsMeetText;
    public TMP_Text nbExitsText;
    public TMP_Text maxLevelReachedText;

    public void Initialised(){
        StartCoroutine(APIManager.FetchStat(SetDataToText));
    }

    private void SetDataToText(GameStats stat){
        nbLevelsGeneratedText.text = "" + stat.nbLevelsGenerated;
        nbCorrectAnswersText.text = "" + stat.nbCorrectAnswers;
        nbLevelsPlayedUntilEndText.text = "" + stat.nbLevelsPlayedUntilEnd;
        totalTimeText.text = "" + stat.totalTime;
        nbDeathsText.text = "" + stat.nbDeaths;
        totalCoinsText.text = "" + stat.totalCoins;
        nbQuestionsMeetText.text = "" + stat.nbQuestionsMeet;
        nbExitsText.text = "" + stat.nbExits;
        maxLevelReachedText.text = "" + stat.maxLevelReached;
    }
}
