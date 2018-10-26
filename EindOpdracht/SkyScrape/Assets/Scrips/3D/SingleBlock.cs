using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {

    public class SingleBlock : MonoBehaviour {
        public bool isSet = false;

        public void BuildBuilding(Transform position) {
            transform.parent.gameObject.GetComponent<TetrisBlock>().singleBlocks.Remove(this.gameObject);       //Remove pointer from parent TetrisBlock 
            this.gameObject.transform.position = position.position;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;                   //Turn own meshRenderer off
            GameObject temp = Instantiate(GetModel(position),this.transform);
            GameObject rememberParent = transform.parent.gameObject;                        //Remeber Parent for Reset functions
            Vector3 rememberParentPos = transform.parent.gameObject.transform.position;
            transform.parent = null;                                                        //Unparent this from Tertris Transform
            rememberParent.GetComponent<Rigidbody>().ResetCenterOfMass();                   //Reset Center of mass in parent TetrisBlock
            rememberParent.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);       //Reset Velocity
            rememberParent.transform.position = rememberParentPos;
            
            if(rememberParent.transform.childCount == 1) {
                Destroy(rememberParent);
            }
        }


        private GameObject GetModel(Transform pos) {
            if(pos.position.x >= 2.4 && pos.position.z >= 2.4) {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                return InitManager.Instance.buildingModels[0];             
            }
            else if (pos.position.x <= -2.4 && pos.position.z <= -2.4) {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                return InitManager.Instance.buildingModels[0];
            }
            else if (pos.position.x <= -2.4 && pos.position.z >= 2.4) {
                this.transform.eulerAngles = new Vector3(0, -90, 0);
                return InitManager.Instance.buildingModels[0];
            }
            else if (pos.position.x >= 2.4 && pos.position.z <= -2.4) {
                this.transform.eulerAngles = new Vector3(0, 90, 0);
                return InitManager.Instance.buildingModels[0];
            }
            else {
                if(pos.position.x <= -2.4) {
                    this.transform.eulerAngles = new Vector3(0, 180, 0);
                    return InitManager.Instance.buildingModels[1];
                }
                if(pos.position.z <= -2.4) {
                    this.transform.eulerAngles = new Vector3(0, 90, 0);
                    return InitManager.Instance.buildingModels[1];
                }
                if (pos.position.x >= 2.4) {
                    this.transform.eulerAngles = new Vector3(0, 0, 0);
                    return InitManager.Instance.buildingModels[1];
                }
                if (pos.position.z >= 2.4) {
                    this.transform.eulerAngles = new Vector3(0, -90, 0);
                    return InitManager.Instance.buildingModels[1];
                }
                else {
                    this.transform.eulerAngles = new Vector3(0, 0, 0);
                    return InitManager.Instance.buildingModels[2];
                }
            }
        }
    }
}

