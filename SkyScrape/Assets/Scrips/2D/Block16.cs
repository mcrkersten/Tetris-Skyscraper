using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block16 : Block {


    //Lock Rigidbody and change sprite
    public override void EndGameLock() {
        float lenght = .75f;
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
                    if (hitGround.transform.transform.tag == "Ground") {
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
                if (neighbours == new Vector3(1, 0, 0)) { block.GetComponent<SpriteRenderer>().sprite = sprites[3]; } //LEFT
                else if (neighbours == new Vector3(1, 0, 1)) { block.GetComponent<SpriteRenderer>().sprite = sprites[1]; } //BOTH
                else if (neighbours == new Vector3(0, 0, 1)) { block.GetComponent<SpriteRenderer>().sprite = sprites[0]; } //RIGHT

                //Ground floor
                if (neighbours == new Vector3(1, 1, 0)) { block.GetComponent<SpriteRenderer>().sprite = sprites[6]; } //LEFT & BOTTOM
                else if (neighbours == new Vector3(1, 1, 1)) { block.GetComponent<SpriteRenderer>().sprite = sprites[5]; } // ALL
                else if (neighbours == new Vector3(0, 1, 1)) { block.GetComponent<SpriteRenderer>().sprite = sprites[4]; } //RIGHT & BOTTOM

                block.GetComponent<Collider2D>().enabled = true;
                print(neighbours);
            }
        //Sets te blocks on lines
        this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
        this.transform.eulerAngles = new Vector3(0, 0, Mathf.Round(this.transform.eulerAngles.z));
        }
    }
}   