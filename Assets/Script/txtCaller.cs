using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class txtCaller : MonoBehaviour
{
    public string ReadTxt(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        string value = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd();
            reader.Close();           
        }

        else
            value = "파일이 없습니다.";

        return value;
    }
}
