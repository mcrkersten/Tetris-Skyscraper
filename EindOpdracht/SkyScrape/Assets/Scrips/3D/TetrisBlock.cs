using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Version3D {
    [RequireComponent(typeof(Rigidbody))]
    public class TetrisBlock : MonoBehaviour {

        [HideInInspector]
        public List<GameObject> singleBlocks = new List<GameObject>();
        public GameObject testColliderBase;

        protected Rigidbody rigid;
        protected int score = 4;

        private bool testResult = false;
        public bool isUsed = false;
        public GameObject parent;

        public delegate void OnColission();
        public static event OnColission OnColissionEvent;

        public delegate void OnColissionProjector(Transform tetrisBlockParent);
        public static event OnColissionProjector OnColissionProjectorEvent;


        private void Start() {
            //Voor alle children in de transform.
            foreach (Transform child in transform) {
                singleBlocks.Add(child.gameObject);
            }
        }


        private void OnCollisionEnter(Collision collision) {
            if(this.gameObject != collision.gameObject)
            {
                if (OnColissionEvent != null && isUsed == false)
                {
                    isUsed = true;
                    foreach(Transform child in transform) {
                        child.gameObject.layer = 11;
                    }
                    this.gameObject.layer = 11;
                    OnColissionEvent();
                    if(OnColissionProjectorEvent != null) {
                        OnColissionProjectorEvent(this.transform);
                    }                   
                }
            }
        }

        public int GetScore() {
            return score;
        }


        public void Release() {
            foreach (Transform child in transform) {
                if (child.tag != "TestTriggerBase") {
                    child.GetComponent<SingleBlock>().isSet = true;
                }               
            }
            rigid = this.gameObject.GetComponent<Rigidbody>();
            rigid.useGravity = true;
            rigid.constraints = RigidbodyConstraints.None;
            this.transform.position = new Vector3(parent.transform.position.x, this.transform.position.y, parent.transform.position.z);
        }

        public void ActivateCollisions() {                  //PlayerControler activates colliders if this object is used
            foreach(Transform child in transform) {
                if (child.tag != "TestTriggerBase") {
                    child.GetComponent<Collider>().enabled = true;
                }           
            }
        }


        public void TestMovement(Vector3 newPos) {
            testColliderBase.transform.position = new Vector3(newPos.x, testColliderBase.transform.position.y, newPos.z);          
        }


        public void TestRotation(Vector3 newRot, float angle) {
            testColliderBase.transform.Rotate(newRot, angle, Space.World);
        }


        public void ReturnPos() {
            testColliderBase.transform.position = testColliderBase.transform.parent.transform.position;
            testColliderBase.transform.rotation = testColliderBase.transform.parent.transform.rotation;
        }
    }
}

