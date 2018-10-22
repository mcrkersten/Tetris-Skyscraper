using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace extras {
    public class CarInstantiator : MonoBehaviour {
        public GameObject car;
        public float randomTimeMin;
        public float randomTimeMax;

        private float randomTimeGen;
        private float currentTime;

        private void Start() {
            randomTimeGen = Random.Range(randomTimeMin, randomTimeMax);
        }


        void Update() {
            currentTime -= Time.deltaTime;
            if(currentTime < 0) {
                Instantiate(car, this.transform);
                ResetTime();
            }
        }


        private void ResetTime() {
            randomTimeGen = Random.Range(randomTimeMin, randomTimeMax);
            currentTime = randomTimeGen;
        }

    }
}
