using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseNeonIntensity : MonoBehaviour {

    [SerializeField]
    private float intensity;
    private Color emissionColor;
    private Material material;
    private Color newEmission;
    [SerializeField]
    private float minIntensity;
    [SerializeField]
    private float timePerPeriod;
    [SerializeField]
    private float intensityDampener;

	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
        emissionColor = material.GetColor("_EmissionColor");
	}
	
	// Update is called once per frame
	void Update () {
        intensity = minIntensity + 1 + (Mathf.Sin((Time.time * 2 * Mathf.PI) / timePerPeriod)) / intensityDampener;
        newEmission = emissionColor * intensity;

        material.SetColor("_EmissionColor", newEmission);
	}
}
