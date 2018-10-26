using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy {
    public class Block3D : MonoBehaviour {

        public bool hit = false;
        protected List<GameObject> localModels = new List<GameObject>();
        protected Rigidbody rigid;
        protected int score = 4;
        //private Vector3 position;
        private float[] Ypositions = new float[8] { 1.6f, 0f, -1.6f, -3.2f, -4.8f, -6.4f, -8f, -9.6f };


        private void Start() {
            hit = false;
            rigid = this.GetComponent<Rigidbody>();
            //Put every sprite of object in list
            foreach (Transform child in transform) {
                localModels.Add(child.gameObject);
            }
        }


        private void OnCollisionEnter(Collision collision) {
            hit = true;
        }


        //public void SetPosition(Vector3 newPos) {
        //    position = newPos;
        //}


        public void Release() {
            rigid.useGravity = true;
            rigid.constraints = RigidbodyConstraints.None;
        }


        public int GetScore() {
            return score;
        }


        //Lock Rigidbody and change sprite
        public virtual void EndGameLock() {
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            if (rigid.velocity.magnitude < 1f) {
                foreach (GameObject block in localModels) {
                    this.gameObject.transform.position = new Vector3((float)System.Math.Round(this.gameObject.transform.position.x, 1, System.MidpointRounding.AwayFromZero),

                                                            Closest(this.gameObject.transform.position.y),

                                                            (float)System.Math.Round(this.gameObject.transform.position.z, 1, System.MidpointRounding.AwayFromZero));

                    this.gameObject.transform.eulerAngles = new Vector3((float)System.Math.Round(this.gameObject.transform.eulerAngles.x, 1),
                                                            (float)System.Math.Round(this.gameObject.transform.eulerAngles.y, 1),
                                                            (float)System.Math.Round(this.gameObject.transform.eulerAngles.z, 1));

                    block.GetComponent<BlockCube>().ReturnModel();
                    print("Ik kom er langs");
                }
                //Sets te blocks on lines
                //this.transform.position = position;
            }
        }


        public bool CheckMovePosition(Vector3 side, bool detectSelf) {
            float lenght = .85f;
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            RaycastHit hit;
            //Fire a raycast and continue if it hit a collision.
            foreach (GameObject child in localModels) {
                if (Physics.Raycast(child.transform.position, transform.TransformDirection(side), out hit, lenght, layerMask)) {
                    //if the collision that is hit by the raycast is not from a child inside the parent object, stop the procces and return a false.
                    if (!hit.collider.transform.IsChildOf(this.gameObject.transform) && detectSelf == false) {
                        return false;
                    }
                }
            }
            return true;
        }


        public float Closest(float num) {
            float closest = 0;
            float smallestDiff = -10;

            for (int i = 1; i < Ypositions.Length; i++) {
                float currentDiff = System.Math.Abs(num - Ypositions[i]);
                if (currentDiff < smallestDiff) {
                    smallestDiff = currentDiff;
                    closest = Ypositions[i];
                }
            }
            return closest;
        }
    }
}
