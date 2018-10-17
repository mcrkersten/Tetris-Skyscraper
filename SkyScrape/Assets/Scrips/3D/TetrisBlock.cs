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
        private bool isUsed = false;
        public delegate void OnColission();
        public static event OnColission OnColissionEvent;

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
                    OnColissionEvent();
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
        }

        public void ActivateCollisions() {                  //PlayerControler activates colliders if this object is used
            foreach(Transform child in transform) {
                if (child.tag != "TestTriggerBase") {
                    child.GetComponent<Collider>().enabled = true;
                }           
            }
        }

        public void TestMovement(Vector3 newPos)
        {
            testColliderBase.transform.position = new Vector3(newPos.x, testColliderBase.transform.position.y, newPos.z);
            //ReturnPos();
            
        }


        public void ReturnPos() {
            testColliderBase.transform.position = testColliderBase.transform.parent.transform.position;
        }
    }
}

