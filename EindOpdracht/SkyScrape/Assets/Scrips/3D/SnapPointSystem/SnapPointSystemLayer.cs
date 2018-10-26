using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Version3D {
    public class SnapPointSystemLayer : MonoBehaviour {
        public int layer;
        public bool hasBuild = false;

        public List<Collider> objectsInTrigger = new List<Collider>();

        public void OnTriggerEnter(Collider other) {
            if(other.transform.tag != "TestTriggerBase") {
                objectsInTrigger.Add(other);
            }         
        }


        public void OnTriggerExit(Collider other) {
            if (other.transform.tag != "TestTriggerBase") {
                objectsInTrigger.Remove(other);
            }
        }
    }
}

