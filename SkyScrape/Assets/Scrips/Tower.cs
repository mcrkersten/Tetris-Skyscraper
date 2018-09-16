using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public List<GameObject> playedBlocks = new List<GameObject>();
    public PlayerController player;

    private int lives = 5;
	// Use this for initialization

	public int GetScore() {
        int score = 0;
        foreach(GameObject block in playedBlocks) {
            score += block.GetComponent<Block>().GetScore();
        }
        return score;
    }


    public void EndPlayerGame() {
        foreach(GameObject block in playedBlocks) {
            block.GetComponent<Block>().EndGameLock();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        lives = lives -1;
        if(lives < 0) { EndPlayerGame(); }        
    }
}
