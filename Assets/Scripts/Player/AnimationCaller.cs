using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCaller : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void GetUp()                                             //This is trigged by the animation to activate the player again
    {
        _playerMovement.GetUp();
    }
}
