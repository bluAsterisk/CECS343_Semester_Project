using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : Singleton<PlayerSingleton>
{
    [SerializeField]
    private GameObject player;
    
    public GameObject GetPlayer()
    {
        return player;
    }
    
}
