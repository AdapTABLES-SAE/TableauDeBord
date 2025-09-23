using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfAddJSON
{
    public string idProf;
    public string name;
    public ProfAddJSON(string id, string nameP){
        idProf=id;
        name=nameP;
    }

    public ProfClass ToClass()
    {
        ProfClass prof = new ProfClass(idProf);
        prof.name = name;
        return prof;
    }
}
