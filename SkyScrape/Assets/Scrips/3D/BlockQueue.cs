using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Version3D {
    public class BlockQueue : MonoBehaviour {

        public PlayerController controller;
        public GameObject[] blockShapes;
        public List<GameObject> blockQueue = new List<GameObject>();

        private int queueNumber;
        private List<GameObject> queuePosition = new List<GameObject>();

        private static BlockQueue instance = null;
        public static BlockQueue Instance {
            get {
                if (instance == null) {
                    // This is where the magic happens.
                    //  FindObjectOfType(...) returns the first BlockQueue object in the scene.
                    instance = FindObjectOfType(typeof(BlockQueue)) as BlockQueue;
                }

                // If it is still null, create a new instance
                if (instance == null) {
                    GameObject obj = new GameObject("BlockQueue");
                    instance = obj.AddComponent(typeof(BlockQueue)) as BlockQueue;
                    Debug.Log("Could not locate an BlockQueue object.  BlockQueue was Generated Automaticly.");
                }
                return instance;
            }
        }

        private void Awake() {
            controller = PlayerController.Instance;
            controller.blockQueue = this;
            controller.NextBlock();
        }


        public GameObject GetNextBlock() {
            GameObject returnBlock = blockQueue[0];
            blockQueue.RemoveAt(0);
            blockQueue.Add(GenerateNewBlok());
            return returnBlock;
        }


        private GameObject GenerateNewBlok() {
            int index = Random.Range(0, blockShapes.Length);
            GameObject block = Instantiate(blockShapes[index], this.transform);
            SetToPositionInList(block);
            if (queueNumber < 2) {
                queueNumber++;
            }
            return block;
        }


        private void SetToPositionInList(GameObject block) {
            block.transform.position = queuePosition[queueNumber].transform.position;
        }
    }
}
