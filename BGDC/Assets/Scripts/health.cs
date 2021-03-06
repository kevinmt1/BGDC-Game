using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{

    [SerializeField] float maxHealth = 100f;
    public float curHealth;
    public float healPerSecond;
    float healthAfterHeal = 0f;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Heal(30);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            takeDamage(30);
        }
        if (healthAfterHeal > 0)
        {
            if (curHealth < healthAfterHeal)
                curHealth += healPerSecond * Time.deltaTime;
        }
        if (curHealth >= 100)
        {
            curHealth = 100;
        }
    }

    public void Heal(int healNum)
    {
        healthAfterHeal = curHealth + healNum;
        if (healthAfterHeal > 100)
        {
            healthAfterHeal = 100;
        }
    }
    public void takeDamage(int damage)
    {
        if (healthAfterHeal > 0)
        {
            healthAfterHeal -= damage;
        }
        curHealth -= damage;
        if (curHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
       //Dead animation
       //Restart game
    }
}
