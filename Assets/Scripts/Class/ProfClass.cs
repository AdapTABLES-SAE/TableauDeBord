using System.Collections;
using System.Collections.Generic;
using System;


public class ProfClass
{

    public static ProfClass loggedTeacher;

    public string name;
    public string idProf;
    public List<ClassesClass> classes;

    public ProfClass(string idProf){

        this.idProf = idProf;
        name = "M. Duchemolle";
        classes = new List<ClassesClass>();
        ClassesClass firstClassesRoom = new ClassesClass();
        firstClassesRoom.id = "c1";
        firstClassesRoom.name = "mes chouchous CE2";
        firstClassesRoom.nbStudents = 15;
        classes.Add(firstClassesRoom);
        firstClassesRoom = new ClassesClass();
        firstClassesRoom.id = "c2";
        firstClassesRoom.name = "les connards de CM1";
        firstClassesRoom.nbStudents = 32;
        classes.Add(firstClassesRoom);

    }
}
