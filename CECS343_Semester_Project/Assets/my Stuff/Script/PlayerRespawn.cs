using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : Singleton<PlayerRespawn>
{
    // Respawns player by creating another one.
    public void RespawnPlayer()
    {
        // print("RespawnPlayer Start"); // To see Player respawn start
        Instantiate(PlayerSingleton.Instance.GetPlayer(), transform.position, transform.rotation);
    }
    
    // Sets the respawn point using this objects transform.
    public void SetSpawnPoint(Transform point)
    {
        transform.position = point.position;
        transform.rotation = point.rotation; // Not needed in a 2D environment
    }
}

