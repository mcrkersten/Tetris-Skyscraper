using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {
    public class PlayerController : MonoBehaviour {

        private int score;

        private float moveAmount = 1.6f;
        private float levelSize = 2.4f;

        public Tower tower;
        public BlockQueue blockQueue;
        private GameObject currentBlock;
        private static PlayerController instance = null;
        public static PlayerController Instance {
            get {
                if (instance == null) {
                    // This is where the magic happens.
                    //  FindObjectOfType(...) returns the first PlayerController object in the scene.
                    instance = FindObjectOfType(typeof(PlayerController)) as PlayerController;
                }

                // If it is still null, create a new instance
                if (instance == null) {
                    GameObject obj = new GameObject("PlayerController");
                    obj.transform.position = new Vector3(0, 15, 0);
                    instance = obj.AddComponent(typeof(PlayerController)) as PlayerController;
                    Debug.Log("Could not locate an PlayerController object.  PlayerController was Generated Automaticly.");
                }
                return instance;
            }
        }

        void Awake() {
            blockQueue = BlockQueue.Instance;                               //Look for BlockQueue
            blockQueue.controller = this;
            tower = Tower.Instance;                                         //Look for Tower
            Tower.SendScore += UpdateScore;                                 //Subscribe < Listen for new Score from Tower
            Tower.OnBlockFall += UpdateLifes;                               //Subscribe < Listen for Life-update from Tower
        }


        public void NextBlock() {
            TetrisBlock.OnColissionEvent += ReleaseBlock;                   //Subscribe < Listen to blockQueue
            currentBlock = blockQueue.GetNextBlock();                       //Get next block from blockQueue
            currentBlock.transform.position = this.transform.position;
            currentBlock.transform.parent = this.gameObject.transform;
        }


        void ReleaseBlock() {                                               //ReleaseBlock Event
            TetrisBlock.OnColissionEvent -= ReleaseBlock;                   //Un-subscribe < Don't listen to blockQueue (Prevent MemoryLeak)
            tower.playedBlocks.Add(currentBlock);                           //Add currentBlock to the Tower Object
            currentBlock.GetComponent<TetrisBlock>().Release();
            currentBlock.transform.parent = null;                           //Reset all currentblock variables
            currentBlock = null;
            NextBlock();                                                    //Get the next block
        }   
        

        void UpdateScore(int scoreToAdd) {                                  //Score event
            score += scoreToAdd;
        }


        void UpdateLifes() {

        }


        private void PlayerMovement() {
            if (Input.GetKeyDown(KeyCode.A) && gameObject.transform.position.x > -levelSize) {       //Move Left on A-press | LEFT
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveAmount, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            if(Input.GetKeyDown(KeyCode.D) && gameObject.transform.position.x < levelSize) {         //Move right on D-press | RIGHT
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveAmount, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
    }
}
