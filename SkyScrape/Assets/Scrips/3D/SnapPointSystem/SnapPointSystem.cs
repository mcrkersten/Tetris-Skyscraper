﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Version3D{
    public class SnapPointSystem : MonoBehaviour {
        public List<SnapPointSystemLayer> layer = new List<SnapPointSystemLayer>();

        public delegate void ScorePoints(int score);
        public static event ScorePoints SendScore;

        public delegate void TowerBuild();
        public static event TowerBuild OnFloorbuild;


        private int blocksForMovement = 16;

        private void Start() {
            PlayerController.OnCheckLayer += CheckLayer;

            foreach (Transform child in transform) {
                if(child.gameObject.GetComponent<SnapPointSystemLayer>() != null) {
                    layer.Add(child.gameObject.GetComponent<SnapPointSystemLayer>());
                }
            }
        }

        public void CheckLayer() {
            foreach(SnapPointSystemLayer layerX in layer) {
                if(layerX.objectsInTrigger.Count >= 16) {
                    foreach (Collider singleBlock in layerX.objectsInTrigger) {
                        foreach (Transform point in layerX.transform) {
                            if (Vector3.Distance(point.position, singleBlock.transform.position) < .5f) {
                                singleBlock.GetComponent<SingleBlock>().BuildBuilding(point);
                            }
                        }
                    }                  
                    if(layerX.hasBuild == false) {
                        OnFloorbuild();
                        layerX.hasBuild = true;
                        if (SendScore != null) {
                            SendScore(16);
                        }
                    }                    
                }               
            }
        }
    }
}
