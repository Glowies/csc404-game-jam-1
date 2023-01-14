using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out HazardCollider hazard))
        {
            return;
        }

        _gameManager.EndGame();
    }
}
