using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public float maxHealth;
    private float currentHealth;
    public GameObject deathEffect;
    public float timer;

    HitEffect effect;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        effect = GetComponent<HitEffect>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        GetComponent<SpriteRenderer>().material = effect.white;
        StartCoroutine(BackToNormal());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(timer);
        GetComponent<SpriteRenderer>().material = effect.original;
    }
}
