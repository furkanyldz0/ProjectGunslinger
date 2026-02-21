using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health;

    private void Start() {
        health = 100f;   
    }

    public void Damage(float damageAmount) {
        health -= damageAmount;
        Debug.Log(health + " " + this);
        if(health <= 0f) {
            Destroy(gameObject);
        }
    }

}
