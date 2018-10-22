using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace extras {
    public class Obscurable : MonoBehaviour {
        private void Start() {
            // get all renderers in this object and its children:
            Renderer[] renders = GetComponentsInChildren<Renderer>();
            foreach (Renderer rendr in renders) {
                rendr.material.renderQueue = 2002; // set their renderQueue
            }
        }
    }
}

