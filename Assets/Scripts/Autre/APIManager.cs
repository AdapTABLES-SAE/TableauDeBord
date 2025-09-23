using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using XCharts.Runtime;

public class APIManager{

    public static string SERVER_URL = "http://localhost:8080/FrameworkAPI/";
    //public static string SERVER_URL = "https://dungeon-generator.univ-lemans.fr/FrameworkAPI/";
    public static string LOGIN_TEACHER = "login/teacher/";
    public static string GET_TEACHER = "data/teacher/";
    public static string EQUIP_URL = "store/learner/";
    public static string STATS_URL = "statistics/learner/";
    public static string ADD_CLASSROOM = "data/classroom/";
    public static string MODIF_CLASSROOM = "data/classroom/";
    public static string DELETE_CLASSROOM_1 = "data/teacher/";
    public static string DELETE_CLASSROOM_2 = "/classroom/";
    public static string GET_STUDENTS_1 = "data/students/teacher/";
    public static string GET_STUDENTS_2 = "/classroom/";
    public static string ADD_STUDENT = "data/student";
    public static string MODIF_STUDENT = "data/student";
    public static string DELETE_STUDENT_1 = "data/teacher/";
    public static string DELETE_STUDENT_2 = "/classroom/";
    public static string DELETE_STUDENT_3 = "/learner/";
    public static string ADD_PROF = "data/teacher/";
    public static string GET_LEARNINGPATH = "path/training/learner/";
    public static string GET_LEVEL_1 = "results/learner/";
    public static string GET_LEVEL_2 = "/objective/";
    public static string GET_LEVEL_3 = "/level/";
    public static string SAVE_LEARNINGPATH = "path/training/";
    public static string DELETE_PROF = "data/teacher/";
    public static string GET_TEACHERS = "data/teachers/";

    public static IEnumerator GetProf(string teacherId, Action<ProfClass> callback)
    {
        string url = SERVER_URL + GET_TEACHER + teacherId;
        Debug.Log("Url login : " + url);
        Debug.Log(teacherId);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Tutorial.enable = false;
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string resultJson = webRequest.downloadHandler.text;

                    Debug.Log(resultJson);

                    ProfClass prof = JsonConvert.DeserializeObject<ProfClass>(resultJson);
                    callback(prof);

                    break;
            }
            webRequest.Dispose();
        }
    }

    public static IEnumerator CheckLoginProf(string teacherId, Action<string> callback)
    {
        string url = SERVER_URL + LOGIN_TEACHER + teacherId;
        Debug.Log("Url check login : " + url);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    //Debug.LogError("Error: " + webRequest.error);
                    callback("KO");
                    break;
                case UnityWebRequest.Result.Success:
                    //Tutorial.enable = false;
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string resultJson = webRequest.downloadHandler.text;

                    Debug.Log(resultJson);

                    callback(resultJson);

                    break;
            }
            webRequest.Dispose();
        }
    }

    public static IEnumerator GetStudents(string idClasse,string idProf, Action<List<EleveClass>> callback)
    {
        string url = SERVER_URL + GET_STUDENTS_1 + idProf + GET_STUDENTS_2 + idClasse;
        Debug.Log("Url get students : " + url);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    //List<EleveClass> listError = new List<EleveClass>();
                    //listError.Add(new EleveClass("Erreur requête","Veuillez Réessayer","1"));
                    callback(null);

                    break;
                case UnityWebRequest.Result.Success:
                    //Tutorial.enable = false;
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string resultJson = webRequest.downloadHandler.text;

                    Debug.Log(resultJson);

                    List<EleveClass> elevesFromJsonGood = JsonConvert.DeserializeObject<List<EleveClass>>(resultJson);

                    callback(elevesFromJsonGood);

                    break;
            }
            webRequest.Dispose();
        }
    }

    public static IEnumerator AddClasse(string idProf,ClassesClass classe,Action<bool, ClassesClass> callback)
    {
        string url = SERVER_URL + ADD_CLASSROOM;
        Debug.Log("url add class " + url);

        // EquipmentsJSON json = new EquipmentsJSON(items);

        ClasseAddJSON classAdded = new ClasseAddJSON(idProf,classe);

        string data = JsonConvert.SerializeObject(classAdded, Formatting.Indented);
        Debug.Log(data);

        // {
        //     idProf : "",
        //     classe : {
        //         idClasse : "",
        //         name : "",
        //         nbStudents : 45465
        //     }
        // }

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url,data,"application/json"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    callback(false,null);
                    break;
                case UnityWebRequest.Result.Success:
                    callback(true,classe);
                    break;
            }

            webRequest.Dispose();
        }
    }

    public static IEnumerator ModifClasse(string idClass,string newname,Action<bool> callback)
    {
        string url = SERVER_URL + MODIF_CLASSROOM;
        Debug.Log("url modify class name " + url);

        // EquipmentsJSON json = new EquipmentsJSON(items);

        ClasseModifyJSON classModify = new ClasseModifyJSON(idClass,newname);

        string data = JsonConvert.SerializeObject(classModify, Formatting.Indented);
        Debug.Log(data);

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "PUT"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    callback(false);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    callback(true);
                    break;
            }

            webRequest.Dispose();
        }
    }

    public static IEnumerator DeleteClasse(string idClasse,string idProf,Action<bool> callback)
    {
        Debug.Log("Delete Api");
        string url = SERVER_URL + DELETE_CLASSROOM_1 + idProf + DELETE_CLASSROOM_2 + idClasse;
        Debug.Log(url);

        using (UnityWebRequest webRequest = UnityWebRequest.Delete(url))
        {
            Debug.Log("Call Delete");
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    callback(false);
                    Debug.LogWarning("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    callback(true);
                    break;
            }

            webRequest.Dispose();
        }
    }

    public static IEnumerator AddStudent(EleveClass eleve, Action<bool> callback)
    {
        string url = SERVER_URL + ADD_STUDENT;
        Debug.Log("Add student " + url);


        string data = JsonConvert.SerializeObject(eleve, Formatting.Indented);
        Debug.Log(data);

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    callback(false);
                    break;
                case UnityWebRequest.Result.Success:
                    string resultJson = webRequest.downloadHandler.text;
                    if (resultJson != "")
                        callback(false);
                    else
                        callback(true);
                    break;
            }

            webRequest.Dispose();
        }
    }

    public static IEnumerator ModifStudent(EleveClass eleve,Action<bool> callback)
    {
        string url = SERVER_URL + MODIF_STUDENT;
        Debug.Log("Modify student " + url);

        string data = JsonConvert.SerializeObject(eleve, Formatting.Indented);
        Debug.Log(data);

        // {
        //     idProf : "",
        //     classe : {
        //         idClasse : "",
        //         name : "",
        //         nbStudents : 45465
        //     }
        // }

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "PUT"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    callback(false);
                    break;
                case UnityWebRequest.Result.Success:
                    string resultJson = webRequest.downloadHandler.text;
                    if(resultJson!="")
                        callback(false);
                    else
                        callback(true);
                    break;
            }

            webRequest.Dispose();
        }
    }

    public static IEnumerator DeleteStudent(EleveClass eleve,string idProf, Action<bool> callback)
    {
        string url = SERVER_URL + DELETE_STUDENT_1 + idProf + DELETE_STUDENT_2 + eleve.idClasse + DELETE_STUDENT_3 + eleve.idStudent;
        Debug.Log("Delete student" + url);

        // {
        //     idProf : "",
        //     classe : {
        //         idClasse : "",
        //         name : "",
        //         nbStudents : 45465
        //     }
        // }

        using (UnityWebRequest webRequest = UnityWebRequest.Delete(url))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    callback(false);
                    Debug.LogWarning("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    callback(true);
                    break;
            }

            webRequest.Dispose();
        }
    }

    public static IEnumerator FetchStat(Action<GameStats> callback)
    {
        string url = SERVER_URL + STATS_URL + EleveClass.studentChosen.idStudent;
        //url = UnityWebRequest.EscapeURL(url); 
        Debug.Log("Fetch stats / GET / " + url);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    //Debug.LogError("Error: " + webRequest.error);
                    //Tutorial.enable = true;

                    //GameStats stats = new GameStats();
                    //Debug.Log(JsonConvert.SerializeObject(stats, Formatting.Indented));
                    string resultJsonError = "{\"nbLevelsGenerated\": 1,\"nbCorrectAnswers\": 0,\"nbLevelsPlayedUntilEnd\": 0,\"nbDeaths\": 0,\"totalCoins\": 0,\"nbQuestionsMeet\": 0,\"nbExits\": 0,\"maxLevelReached\": 1}";
                    GameStats statsError = JsonConvert.DeserializeObject<GameStats>(resultJsonError);
                    callback(statsError);
                    //errorPanel.SetActive(true);
                    break;
                case UnityWebRequest.Result.Success:
                    //Tutorial.enable = false;
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string resultJson = webRequest.downloadHandler.text;

                    GameStats stats = JsonConvert.DeserializeObject<GameStats>(resultJson);
                    Debug.Log(resultJson);
                    callback(stats);

                    break;
            }
            webRequest.Dispose();
        }
    }

    public static IEnumerator FetchEquipments(Action<EquipmentsJSON> callback)
    {
        string url = SERVER_URL + EQUIP_URL + EleveClass.studentChosen.idStudent;
        //url = UnityWebRequest.EscapeURL(url); 
        Debug.Log("Fetch equipments / GET / " + url);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    //GameStats stats = new GameStats();
                    string resultJsonError = "{\"items\": [{\"id\": \"BRACELET\",\"isActivated\": true,\"isBought\": true},{\"id\": \"BELT\",\"isActivated\": false,\"isBought\": true},{\"id\": \"AMOR\",\"isActivated\": true,\"isBought\": true}]}";
                    EquipmentsJSON equipsError = JsonConvert.DeserializeObject<EquipmentsJSON>(resultJsonError);
                    callback(equipsError);
                    //errorPanel.SetActive(true);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string resultJson = webRequest.downloadHandler.text;

                    Debug.Log(resultJson);
                    if (resultJson.Equals("{}"))
                        callback(null);
                    EquipmentsJSON equips = JsonConvert.DeserializeObject<EquipmentsJSON>(resultJson);
                    //Debug.Log(stats.nbLevelsGenerated);
                    callback(equips);

                    break;
            }
            webRequest.Dispose();
        }
    }

    public static IEnumerator AddProf(string identifiant,string nom, Action<bool, ProfClass> callback)
    {
        string url = SERVER_URL + ADD_PROF;
        Debug.Log("Add teacher " + url);

        ProfAddJSON profAdded = new ProfAddJSON(identifiant,nom);

        string data = JsonConvert.SerializeObject(profAdded, Formatting.Indented);
        Debug.Log(data);

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    callback(false,profAdded.ToClass());
                    break;
                case UnityWebRequest.Result.Success:
                    string resultJson = webRequest.downloadHandler.text;
                    Debug.Log(resultJson);
                    if(resultJson!="")
                        callback(false, profAdded.ToClass());
                    else
                        callback(true, profAdded.ToClass());
                    break;
            }

            webRequest.Dispose();
        }
    }

    public static IEnumerator GetProgression(string teacherId, string idClasse, string idStudent, Action<LearningPathClass> callback, TextAsset file) 
    {
        string url = SERVER_URL + GET_LEARNINGPATH + idStudent;
        Debug.Log("Get progress " + url);
        LearningPathJSON lp = new LearningPathJSON();

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    string resultTestJSON = file.text;//File.ReadAllText(".\\Assets\\Resources\\message_5.txt");
                    lp = JsonConvert.DeserializeObject<LearningPathJSON>(resultTestJSON);
                    Debug.Log(lp.JSONToClass().objectifs.Count);
                    lp.learningPathID = "" + idStudent + "-LP";
                    Debug.Log(lp.learningPathID);
                    callback(lp.JSONToClass());
                    break;
                case UnityWebRequest.Result.Success:
                    //Tutorial.enable = false;
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string resultJson = webRequest.downloadHandler.text;
                    lp = JsonConvert.DeserializeObject<LearningPathJSON>(resultJson);
                    //Debug.Log(lp.JSONToClass());
                    callback(lp.JSONToClass());

                    break;
            }
            webRequest.Dispose();
        }
    }

    public static IEnumerator GetProgressionLevel(LevelClass level, string idObjective, string idStudent, Action<LevelClass,DataLevelJSON> callback)
    {
        string url = SERVER_URL + GET_LEVEL_1 + idStudent + GET_LEVEL_2 + idObjective + GET_LEVEL_3 + level.id;
        Debug.Log("Get progress " + url);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            // Requête stoper entre là
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            // et là
            string[] pages = url.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    callback(level,null);
                    break;
                case UnityWebRequest.Result.Success:
                    //Tutorial.enable = false;
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string resultJson = webRequest.downloadHandler.text;
                    DataLevelJSON lp = JsonConvert.DeserializeObject<DataLevelJSON>(resultJson);
                    //Debug.Log(lp.JSONToClass());
                    callback(level,lp);

                    break;
            }
            webRequest.Dispose();
        }
    }

    public static IEnumerator SaveObjectifs(LearningPathClass lp, EleveClass learner, Action<bool> callback)
    {
        string url = SERVER_URL + SAVE_LEARNINGPATH;
        Debug.Log("POST LP " + url);

        // EquipmentsJSON json = new EquipmentsJSON(items);
        LearningPathJSONPost lpJson= new LearningPathJSONPost(learner.idStudent);
        lpJson.TranfertToJSON(lp);

        string data = JsonConvert.SerializeObject(lpJson, Formatting.Indented);
        Debug.Log("data sent : \n" + data);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url,data,"application/json"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    callback(false);
                    break;
                case UnityWebRequest.Result.Success:
                    callback(true);
                    break;
            }

            webRequest.Dispose();
        }
    }

    public static IEnumerator SaveObjectifs(LearningPathClass lp, EleveClass learner, Action<bool,EleveClass> callback)
    {
        string url = SERVER_URL + SAVE_LEARNINGPATH;
        Debug.Log("Save progress " + url);

        // EquipmentsJSON json = new EquipmentsJSON(items);
        LearningPathJSONPost lpJson = new LearningPathJSONPost(learner.idStudent);
        lpJson.TranfertToJSON(lp);

        string data = JsonConvert.SerializeObject(lpJson, Formatting.Indented);
        Debug.Log("data sent : \n" + data);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, data, "application/json"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    callback(false,learner);
                    break;
                case UnityWebRequest.Result.Success:
                    callback(true,learner);
                    break;
            }

            webRequest.Dispose();
        }
    }

    public static IEnumerator DeleteProf(string idProf, Action<bool> callback)
    {
        string url = SERVER_URL + DELETE_PROF + idProf;
        Debug.Log("Delete student" + url);

        using (UnityWebRequest webRequest = UnityWebRequest.Delete(url))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    callback(false);
                    Debug.LogWarning("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    callback(true);
                    break;
            }

            webRequest.Dispose();
        }
    }

    public static IEnumerator GetProfs(Action<List<ProfClass>> callback)
    {
        string url = SERVER_URL + GET_TEACHERS;
        Debug.Log("Url login : " + url);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Tutorial.enable = false;
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string resultJson = webRequest.downloadHandler.text;

                    Debug.Log(resultJson);

                    List<ProfClass> prof = JsonConvert.DeserializeObject<List<ProfClass>>(resultJson);
                    callback(prof);

                    break;
            }
            webRequest.Dispose();
        }
    }
}
