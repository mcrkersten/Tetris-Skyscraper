using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {
    public class Tower : MonoBehaviour {

        public List<GameObject> playedBlocks = new List<GameObject>();
        private SnapPointSystem snapSystem;

        public delegate void ScorePoints(int score);
        public static event ScorePoints SendScore;

        public delegate void BlockFall();
        public static event BlockFall OnBlockFall;

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
                    obj.AddComponent(typeof(BoxCollider));                          //Generate BoxCollider
                    obj.GetComponent<BoxCollider>().size = new Vector3(16, 1, 16);  //Set Size of BoxCollider
                    instance = obj.AddComponent(typeof(Tower)) as Tower;
                    Debug.Log("Could not locate an Tower object.  Tower was Generated Automaticly.");
                }
                return instance;
            }
        }


        private void Start() {
            snapSystem = Instantiate(InitManager.Instance.snapSystem).GetComponent<SnapPointSystem>();
        }


        public void GetScore() {
            int score = 0;
            foreach (GameObject block in playedBlocks) {
                score += block.GetComponent<TetrisBlock>().GetScore();
            }
            if (SendScore != null) {
                SendScore(score);       //Raise score event with a int of score to add
            }
        }


        private void OnTriggerEnter(Collider collision) {
            OnBlockFall();
        }


        public void CheckLayer() {
            snapSystem.CheckLayer();
        }
    }
}

