using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Version3D {
    public class InitManager : MonoBehaviour {

        Tower tower;
        BlockQueue blockQueue;
        PlayerController controler;

        public GameObject[] tetrisModels;
        public GameObject[] buildingModels;
        public GameObject snapSystem;

        private static InitManager instance = null;
        public static InitManager Instance
        {
            get {
                if (instance == null) {
                    instance = FindObjectOfType(typeof(InitManager)) as InitManager;
                }
                if (instance == null) {
                    //HAS TO EXSIST
                    throw new System.ArgumentException("FATAL ERROR: Init manager has to exsist");
                }
                return instance;
            }
        }

        void Awake() {
            Init();
        }

        private void Init() {
            controler = PlayerController.Instance;
            tower = Tower.Instance;
            blockQueue = BlockQueue.Instance;
        }
    }
}
