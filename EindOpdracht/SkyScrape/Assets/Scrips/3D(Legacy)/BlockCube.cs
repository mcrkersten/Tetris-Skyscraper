using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy {

    public class BlockCube : MonoBehaviour {
        public Vector4 sides = new Vector4(0, 0, 0, 0);
        public Vector2 topBot = new Vector2(0, 0);
        public GameObject[] models;
        private GameObject instant;

        public void VieuwNeighbours() {
            float lenght = 1f;
            RaycastHit hit;
            //Fire a raycast and continue if it hit a collision.
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.left), out hit, lenght)) { sides.x = 1; }
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.right), out hit, lenght)) { sides.y = 1; }
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, lenght)) { sides.z = 1; }
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.back), out hit, lenght)) { sides.w = 1; }
        }

        public void ReturnModel() {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            VieuwNeighbours();
            if (CountVect() == 4) { }//Roof


            if (CountVect() == 3) {
                instant = Instantiate(models[3], this.transform.position, this.gameObject.transform.rotation, this.gameObject.transform);
                if (sides.x == 1 && sides.y == 1) {
                    if (sides.z == 1) { instant.gameObject.transform.eulerAngles = new Vector3(0, 90, 0); }
                    else { instant.gameObject.transform.eulerAngles = new Vector3(0, -90, 0); }
                }
                else if (sides.z == 1 && sides.w == 1) {
                    if (sides.y == 1) { instant.gameObject.transform.eulerAngles = new Vector3(0, 180, 0); }
                    else { instant.gameObject.transform.eulerAngles = new Vector3(0, 0, 0); }
                }//Inham
                instant.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            }

            if (CountVect() == 2) {
                if (sides == new Vector4(1, 1, 0, 0)) {
                    instant = Instantiate(models[4], this.transform.position, this.gameObject.transform.rotation, this.gameObject.transform);
                    instant.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (sides == new Vector4(0, 0, 1, 1)) {
                    instant = Instantiate(models[4], this.transform.position, this.gameObject.transform.rotation, this.gameObject.transform);
                    instant.gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
                }

                else if (sides.z == 1) {
                    instant = Instantiate(models[2], this.transform.position, this.gameObject.transform.rotation, this.gameObject.transform);
                    if (sides.x == 1) {
                        instant.transform.eulerAngles = new Vector3(0, 90, 0); //Left = true
                    }
                    else {
                        instant.gameObject.transform.eulerAngles = new Vector3(0, 180, 0); //Right = true
                    }
                }
                else if (sides.w == 1) {
                    instant = Instantiate(models[2], this.transform.position, this.gameObject.transform.rotation, this.gameObject.transform);
                    if (sides.x == 1) {
                        instant.transform.eulerAngles = new Vector3(0, 0, 0); //Left = true
                    }
                    else {
                        instant.transform.eulerAngles = new Vector3(0, -90, 0); //Right = true
                    }
                }
                instant.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            }

            if (CountVect() == 1) { Instantiate(models[1], this.transform.position, this.gameObject.transform.rotation, this.gameObject.transform); }
            if (CountVect() == 0) { Instantiate(models[0], this.transform.position, this.gameObject.transform.rotation, this.gameObject.transform); }//Tower
            this.gameObject.GetComponent<MeshFilter>().mesh = null;
        }


        private float CountVect() {
            float sum = sides.w + sides.x + sides.y + sides.z;
            return sum;
        }
    }
}

