using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy {
    public class Tower3D : MonoBehaviour {

        public List<GameObject> playedBlocks = new List<GameObject>();
        public PlayerController3D player;

        private int lives = 1;


        public int GetScore() {
            int score = 0;
            foreach (GameObject block in playedBlocks) {
                score += block.GetComponent<Block3D>().GetScore();
            }
            return score;
        }


        public void EndPlayerGame() {
            foreach (GameObject block in playedBlocks) {
                block.GetComponent<Block3D>().EndGameLock();
            }
        }


        private void OnTriggerEnter(Collider collision) {
            lives = lives - 1;
            if (lives < 0) {
                EndPlayerGame();
                this.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
