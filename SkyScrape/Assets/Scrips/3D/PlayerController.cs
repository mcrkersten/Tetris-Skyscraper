using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace Version3D {
    public class PlayerController : MonoBehaviour {

        private int score;
        private Vector3 move;
        private float moveAmount = 1.6f;
        private float moveDownSpeed = .1f;
        private float levelSize = 2.4f;
        private bool canMove = false;
        private GameObject currentBlock;
        private TetrisBlock curTetrisBlock;
        private Text scoreText;
        private Tower tower;

        public delegate void RotateCamera(float rotation);
        public static event RotateCamera OnRotateCamera;

        public delegate void CheckLayer();
        public static event CheckLayer OnCheckLayer;

        public BlockQueue blockQueue;
        
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


        private void Awake() {
            scoreText = InitManager.Instance.scoreText;                     //Get scoreObject from InitManager
            blockQueue = BlockQueue.Instance;                               //Look for BlockQueue
            tower = Tower.Instance;                                         //Look for Tower
            SnapPointSystem.SendScore += UpdateScore;                       //Subscribe < Listen for new Score from SnapPointSystem
            PositionTest.OnTriggerDetect += OnTriggerDetect;                //Subscribe < Listen for trigger update from all SingleBlock objects
            SnapPointSystem.OnFloorbuild += OnFloorBuild;                   //Subscribe < Listen for floorbuild evenbt from SnapPointSystem
        }


        private void FixedUpdate() {
            PlayerMovement();
            PlayerRotation();
            CameraRotation();
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
            curTetrisBlock = currentBlock.GetComponent<TetrisBlock>();
            currentBlock.transform.position = this.transform.position;
            currentBlock.transform.parent = this.gameObject.transform;
        }


        private void ReleaseBlock() {                                       //ReleaseBlock Event
            StopCoroutine(ExecuteMovementWait(move));                       //Exit Movement wait coroutine
            TetrisBlock.OnColissionEvent -= ReleaseBlock;                   //Un-subscribe < Don't listen to blockQueue (Prevent MemoryLeak)     
            curTetrisBlock.parent = this.gameObject;
            curTetrisBlock.Release();
            if(OnCheckLayer != null) {
                OnCheckLayer();
            }
            currentBlock.transform.parent = null;                           //Reset all currentblock variables
            currentBlock = null;
            NextBlock();                                                    //Get the next block
        }   


        private void OnTriggerDetect(bool x) {
            canMove = x;
        }
        

        private void UpdateScore(int scoreToAdd) {                                  //Score event
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }


        private void PlayerMovement() {
            if (Input.GetKeyDown(KeyCode.A) && currentBlock.gameObject.transform.position.x > -levelSize) {       //Move Left on A-press | !!! LEFT !!!
                curTetrisBlock.TestMovement(new Vector3(gameObject.transform.position.x - moveAmount, gameObject.transform.position.y, gameObject.transform.position.z));
                move = new Vector3(gameObject.transform.position.x - moveAmount, gameObject.transform.position.y, gameObject.transform.position.z);
                StartCoroutine(ExecuteMovementWait(move));
            }
            if (Input.GetKeyDown(KeyCode.D) && currentBlock.gameObject.transform.position.x < levelSize) {         //Move right on D-press | !!! RIGHT !!!
                curTetrisBlock.TestMovement(new Vector3(gameObject.transform.position.x + moveAmount, gameObject.transform.position.y, gameObject.transform.position.z));
                move = new Vector3(gameObject.transform.position.x + moveAmount, gameObject.transform.position.y, gameObject.transform.position.z);
                StartCoroutine(ExecuteMovementWait(move));
            }
            if (Input.GetKeyDown(KeyCode.W) && currentBlock.gameObject.transform.position.z < levelSize) {       //Move right on A-press | !!! UP !!!
                curTetrisBlock.TestMovement(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + moveAmount));
                move = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + moveAmount);
                StartCoroutine(ExecuteMovementWait(move));
            }
            if (Input.GetKeyDown(KeyCode.S) && currentBlock.gameObject.transform.position.z > -levelSize) {        //Move right on D-press | !!! DOWN !!!
                curTetrisBlock.TestMovement(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - moveAmount));
                move = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - moveAmount);
                StartCoroutine(ExecuteMovementWait(move));
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.T)) {
                if (Time.timeScale == 0) {
                    Time.timeScale = 1;
                }
                else {
                    Time.timeScale = 0;
                }

            }
        }


        private void PlayerRotation() {
            if (Input.GetKeyDown(KeyCode.U)) {
                curTetrisBlock.TestRotation(Vector3.up, -90f);                          //Test Rotate Left on Y-axis
                StartCoroutine(ExecuteRotationWait(Vector3.up, -90));                   //Execute Rotation
            }
            if (Input.GetKeyDown(KeyCode.O)) {
                curTetrisBlock.TestRotation(Vector3.up, 90f);                           //Test Rotate Right on Y-axis
                StartCoroutine(ExecuteRotationWait(Vector3.up, 90));                    //Execute Rotation
            }
            if (Input.GetKeyDown(KeyCode.J)) {
                curTetrisBlock.TestRotation(Vector3.forward, -90f);                     //Test Rotate Left on Z-Axis
                StartCoroutine(ExecuteRotationWait(Vector3.forward, -90));              //Execute Rotation
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                curTetrisBlock.TestRotation(Vector3.forward, 90f);                      //Test Rotate Left on Z-Axis
                StartCoroutine(ExecuteRotationWait(Vector3.forward, 90));               //Execute Rotation
            }
            if (Input.GetKeyDown(KeyCode.I)) {
                curTetrisBlock.TestRotation(Vector3.left, -90f);                        //Test Rotate Left on X-Axis
                StartCoroutine(ExecuteRotationWait(Vector3.left, -90));                 //Execute Rotation
            }
            if (Input.GetKeyDown(KeyCode.K)) {
                curTetrisBlock.TestRotation(Vector3.left, 90f);                         //Test Rotate Right on X-Axis
                StartCoroutine(ExecuteRotationWait(Vector3.left, 90));                  //Execute Rotation
            }
        }


        private void OnFloorBuild() {
            StartCoroutine(NextBlockWait());
        }


        private void CameraRotation() {
            if(OnRotateCamera != null) {
                if (Input.GetKeyDown(KeyCode.Z)) {
                    OnRotateCamera(90);
                }
                if (Input.GetKeyDown(KeyCode.X)) {
                    OnRotateCamera(-90);
                }
            }
        }


        private void ExecuteMovement(Vector3 move) {
            if (canMove) {
                gameObject.transform.position = move;
            }     
        }


        private void ExecuteRotation(Vector3 axis, float angle) {
            if (canMove) {
                currentBlock.transform.Rotate(axis, angle, Space.World);
            }
        }

        private IEnumerator NextBlockWait() {
            yield return new WaitForSeconds(.05f);
            this.gameObject.transform.Translate(Vector3.up * 1.6f, Space.World);
        }


        private IEnumerator ExecuteMovementWait(Vector3 move) {
            yield return new WaitForSeconds(.05f);
            ExecuteMovement(move);
            curTetrisBlock.ReturnPos();
        }


        private IEnumerator ExecuteRotationWait(Vector3 axis, float angle) {
            yield return new WaitForSeconds(.05f);
            ExecuteRotation(axis, angle);
            curTetrisBlock.ReturnPos();
        }
    }
}