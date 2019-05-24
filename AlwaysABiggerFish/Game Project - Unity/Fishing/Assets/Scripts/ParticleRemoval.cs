using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRemoval : MonoBehaviour {

	// the particle system will destroy itself after half a second
	IEnumerator Start ()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
	}
	

}
