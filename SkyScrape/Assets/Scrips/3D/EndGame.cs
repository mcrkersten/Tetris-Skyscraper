using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Version3D {
    public class EndGame : MonoBehaviour {
        public GameObject killScreen;

        private void OnTriggerStay(Collider other) {
            if (other.tag == "Cube" && other.gameObject.transform.parent.GetComponent<TetrisBlock>() != null) {
                if (other.gameObject.transform.parent.GetComponent<TetrisBlock>().isUsed == true) {
                    killScreen.SetActive(true);
                    Time.timeScale = 0;                      
                }
            }
        }
    }
}
