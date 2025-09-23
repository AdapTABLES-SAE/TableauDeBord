using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleveClassModifyJSON
{
    public string idStudent;
    public string nomEleve;
    public string prenomEleve;
    public EleveClassModifyJSON(EleveClass eleve){
        idStudent=eleve.idStudent;
        nomEleve=eleve.nomEleve;
        prenomEleve=eleve.prenomEleve;
    }
}
