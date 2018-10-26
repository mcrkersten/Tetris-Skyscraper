using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace version2D {
    public class BlockQueue : MonoBehaviour {

        //private GameObject[] BlockType;
        public List<GameObject> blockQueue = new List<GameObject>();
        public GameObject[] blockShapes;
        public int queueLength;
        public PlayerController playerController;

        private List<GameObject> queuePosition = new List<GameObject>();
        private int queueNumber;


        private void Start() {
            //Put all QueuePositions in List
            foreach (Transform child in transform) {
                queuePosition.Add(child.gameObject);
            }

            //Instantiate new Blocks in queueList
            for (int i = 0; i < queueLength; ++i) {
                blockQueue.Add(GenerateNewBlok());
            }
            playerController.NextBlock();
        }


        private void FixedUpdate() {
            int i = 0;
            foreach (GameObject block in blockQueue) {
                block.transform.position = queuePosition[i].transform.position;
                i++;
            }
        }


        //Return block on top of list and remove it than generate new block.
        public GameObject GetNextBlock() {
            print(blockQueue.Count);
            GameObject returnBlock = blockQueue[0];
            blockQueue.RemoveAt(0);
            blockQueue.Add(GenerateNewBlok());
            return returnBlock;
        }


        //Make random int and instantiate new block and return it.
        private GameObject GenerateNewBlok() {
            int index = Random.Range(0, blockShapes.Length);
            GameObject block = Instantiate(blockShapes[index], this.transform);
            SetToPositionInList(block);
            if (queueNumber < 2) {
                queueNumber++;
            }
            return block;
        }


        //Put new blocks in the QueuePositions
        private void SetToPositionInList(GameObject block) {
            block.transform.position = queuePosition[queueNumber].transform.position;
        }
    }
}
