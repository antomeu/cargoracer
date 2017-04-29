using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Conclify;
using Conclify.Game;
using TinyJSON;

public class APIManager : MonoBehaviour
{


    private ConclifyApi GetApi()
    {
        ConclifyApi api = GetComponent<ConclifyApi>();
        return api;
    }

    

}
