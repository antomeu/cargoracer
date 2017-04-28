using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyJSON;

public class APIManager : MonoBehaviour
{
    public string url = "";
    public string Response;
    
    IEnumerator Start()
    {
        WWW www = new WWW(url);
        yield return www;
        var data = JSON.Load(www.text);
        foreach (var pair in data as ProxyObject)
        {
            if (string.Equals(pair.Key, "isSuccess"))
                Debug.Log(bool.Parse(data["isSuccess"])); //pair.Key + " = " + pair.Value);
        }
        
        Response = www.text;
    }
}
