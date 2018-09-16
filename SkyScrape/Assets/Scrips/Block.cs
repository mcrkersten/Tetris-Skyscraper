using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public Sprite[] sprites;
    public bool hit = false;
    protected List<GameObject> localSprites = new List<GameObject>();
    protected Rigidbody2D rigid;
    protected int score = 4;
    protected float xPos;

    private void Start() {        
        rigid = this.GetComponent<Rigidbody2D>();
        //Put every sprite of object in list
        foreach (Transform child in transform) {
            localSprites.Add(child.gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        hit = true;
    }


    private void Update() {
        DebugLines();
    }


    //Gets called in PlayerController
    public void BlockSpriteRotate(float rotate) {
        foreach(GameObject sprite in localSprites) {
            sprite.transform.localEulerAngles = new Vector3(0, 0, rotate);
        }
    }


    public void SetXpos(float newXpos) {
        xPos = newXpos;
    }


    public void Release() {
        rigid.gravityScale = 1;
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
            
            foreach (GameObject block in localSprites) {
                rigid.bodyType = RigidbodyType2D.Static;
                block.GetComponent<Collider2D>().enabled = false;
                Vector3 neighbours = new Vector3(0, 0, 0);
                RaycastHit2D hitL = Physics2D.Raycast(block.transform.position, block.transform.TransformDirection(Vector2.left), lenght, layerMask);
                if (hitL.collider != null) {
                    if (hitL.distance <= lenght) {
                        neighbours = new Vector3(1, neighbours.y, neighbours.z);
                    }
                }
                RaycastHit2D hitGround = Physics2D.Raycast(block.transform.position, block.transform.TransformDirection(Vector2.down), lenght, layerMask);
                if (hitGround.collider != null) {
                    if (hitGround.transform.tag == "Ground") {
                        if (hitGround.distance <= lenght) {
                            neighbours = new Vector3(neighbours.x, 1, neighbours.z);
                        }
                    }
                }
                RaycastHit2D hitR = Physics2D.Raycast(block.transform.position, block.transform.TransformDirection(Vector2.right), lenght, layerMask);
                if (hitR.collider != null) {
                    if (hitR.distance <= lenght) {
                        neighbours = new Vector3(neighbours.x, neighbours.y, 1);
                    }
                }

                //No Ground floor
                if (neighbours == new Vector3(1, 0, 0)) { block.GetComponent<SpriteRenderer>().sprite = sprites[2]; }
                else if (neighbours == new Vector3(1, 0, 1)) { block.GetComponent<SpriteRenderer>().sprite = sprites[1]; }
                else if (neighbours == new Vector3(0, 0, 1)) { block.GetComponent<SpriteRenderer>().sprite = sprites[0]; }
                //Ground floor
                if (neighbours == new Vector3(1, 1, 0)) { block.GetComponent<SpriteRenderer>().sprite = sprites[5]; }
                else if (neighbours == new Vector3(1, 1, 1)) { block.GetComponent<SpriteRenderer>().sprite = sprites[4]; }
                else if (neighbours == new Vector3(0, 1, 1)) { block.GetComponent<SpriteRenderer>().sprite = sprites[3]; }

                block.GetComponent<Collider2D>().enabled = true;
                print(neighbours);
            }
            //Sets te blocks on lines
            this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Round(this.transform.eulerAngles.z));
        }
    }


    private void DebugLines() {
        float lenght = .70f;
        Color debugColor = Color.red;
        foreach(GameObject block in localSprites)
        {
            Vector3 down = block.transform.TransformDirection(Vector3.down) * lenght;
            Debug.DrawRay(block.transform.position, down, debugColor);

            //Vector3 left = block.transform.TransformDirection(Vector3.left) * lenght;
            //Debug.DrawRay(block.transform.position, left, debugColor);

            //Vector3 right = block.transform.TransformDirection(Vector3.right) * lenght;
            //Debug.DrawRay(block.transform.position, right, debugColor);
        }
    }
}   