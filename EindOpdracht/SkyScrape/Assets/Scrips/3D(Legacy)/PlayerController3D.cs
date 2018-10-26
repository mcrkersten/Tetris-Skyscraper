using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy {
    public class PlayerController3D : MonoBehaviour {

        public Tower3D tower;
        private GameObject currentBlock;
        private BlockQueue3D blockQueue;
        private float rotateX;
        private float rotateY;
        public GameObject cam;


        private void Awake() {
            if (GameObject.FindGameObjectWithTag("BlockQueue") != null) {
                blockQueue = GameObject.FindGameObjectWithTag("BlockQueue").GetComponent<BlockQueue3D>();
            }
        }


        private void Update() {
            if (currentBlock.GetComponent<Block3D>().hit == true) { ReleaseBlock(); }
            PlayerMovement();
            MoveBlock();
        }


        //Get the next block from the BlockQueue
        public void NextBlock() {
            if (blockQueue != null) { currentBlock = blockQueue.GetNextBlock(); }
            currentBlock.transform.position = this.transform.position;
            currentBlock.transform.parent = this.gameObject.transform;
        }


        private void RotateBlockX(bool left) {
            if (left == true) {
                rotateX += 90;
            }
            else {
                rotateX -= 90;
            }
            currentBlock.gameObject.transform.eulerAngles = new Vector3(currentBlock.gameObject.transform.eulerAngles.x, currentBlock.gameObject.transform.eulerAngles.y, rotateX);
        }

        private void RotateBlockY(bool left) {
            if (left == true) {
                rotateY += 90;
            }
            else {
                rotateY -= 90;
            }
            currentBlock.gameObject.transform.eulerAngles = new Vector3(currentBlock.gameObject.transform.eulerAngles.x, rotateY, currentBlock.gameObject.transform.eulerAngles.z);
        }


        //Gets current position and moves it down every frame (hoeveelheid is gedeelt door framesnelheid)
        private void MoveBlock() {
            float move = 0.015f * Time.deltaTime;
            if (Input.GetKey("space")) { move = 0.05f * Time.deltaTime; }
            Vector3 localPosition = currentBlock.gameObject.transform.localPosition;
            currentBlock.gameObject.transform.localPosition = new Vector3(0, currentBlock.gameObject.transform.localPosition.y - (move / Time.deltaTime), 0);
        }


        //Activate ReleaseMEthode in Block and set current block to null
        private void ReleaseBlock() {
            tower.playedBlocks.Add(currentBlock);
            currentBlock.GetComponent<Block3D>().Release();
            currentBlock.transform.parent = null;
            currentBlock = null;
            NextBlock();
        }


        private void PlayerMovement() {
            float movement = 1.6f;
            //If key is pressed and the gameobject transform is within a range.
            if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.transform.position.x < 2.4f) {
                if (!currentBlock.GetComponent<Block3D>().CheckMovePosition(Vector3.right, false)) { return; }
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + movement, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.transform.position.x > -2.4f) {
                if (!currentBlock.GetComponent<Block3D>().CheckMovePosition(Vector3.left, false)) { return; }
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - movement, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && gameObject.transform.position.z < 2.4f) {
                if (!currentBlock.GetComponent<Block3D>().CheckMovePosition(Vector3.forward, false)) { return; }
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + movement);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && gameObject.transform.position.z > -2.4f) {
                if (!currentBlock.GetComponent<Block3D>().CheckMovePosition(Vector3.back, false)) { return; }
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - movement);
            }


            else if (Input.GetKeyDown(KeyCode.Z)) {
                RotateBlockX(true);
            }
            else if (Input.GetKeyDown(KeyCode.X)) {
                RotateBlockX(false);
            }

            else if (Input.GetKeyDown(KeyCode.Q)) {
                RotateBlockY(false);
            }
            else if (Input.GetKeyDown(KeyCode.E)) {
                RotateBlockY(false);
            }

            if ((Input.GetKeyDown(KeyCode.C))) {
                float temp = cam.transform.eulerAngles.y;
                temp += 90;
                cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, temp, cam.transform.eulerAngles.z);
            }
            else if (Input.GetKeyDown(KeyCode.V)) {
                float temp = cam.transform.eulerAngles.y;
                temp -= 90;
                cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, temp, cam.transform.eulerAngles.z);
            }
        }
    }
}

