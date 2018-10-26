using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Version3D {
    public class ProjectorBlock : MonoBehaviour {
        Vector3 rotation = new Vector3(90, 0, 0);
        Projector projector;

        // Update is called once per frame
        private void Awake() {
            TetrisBlock.OnColissionProjectorEvent += DestroyThis;
            projector = this.gameObject.GetComponent<Projector>();
        }


        void Update() {
            this.transform.eulerAngles = rotation;
            Ray downRay = new Ray(transform.position, Vector3.down);
            List<float> distance = new List<float>();
            RaycastHit[] allHits;
            allHits = Physics.RaycastAll(downRay, 15);
            foreach (RaycastHit bong in allHits) {  
                if (!bong.transform.parent) {

                    distance.Add(bong.distance);
                                  
                }
            }
            if (this.projector != null) {
                if(distance.Count > 1) {
                    projector.farClipPlane = distance.Min();
                }          
            }
        }


        private void DestroyThis(Transform tetrisBlockParent) {
            if(tetrisBlockParent == this.gameObject.transform.parent.gameObject.transform.parent) {
                if(projector != null) {
                    Destroy(projector);
                }              
            }            
        }
    }
}

