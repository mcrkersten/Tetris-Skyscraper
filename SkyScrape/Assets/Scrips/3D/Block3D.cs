using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block3D : MonoBehaviour {

    public GameObject[] model;
    public bool hit = false;
    protected List<GameObject> localModels = new List<GameObject>();
    protected Rigidbody rigid;
    protected int score = 4;
    private Vector3 position;


    private void Start() {
        hit = false;
        rigid = this.GetComponent<Rigidbody>();
        //Put every sprite of object in list
        foreach (Transform child in transform) {
            localModels.Add(child.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision) {
        hit = true;
    }


    public void SetPosition(Vector3 newPos) {
        position = newPos;
    }


    public void Release() {
        rigid.useGravity = true;
        rigid.constraints = RigidbodyConstraints.None;
    }


    public int GetScore() {
        return score;
    }


    //Lock Rigidbody and change sprite
    public virtual void EndGameLock() {
        float lenght = .70f;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (rigid.velocity.magnitude < 1f) {

            foreach (GameObject block in localModels) {
                rigid.constraints = RigidbodyConstraints.FreezeAll;
                block.GetComponent<Collider>().enabled = false;
                Vector3 neighbours = new Vector3(0, 0, 0);

                RaycastHit hitL;
                if (Physics.Raycast(block.transform.position, block.transform.TransformDirection(Vector3.left), out hitL, lenght, layerMask)) {
                    if (hitL.distance <= lenght) {
                        neighbours = new Vector3(-1, neighbours.y, neighbours.z);
                    }
                }
                RaycastHit hitR;
                if (Physics.Raycast(block.transform.position, block.transform.TransformDirection(Vector3.right), out hitR, lenght, layerMask)) {
                    if (hitR.distance <= lenght) {
                        //Check if object is at left hand if none set to 1 else -2
                        if(neighbours == new Vector3(-1, neighbours.y, neighbours.z)) {
                            neighbours = new Vector3(2, neighbours.y, neighbours.z);
                        }
                        else {
                            neighbours = new Vector3(1, neighbours.y, neighbours.z);
                        }           
                    }
                }
                RaycastHit hitForward;
                if (Physics.Raycast(block.transform.position, block.transform.TransformDirection(Vector3.forward), out hitForward, lenght, layerMask)) {
                    if (hitForward.distance <= lenght) {
                        neighbours = new Vector3(neighbours.x, neighbours.y, 1);
                    }
                }
                RaycastHit hitBack;
                if (Physics.Raycast(block.transform.position, block.transform.TransformDirection(Vector3.back), out hitBack, lenght, layerMask)) {
                    if (hitForward.distance <= lenght) {
                        if(neighbours == new Vector3(neighbours.x, neighbours.y, 1)) {
                            neighbours = new Vector3(neighbours.x, neighbours.y, 2);
                        }
                        else {
                            neighbours = new Vector3(neighbours.x, neighbours.y, -1);
                        }                    
                    }
                }

                //No Ground floor
                block.GetComponent<Collider>().enabled = true;
                print(neighbours);
            }
            //Sets te blocks on lines
            this.transform.position = position;
            //this.transform.eulerAngles = new Vector3(0, 0, Mathf.Round(this.transform.eulerAngles.z));
        }
    }
}
