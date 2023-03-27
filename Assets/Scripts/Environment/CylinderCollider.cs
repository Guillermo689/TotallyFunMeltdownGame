using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)                         //The reason is a trigger instead of a collider is that allows the cylinder to pass through to prevent pulling the player
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.Fall();
        }
    }
}
