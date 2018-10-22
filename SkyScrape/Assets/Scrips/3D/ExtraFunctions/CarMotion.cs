using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMotion : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.left * Time.deltaTime * 5, Space.World);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.tag == "DestroyObject") {
            Destroy(gameObject);
        }
    }
}
