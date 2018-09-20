using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour {

    public Tower3D tower;
    private GameObject currentBlock;
    private BlockQueue3D blockQueue;
    private float rotate;


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


    private void RotateBlock(bool left) {
        if (left == true) {
            rotate += 90;
        }
        else {
            rotate -= 90;
        }
        currentBlock.gameObject.transform.eulerAngles = new Vector3(0, 0, rotate);
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
        float movement = .80f;
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + movement, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - movement, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + movement);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - movement);
        }
        else if (Input.GetKeyDown(KeyCode.Z)) {
            RotateBlock(true);
        }
        else if (Input.GetKeyDown(KeyCode.X)) {
            RotateBlock(false);
        }
        //if (currentBlock != null) {
        //    currentBlock.GetComponent<Block>().SetXpos(this.gameObject.transform.position.x);
        //}
    }
}
