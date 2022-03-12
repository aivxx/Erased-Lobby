using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform spawnPoint;
    public CharacterController player;

    private void OnTriggerEnter(Collider other)
    {
        // Collect clues
        if(other.CompareTag("Clues"))
        {
            Debug.Log("Clue Collected!");
        }
    

    // Respawn after hitting fall zone
    if(other.CompareTag("Fall Zone"))
        {
            Debug.Log("Player out of bounds, respawning...");
            player.enabled = false;
            transform.position = spawnPoint.position;
            player.enabled = true;
        }
}
}
