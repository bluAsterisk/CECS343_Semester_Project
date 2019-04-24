using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPSingleton : Singleton<PlayerHPSingleton>
{
    //const string prefabLoc = "Prefab/Canvas/PlayerHPCanvas"; // Would not need it if it exist in game scene

    /*
    private void Awake()
    {
        //setPrefabName(prefabLoc); // Would not need it if it exist in game scene
    }
    */

    public GameObject GetObject()
    {
        return gameObject;
    }
}
