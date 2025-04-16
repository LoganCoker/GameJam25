using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour {

    public HealthSystem god;
    public SceneTransition move;

    void Start() {
        
    }

    void Update() {
        if (god.GetHealth() <= 0) {
            move.Transition("WinScreen");
        }
    }
}
