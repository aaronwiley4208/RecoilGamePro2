using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGrounded : MonoBehaviour {
    
    public GunClip[] gunClips;
    
    private float distToGround;
    private bool reloaded = false;

    void Start ()
    {
        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;        
    }


    void Update()
    {
        if (Grounded() == true && reloaded == false) { 
            Reload();
        }
        else if(Grounded() == false)
        {
            reloaded = false;
        }
	}

    private bool Grounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);           
    }

    private void Reload()
    {
        foreach (GunClip clip in gunClips)
        {
            clip.Reload();
        }
        reloaded = true;
    }
}
