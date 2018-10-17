using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {

    public class SingleBlock : MonoBehaviour {
        public bool isSet = false;

        public void BuildBuilding(Transform position) {
            if(true) {
                transform.parent.gameObject.GetComponent<TetrisBlock>().singleBlocks.Remove(this.gameObject);
                this.gameObject.transform.position = position.position;
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                GameObject temp = Instantiate(GetModel(position), this.transform);
                GameObject rememberParent = transform.parent.gameObject;
                transform.parent = null;
                rememberParent.GetComponent<Rigidbody>().ResetCenterOfMass();
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

