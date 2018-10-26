using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {
    public class Tower : MonoBehaviour {
        private SnapPointSystem snapSystem;

        private static Tower instance = null;
        public static Tower Instance {
            get {
                if (instance == null) {
                    // This is where the magic happens.
                    //  FindObjectOfType(...) returns the first Tower object in the scene.
                    instance = FindObjectOfType(typeof(Tower)) as Tower;
                }

                // If it is still null, create a new instance
                if (instance == null) {
                    GameObject obj = new GameObject("Tower");
                    obj.transform.position = new Vector3(0, -2, 0);
                    obj.AddComponent(typeof(BoxCollider));                          //Generate BoxCollider
                    obj.GetComponent<BoxCollider>().size = new Vector3(16, 1, 16);  //Set Size of BoxCollider
                    obj.GetComponent<BoxCollider>().isTrigger = true;
                    instance = obj.AddComponent(typeof(Tower)) as Tower;
                    Debug.Log("Could not locate an Tower object.  Tower was Generated Automaticly.");
                }
                return instance;
            } 
        }


        private void Start() {
            snapSystem = Instantiate(InitManager.Instance.snapSystem).GetComponent<SnapPointSystem>();
        }


        private void OnTriggerEnter(Collider collision) {
            InitManager.Instance.lifes--;
        }
    }
}

