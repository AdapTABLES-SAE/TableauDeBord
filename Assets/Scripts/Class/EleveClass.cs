using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class EleveClass 
{
    public static List<EleveClass> studentsOfClassroom = new List<EleveClass>();
    public static EleveClass studentChosen;

    public string idStudent;
    public string nomEleve;
    public string prenomEleve;
    public string idClasse;

    //public EleveClass(string nomEleve, string prenomEleve, string idClasse){
    //    this.SetData(idClasse+DateTime.Now.ToString("MMddyyyyHHmmss") ,nomEleve ,prenomEleve ,idClasse);
    //}

    [JsonConstructor]
    public EleveClass(string id, string nom, string prenom, string idClasse){
        this.SetData(id, nom, prenom, idClasse);
    }

    private void SetData(string idStudent, string nomEleve, string prenomEleve, string idClasse){
        this.idStudent = idStudent;
        this.nomEleve = nomEleve;
        this.prenomEleve = prenomEleve;
        this.idClasse = idClasse;
    }

}
