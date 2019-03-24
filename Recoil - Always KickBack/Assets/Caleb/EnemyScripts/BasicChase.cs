using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicChase : MonoBehaviour {

    [SerializeField]
    private float sightDistance = 10;
    [SerializeField]
    private Transform character;

    private NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        // Raycast to character
        RaycastHit hit;
        Ray ray = new Ray(transform.position, (character.position - transform.position));
        // If it hits, check if it hit character
        if (Physics.Raycast(ray, out hit, sightDistance)) {
            if (hit.transform.tag == "Player") {
                navAgent.destination = hit.transform.position;
            }
        }
	}
}
