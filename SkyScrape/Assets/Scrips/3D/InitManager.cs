using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {
    public class InitManager : MonoBehaviour {

        Tower tower;
        BlockQueue blockQueue;
        PlayerController controler;

        public GameObject[] models;

        void Awake() {
            Init();
            SetValues();
        }

        private void Init() {
            controler = PlayerController.Instance;
            tower = Tower.Instance;
            blockQueue = BlockQueue.Instance;
        }

        private void SetValues() {
            blockQueue.init = this;
        }
    }
}
