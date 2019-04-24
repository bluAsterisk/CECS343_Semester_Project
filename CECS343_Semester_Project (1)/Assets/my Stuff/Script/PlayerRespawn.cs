using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : Singleton<PlayerRespawn>
{
    private Transform spawnPoint;
    private void Start()
    {
        spawnPoint = transform;
    }
    public void RespawnPlayer()
    {
        // print("RespawnPlayer Start"); // To see Player respawn start
        GameObject test = Instantiate(PlayerSingleton.Instance.GetPlayer(), spawnPoint.position, spawnPoint.rotation);
    }

    public void MakeDead(GameObject player)
    {
        // Probably have to put effect
        Destroy(player);
        RespawnPlayer();
    }
}

