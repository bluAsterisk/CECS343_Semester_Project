using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    // Spawn point for item
    [SerializeField]
    private Transform SpawnPoint;

    [SerializeField]
    private GameObject[] item;

    [SerializeField]
    private float yForce;
    
    private void Start()
    {
        this.GetComponent<Animator>().speed = 0;
        if (SpawnPoint == null)
        {
            Transform[] tCollect;
            tCollect = this.GetComponentsInChildren<Transform>();
            foreach (Transform t in tCollect)
            {
                if (t.CompareTag("Spawn Point"))
                {
                    SpawnPoint = t;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Animator>().speed = 1;
            CreateItem();
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void CreateItem()
    {
        GameObject newItem = Instantiate(item[0], SpawnPoint.position, SpawnPoint.rotation);
        newItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, yForce), ForceMode2D.Impulse);
    }

}
