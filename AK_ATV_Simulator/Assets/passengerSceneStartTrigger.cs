﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passengerSceneStartTrigger : MonoBehaviour
{
    public GameObject endTrigger;
    public GameObject mountainPassTrigger;
    public string scenario;
    public GameObject passenger;
    public GameObject secondPassenger;
    bool doOnce = false;

    /*! This coroutine activates the second passenger and deactivates the start trigger */
    /*! Just spawning the secondPassenger won't make it stay on the atv if the atv has any movement.
     * Therefore, the velocity of the atv and secondPassenger had to be set to zero */
    IEnumerator StartingScenarios(Collider other)
    {
        secondPassenger.SetActive(true);
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        secondPassenger.GetComponent<Rigidbody>().velocity = Vector3.zero;
        secondPassenger.GetComponent<Rigidbody>().isKinematic = true;
        other.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForEndOfFrame();
        other.GetComponent<Rigidbody>().isKinematic = false;
        secondPassenger.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "VehicleCoords" && doOnce == false)
        {
            doOnce = true;
            Debug.Log("Starting scenario for " + other.gameObject.tag);
            other.GetComponent<VehicleScenario>().Update_Scenario(scenario);
            endTrigger.SetActive(true);
            passenger.SetActive(true);
            mountainPassTrigger.SetActive(false);
            StartCoroutine(StartingScenarios(other));
        }
    }
}
