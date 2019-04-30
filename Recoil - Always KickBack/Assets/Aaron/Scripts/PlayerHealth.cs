using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float HP = 3;
	public float barrierStr = 3;
    public ParticleSystem deathExpl;
    public ParticleSystem barrierEffect;
	ParticleSystem.MainModule barrierMain;
	public GameObject heart1;
	public GameObject heart2;
	public GameObject heart3;

    public float rechargeTimer = 3;
    public float barrierPsec = 0.1f;

    bool barrierActive = false;
    bool gotHit = false;
    float downTimer = 0;
    float timer = 0;

    bool invul = false;
    float invulTimer = 0;
    float invulLen = .2f;

    // Use this for initialization
    void Start () {
		barrierMain = barrierEffect.main;
		barrierMain.startColor = Color.green;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (HP <= 0) {
            StartCoroutine(Die());
        }        
		if (barrierStr > 0) {
            barrierEffect.Play();
            barrierMain.startColor = Color.Lerp(Color.red, Color.green, barrierStr/3);
		} else {
			barrierEffect.Stop();
        }
        if (barrierStr < 0) {
            barrierStr = 0;
        }
        if (barrierActive) {
            if (gotHit && (barrierStr > 0)){
                if (rechargeTimer <= downTimer){                    
                    downTimer = 0;
                    gotHit = false;
                }
                if (downTimer < rechargeTimer){
                    downTimer += Time.deltaTime;
                }
            }else if(gotHit && (barrierStr <= 0)){
                if (3*rechargeTimer <= downTimer)
                {
                    downTimer = 0;
                    gotHit = false;
                }
                if (downTimer < 3*rechargeTimer)
                {
                    downTimer += Time.deltaTime;
                }
            }else{
                if (1 <= timer && barrierStr < 3) {
                    if (barrierStr + barrierPsec > 3)
                        barrierStr = 3;
                    else
                        barrierStr += barrierPsec;
                    timer = 0;
                }            
                if (timer < 1) {
                    timer += Time.deltaTime;
                
                }
            }         
        }
        if (invul && barrierStr > 0) {
            if (invulLen <= invulTimer)
            {
                invulTimer = 0;
                invul = false;
                GetComponent<Renderer>().enabled = true;
            } else
            if (invulTimer < invulLen)
            {
                invulTimer += Time.deltaTime;
                GetComponent<Renderer>().enabled = false;
            }
        } else if (invul && barrierStr <= 0) {
            if (2*invulLen <= invulTimer)
            {
                invulTimer = 0;
                invul = false;
                GetComponent<Renderer>().enabled = true;
            }
            else
            if (invulTimer < 2*invulLen)
            {
                invulTimer += Time.deltaTime;
                GetComponent<Renderer>().enabled = false;
            }
        }
	}

    
    

    IEnumerator Die() {
        print("D E A D T");
        deathExpl.Play();
        barrierActive = false;
        GetComponent<GunHolder>().enabled = false;
        GetComponent<PlayerGroundMovement>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        yield return new WaitForSeconds(2);
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().detectCollisions = false;
        deathExpl.Stop();
    }

    private void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.layer == 8 || col.gameObject.layer == 11) {  //enemies & bullets layers, might change
			if (barrierStr > 0) {
				barrierStr = barrierStr - 1;
                gotHit = true;                
                timer = 0;
                downTimer = 0;                
                if (barrierStr <= 0)
                {
                    barrierStr = 0;
                }
            } else {
                if (!invul)
                {
                    HP = HP - 1;
                    if (heart1.activeSelf == true)
                    {
                        heart1.SetActive(false);
                    }
                    else if (heart2.activeSelf == true)
                    {
                        heart2.SetActive(false);
                    }
                    else
                    {
                        heart3.SetActive(false);
                    }
                }                
            }
            invul = true;
            invulTimer = 0;           
		}if(col.gameObject.name == "BarrierPickUp"){
			Destroy(col.gameObject);
            barrierActive = true;            
            if (barrierStr < 3){
				barrierStr = 3;
				barrierEffect.Play ();
			}
		}
        
        
    }

    
    
    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.layer == 11 || other.gameObject.tag == "Teleporter"){  //bullets layer, might change
			if (barrierStr > 0) {
				barrierStr = barrierStr - 1;
                gotHit = true;                
                timer = 0;
                downTimer = 0;
                
                if (barrierStr <= 0){
                    barrierStr = 0;
                }
			} else {
                if (!invul)
                {
                    HP = HP - 1;
                    if (heart1.activeSelf == true)
                    {
                        heart1.SetActive(false);
                    }
                    else if (heart2.activeSelf == true)
                    {
                        heart2.SetActive(false);
                    }
                    else
                    {
                        heart3.SetActive(false);
                    }
                }
            }
            invul = true;
            invulTimer = 0;
        }
    }
}
