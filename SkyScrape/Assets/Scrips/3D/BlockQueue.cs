using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

                // If it is still null, create a new instance of this script
                if (instance == null) {
                    GameObject obj = new GameObject("BlockQueue");
                    instance = obj.AddComponent(typeof(BlockQueue)) as BlockQueue;
                    obj.AddComponent<FollowPlayerController>();
                    obj.GetComponent<FollowPlayerController>().minAmount = -1.5f;
                    Debug.Log("Could not locate an BlockQueue object.  BlockQueue was Generated Automaticly.");

                    //Create QueuePositions
                    float[] sPos = new float[3];
                    sPos[0] = 13.5f;
                    sPos[1] = 4.5f;
                    sPos[2] = -2.9f;

                    for (int i = 0; i < 3; i++) {                                           
                        GameObject block = new GameObject("QueuePos");
                        block.transform.position = new Vector3(10, sPos[i], -10);
                        block.transform.parent = obj.transform;
                    }
                }
                return instance;
            }
        }

        private void Start() {       
            controller = PlayerController.Instance;
            controller.blockQueue = this;

            if (transform.childCount > 2) {                                                     //Check if this transform has children                                     
                foreach (Transform child in transform) {                                        //Put all QueuePositions in List
                    queuePosition.Add(child.gameObject);
                }
                blockShapes = InitManager.Instance.tetrisModels;                                //Get Models from InitManager;
                for (int i = 0; i < queuePosition.Capacity -1; ++i) {                           //Instantiate new Blocks in queueList
                    blockQueue.Add(GenerateNewBlok());
                }
            }
            else {throw new Exception("ERROR: Could not locate one or more queue locations :: BlockQueue");}    //Throw error all is lost.
            controller.NextBlock();
        }


        private void Update() {
            int i = 0;
            foreach (GameObject block in blockQueue) {
                block.transform.position = queuePosition[i].transform.position;
                i++;
            }
        }


        public GameObject GetNextBlock() {
            GameObject returnBlock = blockQueue[0];
            blockQueue.RemoveAt(0);
            blockQueue.Add(GenerateNewBlok());
            return returnBlock;
        }


        private GameObject GenerateNewBlok() {
            int index = UnityEngine.Random.Range(0, blockShapes.Length);
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
