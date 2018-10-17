using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Version3D{
    public class SnapPointSystem : MonoBehaviour {
        public List<SnapPointSystemLayer> layer = new List<SnapPointSystemLayer>();


        private void Start() {
            foreach (Transform child in transform) {
                if(child.gameObject.GetComponent<SnapPointSystemLayer>() != null) {
                    layer.Add(child.gameObject.GetComponent<SnapPointSystemLayer>());
                }
            }
        }

        public void CheckLayer() {
            foreach(SnapPointSystemLayer layerX in layer) {
                if(layerX.objectsInTrigger.Count >= 16) {
                    foreach (Collider singleBlock in layerX.objectsInTrigger) {
                        foreach (Transform point in layerX.transform) {
                            if (Vector3.Distance(point.position, singleBlock.transform.position) < .5f) {
                                singleBlock.GetComponent<SingleBlock>().BuildBuilding(point);
                            }
                        }
                    }
                }                          
            }
        }
    }
}
