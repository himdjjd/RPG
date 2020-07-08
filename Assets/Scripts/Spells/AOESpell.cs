using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AOESpell : MonoBehaviour
{
    protected List<Enemy> enemies = new List<Enemy>();

    protected float duration;

    protected float damage;

    protected float elapsed;

    protected float tickElapsed;

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= duration)
        {
            Destroy(gameObject);
        }

        Execute();
    }

    public abstract void Execute();

    public void Initialize(float damage, float duration)
    {
        this.damage = damage;
        this.duration = duration;
        enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Remove(collision.GetComponent<Enemy>());
        }
    }
}
