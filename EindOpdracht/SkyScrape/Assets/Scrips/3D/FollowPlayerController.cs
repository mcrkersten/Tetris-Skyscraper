using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Version3D {
    public class FollowPlayerController : MonoBehaviour {
        private GameObject player;
        public float minAmount;
        private Camera cam;
        private float rotation = 0;
        private Transform rotationX;
        private bool canMoveObject = false;

        // Use this for initialization
        private void Start() {
            cam = InitManager.Instance.cam;
            rotationX = InitManager.Instance.followCamTransform;
            PlayerController.OnRotateCamera += RotateAll;
            player = PlayerController.Instance.gameObject;
        }


        // Update is called once per frame
        private void Update() {
            this.transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player.transform.position.y, 0.1f) + minAmount, this.transform.position.z);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotationX.rotation, .1f);
        }


        private void RotateAll(float rotationY) {
            if(canMoveObject == true) {
                rotationX.eulerAngles = new Vector3(rotationX.eulerAngles.x, rotationX.eulerAngles.y + rotationY, rotationX.eulerAngles.z);
            }           
        }
    }
}
