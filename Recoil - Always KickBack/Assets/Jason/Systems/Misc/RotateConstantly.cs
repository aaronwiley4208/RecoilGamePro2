using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateConstantly : MonoBehaviour {
    [SerializeField]
    private float secondsPerRotation;

    public float anglesPerSecond;

	// Use this for initialization
	void Start () {
        anglesPerSecond = 360 / secondsPerRotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, anglesPerSecond * Time.deltaTime, 0, Space.World);
	}
}
