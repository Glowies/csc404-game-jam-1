using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public GameObject DedText;
    public GameObject WinText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HazardCollider hazard))
        {
            DedText.SetActive(true);
            return;
        }

        if(other.TryGetComponent(out WinCollider win))
        {
            WinText.SetActive(true);
            return;
        }
    }
}
