using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : Singleton<PlayerSingleton>
{
    // Hold Player; Used for player Respawn.
    [SerializeField]
    private GameObject player;
    
    public GameObject GetPlayer()
    {
        return player;
    }
    
}
