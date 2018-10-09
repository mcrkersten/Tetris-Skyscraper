using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {
    public class PlayerController : MonoBehaviour {

        private int score;

        private float moveAmount = 1.6f;
        private float levelSize = 2.4f;
        private Vector3 rot = new Vector3(0,0,0);
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


        private void Update() {
            PlayerMovement();
            PlayerRotation();
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
            if (Input.GetKeyDown(KeyCode.A) && gameObject.transform.position.x > -levelSize) {       //Move Left on A-press | !!! LEFT !!!
                currentBlock.gameObject.transform.position = new Vector3(currentBlock.gameObject.transform.position.x - moveAmount, currentBlock.gameObject.transform.position.y, currentBlock.gameObject.transform.position.z);
            }
            if(Input.GetKeyDown(KeyCode.D) && gameObject.transform.position.x < levelSize) {         //Move right on D-press | !!! RIGHT !!!
                currentBlock.gameObject.transform.position = new Vector3(currentBlock.gameObject.transform.position.x + moveAmount, currentBlock.gameObject.transform.position.y, currentBlock.gameObject.transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.W) && gameObject.transform.position.z < levelSize) {       //Move right on A-press | !!! UP !!!
                currentBlock.gameObject.transform.position = new Vector3(currentBlock.gameObject.transform.position.x, currentBlock.gameObject.transform.position.y, currentBlock.gameObject.transform.position.z + moveAmount);
            }
            if (Input.GetKeyDown(KeyCode.S) && gameObject.transform.position.z > -levelSize) {        //Move right on D-press | !!! DOWN !!!
                currentBlock.gameObject.transform.position = new Vector3(currentBlock.gameObject.transform.position.x, currentBlock. gameObject.transform.position.y, currentBlock.gameObject.transform.position.z - moveAmount);
            }
        }

        private void PlayerRotation() {
            if (Input.GetKeyDown(KeyCode.U)) {                
                rot = new Vector3(rot.x, rot.y + 90, rot.z); //Rotate Left on Y-axis
            }
            if (Input.GetKeyDown(KeyCode.O)) {                
                rot = new Vector3(rot.x, rot.y - 90, rot.z); //Rotate Right on Y-axis
            }

            if (Input.GetKeyDown(KeyCode.J)) {                
                rot = new Vector3(rot.x, rot.y, rot.z + 90); //Rotate Left on Z-Axis
            }
            if (Input.GetKeyDown(KeyCode.L)) {                
                rot = new Vector3(rot.x, rot.y, rot.z - 90); //Rotate Right on Z-Axis
            }
            if (Input.GetKeyDown(KeyCode.I)) {                
                rot = new Vector3(rot.x + 90, rot.y, rot.z); //Rotate Left on X-Axis
            }
            if (Input.GetKeyDown(KeyCode.K)) {                
                rot = new Vector3(rot.x - 90, rot.y, rot.z); //Rotate Right on X-Axis
            }
            currentBlock.gameObject.transform.eulerAngles = rot;
        }
    }
}
