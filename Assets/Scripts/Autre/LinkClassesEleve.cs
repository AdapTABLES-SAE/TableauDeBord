using System.Collections;
using System.Collections.Generic;
using System;

public class LinkClassesEleve 
{
    static List<LinkClassesEleve> lienClasseEleves= new List<LinkClassesEleve>();

    public Guid idClasse;
    public List<EleveClass> Eleves;

    public LinkClassesEleve(Guid idClasse){
        this.idClasse=idClasse;
        Eleves=new List<EleveClass>();
        lienClasseEleves.Add(this);
    }
    
    public void AddEleve(EleveClass eleveClass){
        Eleves.Add(eleveClass);
    }

    public int GetNumberOfEleve(){
        return Eleves.Count;
    }

    public static LinkClassesEleve GetLinkClasseEleveWithClasseId(Guid idClasse){
        LinkClassesEleve resultfinal=null;
        foreach(LinkClassesEleve lienClasseEleve in lienClasseEleves)
        {
            if(lienClasseEleve.idClasse==idClasse)
                resultfinal=lienClasseEleve;
        }
        return resultfinal;
    }
}
