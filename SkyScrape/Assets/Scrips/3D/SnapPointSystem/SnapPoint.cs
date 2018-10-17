using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {
    public class SnapPoint : MonoBehaviour {
        public bool isTaken = false;

        private void OnTriggerEnter(Collider other) {
            isTaken = true;
        }
        private void OnTriggerExit(Collider other) {
            isTaken = false;
        }
    }
}

