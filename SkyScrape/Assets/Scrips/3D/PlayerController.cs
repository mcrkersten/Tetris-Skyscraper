using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {
    public class PlayerController : MonoBehaviour {

        private int score;
        private Vector3 move;
        private float moveAmount = 1.6f;
        private float moveDownSpeed = .1f;
        private float levelSize = 2.4f;
        private bool canMove = false;
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
                    obj.transform.position = new Vector3(0.8f, 15, 0.8f);
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
            PositionTest.OnTriggerDetect += OnTriggerDetect;                //Subscribe < Listen for trigger update from all SingleBlock objects
        }


        private void Update() {
            PlayerMovement();
            PlayerRotation();
        }

        private void FixedUpdate() {
            if(currentBlock.transform.localPosition.y < -1) {               //Activate collisions
                currentBlock.GetComponent<TetrisBlock>().ActivateCollisions();  
            }
            if (Input.GetKey(KeyCode.Space)) {
                currentBlock.transform.Translate(Vector3.down * Time.deltaTime * 10, Space.World);
            }
            else {
                currentBlock.transform.Translate(Vector3.down * Time.deltaTime, Space.World);
            } 
        }


        public void NextBlock() {
            TetrisBlock.OnColissionEvent += ReleaseBlock;                   //Subscribe < Listen to blockQueue
            currentBlock = blockQueue.GetNextBlock();                       //Get next block from blockQueue
            currentBlock.transform.position = this.transform.position;
            currentBlock.transform.parent = this.gameObject.transform;
        }


        void ReleaseBlock() {                                               //ReleaseBlock Event
            StopCoroutine(ExecuteMovementWait(move));
            TetrisBlock.OnColissionEvent -= ReleaseBlock;                   //Un-subscribe < Don't listen to blockQueue (Prevent MemoryLeak)
            tower.playedBlocks.Add(currentBlock);                           //Add currentBlock to the Tower Object            
            currentBlock.GetComponent<TetrisBlock>().Release();
            tower.CheckLayer();

            currentBlock.transform.parent = null;                           //Reset all currentblock variables
            currentBlock = null;
            NextBlock();                                                    //Get the next block
        }   


        private void OnTriggerDetect(bool x) {
            canMove = x;
            print(x);
        }
        

        void UpdateScore(int scoreToAdd) {                                  //Score event
            score += scoreToAdd;
        }


        void UpdateLifes() {

        }


        private void PlayerMovement() {
            if (Input.GetKeyDown(KeyCode.A) && currentBlock.gameObject.transform.position.x > -levelSize) {       //Move Left on A-press | !!! LEFT !!!
                currentBlock.GetComponent<TetrisBlock>().TestMovement(new Vector3(gameObject.transform.position.x - moveAmount, gameObject.transform.position.y, gameObject.transform.position.z));
                move = new Vector3(gameObject.transform.position.x - moveAmount, gameObject.transform.position.y, gameObject.transform.position.z);
                StartCoroutine(ExecuteMovementWait(move));             
            }
            if(Input.GetKeyDown(KeyCode.D) && currentBlock.gameObject.transform.position.x < levelSize) {         //Move right on D-press | !!! RIGHT !!!
                currentBlock.GetComponent<TetrisBlock>().TestMovement(new Vector3(gameObject.transform.position.x + moveAmount, gameObject.transform.position.y, gameObject.transform.position.z));
                move = new Vector3(gameObject.transform.position.x + moveAmount, gameObject.transform.position.y, gameObject.transform.position.z);
                StartCoroutine(ExecuteMovementWait(move));
            }
            if (Input.GetKeyDown(KeyCode.W) && currentBlock.gameObject.transform.position.z < levelSize) {       //Move right on A-press | !!! UP !!!
                currentBlock.GetComponent<TetrisBlock>().TestMovement(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + moveAmount));
                move = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + moveAmount);
                StartCoroutine(ExecuteMovementWait(move));
            }
            if (Input.GetKeyDown(KeyCode.S) && currentBlock.gameObject.transform.position.z > -levelSize) {        //Move right on D-press | !!! DOWN !!!
                currentBlock.GetComponent<TetrisBlock>().TestMovement(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - moveAmount));
                move = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - moveAmount);
                StartCoroutine(ExecuteMovementWait(move));
            }
        }

        private void PlayerRotation() {
            if (Input.GetKeyDown(KeyCode.U)) {
                currentBlock.transform.Rotate(Vector3.up, -90f, Space.World);           //Rotate Left on Y-axis
            }
            if (Input.GetKeyDown(KeyCode.O)) {
                currentBlock.transform.Rotate(Vector3.up, 90f, Space.World);            //Rotate Right on Y-axis
            }
            if (Input.GetKeyDown(KeyCode.J)) {
                currentBlock.transform.Rotate(Vector3.forward, -90f, Space.World);      //Rotate Left on Z-Axis
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                currentBlock.transform.Rotate(Vector3.forward, 90f, Space.World);       //Rotate Left on Z-Axis
            }
            if (Input.GetKeyDown(KeyCode.I)) {
                currentBlock.transform.Rotate(Vector3.left, -90f, Space.World);         //Rotate Left on X-Axis
            }
            if (Input.GetKeyDown(KeyCode.K)) {
                currentBlock.transform.Rotate(Vector3.left, 90f, Space.World);          //Rotate Right on X-Axis
            }
        }

        private void ExecuteMovement(Vector3 move) {
            if (canMove) {
                gameObject.transform.position = move;
            }     
        }

        IEnumerator ExecuteMovementWait(Vector3 move) {
            yield return new WaitForSeconds(.05f);
            ExecuteMovement(move);
        }


    }
}
