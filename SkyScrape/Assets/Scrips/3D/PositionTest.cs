using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTest : MonoBehaviour {

    public GameObject parent;
    public Collider test;

    public delegate void OnTrigger(bool trigger);
    public static event OnTrigger OnTriggerDetect;

    public void OnTriggerEnter(Collider collision) {
        if (!collision.transform.IsChildOf(parent.transform) && collision.transform.tag != "DeadTrigger") {
            test = collision;
            if (OnTriggerDetect != null) {
                OnTriggerDetect(false);
            }
        }
    }

    public void OnTriggerExit(Collider collision) {
        if (OnTriggerDetect != null && collision.transform.tag != "DeadTrigger") {
            OnTriggerDetect(true);
        }
    }
}
