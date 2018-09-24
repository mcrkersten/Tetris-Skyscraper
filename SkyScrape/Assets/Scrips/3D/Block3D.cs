using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block3D : MonoBehaviour {

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
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        if (rigid.velocity.magnitude < 1f) {
            foreach (GameObject block in localModels) {
                this.gameObject.transform.position = new Vector3((float)System.Math.Round(this.gameObject.transform.position.x, 1),
                                                        (float)System.Math.Round(this.gameObject.transform.position.y, 1),
                                                        (float)System.Math.Round(this.gameObject.transform.position.z, 1));

                this.gameObject.transform.eulerAngles = new Vector3((float)System.Math.Round(this.gameObject.transform.eulerAngles.x, 1),
                                                        (float)System.Math.Round(this.gameObject.transform.eulerAngles.y, 1),
                                                        (float)System.Math.Round(this.gameObject.transform.eulerAngles.z, 1));

                block.GetComponent<BlockCube>().ReturnModel();
                print("Ik kom er langs");
            }
            //Sets te blocks on lines
            //this.transform.position = position;
        }
    }


    public bool CheckMovePosition(Vector3 side, bool detectSelf) {
        float lenght = .85f;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        //Fire a raycast and continue if it hit a collision.
        foreach (GameObject child in localModels) {
            if (Physics.Raycast(child.transform.position, transform.TransformDirection(side), out hit, lenght, layerMask)) {
                //if the collision that is hit by the raycast is not from a child inside the parent object, stop the procces and return a false.
                if (!hit.collider.transform.IsChildOf(this.gameObject.transform) && detectSelf == false) {
                    return false;
                }
            }
        }
        return true;
    }
}
