using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GoalZoneScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        FirstPersonController player = other.GetComponentInChildren<FirstPersonController>();
        if (player != null)
        { player._playerHUD.ShowVictoryScreen(); }

    }

}
