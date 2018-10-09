using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {
    [RequireComponent(typeof(Rigidbody))]
    public class TetrisBlock : MonoBehaviour {

        protected List<GameObject> singleBlocks = new List<GameObject>();
        protected Rigidbody rigid;
        protected int score = 4;

        public delegate void OnColission();
        public static event OnColission OnColissionEvent;

        private void Start() {
            //Voor alle children in de transform.
            foreach (Transform child in transform) {
                singleBlocks.Add(child.gameObject);
            }
            rigid = this.GetComponent<Rigidbody>();
        }


        private void OnCollisionEnter(Collision collision) {
            if (OnColissionEvent != null) {
                OnColissionEvent();
            }
        }


        public int GetScore() {
            return score;
        }


        public void Release() {
            rigid.useGravity = true;
            rigid.constraints = RigidbodyConstraints.None;
        }


        
        public virtual void EndLock() {
            if(rigid.velocity.magnitude < .1f) {
                foreach (GameObject block in singleBlocks) {

                    //Round all rotations to a single non decimal number
                    block.transform.eulerAngles = new Vector3((float)System.Math.Round(this.gameObject.transform.eulerAngles.x, 1),
                                                              (float)System.Math.Round(this.gameObject.transform.eulerAngles.y, 1),
                                                              (float)System.Math.Round(this.gameObject.transform.eulerAngles.z, 1));
                    block.GetComponent<SingleBlock>().ReturnModel();
                }
            }
        }
    }
}

