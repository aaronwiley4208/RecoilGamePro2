using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns and places bayonettes for this gun
/// </summary>
public class BayonetteHolder : MonoBehaviour {
    [SerializeField]
    private GameObject knifePrefab;
    [SerializeField]
    private Transform playerObj;
    [SerializeField]
    [Tooltip("Don't edit during runtime")]
    private int numKnives;

    [Header("Options")]
    [SerializeField]
    [Tooltip("How far away from the player the knives are held")]
    private float holdingDistance;
    [SerializeField]
    [Tooltip("How far apart each knife should be")]
    private float spreadAngleIncrement;
    [SerializeField]
    [Tooltip("How long the knife should be")]
    private float knifeScale;

    // The current spread, upped whenever the number of knives is upped
    private float knifeSpreadAngle = 0;
    // Our collection of knives
    private List<GameObject> knives;

	// Use this for initialization
	void Start () {
        knives = new List<GameObject>();
		for (int i = 0; i < numKnives; i++) {
            SpawnKnife();
        }
        knifeSpreadAngle = numKnives * spreadAngleIncrement;
	}
	
	// Update is called once per frame
	void Update () {
        if (numKnives == 0) return;

        // Find the mouse pose and where to put the knives
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = playerObj.transform.position.z - Camera.main.transform.position.z;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Vector3 playerToMouse = mousePos - transform.position;
        Vector3 playerToBayonette = -playerToMouse.normalized;
        Vector3 initialHoldingPoint = (playerToBayonette * holdingDistance) + playerObj.position;

        // Place the kniv(f)e(s)
        if (numKnives == 1) {
            knives[0].transform.position = initialHoldingPoint;
            knives[0].transform.rotation = Quaternion.LookRotation(Vector3.forward, playerToBayonette);
        } else {
            Vector3[] directions = ComputeSpread(playerToBayonette);
            for (int i = 0; i < knives.Count; i++) {
                knives[i].transform.position = (directions[i] * holdingDistance) + playerObj.position;
                knives[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, directions[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
            AddKnife();
	}

    private void SpawnKnife() {
        GameObject knife = Instantiate(knifePrefab, transform);
        knife.transform.localScale = new Vector3(1, knifeScale, 1);
        knives.Add(knife);
    }

    // Computes and returns the directions for this spread pattern.
    private Vector3[] ComputeSpread(Vector3 startDirection) {
        Vector3[] directions = new Vector3[numKnives];
        // The interval between directions
        float spreadIncrement = knifeSpreadAngle / (numKnives - 1);
        // The first direction in the spread, and we'll spread from there
        Vector3 initialDir = Quaternion.AngleAxis(knifeSpreadAngle / 2, Vector3.forward) * startDirection;
        // Go through all the knives, making a dir for them all
        for (int i = 0; i < numKnives; i++)
            directions[i] = Quaternion.AngleAxis(-spreadIncrement * i, Vector3.forward) * initialDir;
        return directions;
    }

    public void AddKnife() {
        numKnives++;
        SpawnKnife();
        knifeSpreadAngle += spreadAngleIncrement;
    }

    public void ScaleKnives(float scaleDelta) {
        knifeScale *= (1 + scaleDelta);
        foreach (GameObject knife in knives)
            knife.transform.localScale = new Vector3(1, knifeScale, 1);
    }
}
