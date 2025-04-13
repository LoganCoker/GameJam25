using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScenes : MonoBehaviour {
    public string SceneName;
    public SceneTransition Move;

    void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Player")) { 
           Move.Transition(SceneName);
        }
    }
}