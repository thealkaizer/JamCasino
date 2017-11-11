using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletDamageReceving_GD : MonoBehaviour {
    public SC_BabyController babyController;
    
    void OnParticleCollision(GameObject other) {
        if (other.CompareTag("Bullet")) {
            if(!this.babyController.isJumping) {
                this.babyController.KillPoorBaby();
            }
        }
    }
}

