using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Version3D {
    public class FollowPlayerController : MonoBehaviour {
        GameObject player;
        public float minAmount;
        public Camera cam;
        private float divider = 1;
        private float rotation = 0;
        private Transform rotationX;
        public bool canMoveObject = false;

        // Use this for initialization
        void Start() {
            rotationX = InitManager.Instance.followCamTransform;
            PlayerController.OnRotateCamera += RotateAll;
            player = PlayerController.Instance.gameObject;

        }

        // Update is called once per frame
        void Update() {
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
