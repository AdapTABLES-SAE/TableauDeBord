using UnityEngine;
public class DataTransfer
{
    private static DataTransfer instance = null;
    private static readonly object padlock = new object();

    DataTransfer()
    {
    }

    public static DataTransfer Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DataTransfer();
                }
                return instance;
            }
        }
    }
    public void SetDataString(string data,string name){
        PlayerPrefs.SetString(name,data);
    }
    public void SetDataInt(int data,string name){
        PlayerPrefs.SetInt(name,data);
    }
    public void SetDataFloat(float data,string name){
        PlayerPrefs.SetFloat(name,data);
    }
    public string GetDataString(string name){
        string data = PlayerPrefs.GetString(name);
        PlayerPrefs.DeleteKey(name);
        return data;
    }
    public int GetDataInt(string name){
        int data = PlayerPrefs.GetInt(name);
        PlayerPrefs.DeleteKey(name);
        return data;
    }
    public float GetDataFloat(string name){
        float data = PlayerPrefs.GetFloat(name);
        PlayerPrefs.DeleteKey(name);
        return data;
    }

}