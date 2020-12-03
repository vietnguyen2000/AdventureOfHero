using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected bool isCollected;
    protected AudioClip Collect;
    public bool isBounce;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        if (isBounce) Bounce();

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isCollected)
            {
                isCollected = true;
                gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                gameObject.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                rb.gravityScale = 3f;
                rb.velocity = new Vector2(0, 15f);
                Invoke("Destroy", 1f);
                Active(collision.gameObject);
                if(Collect != null) SFXManager.PlaySoundEffectOneShot(Collect);
            }
        }
    }
    public void Bounce()
    {
        rb.gravityScale = 1f;
        rb.velocity = new Vector2(Random.Range(-4f, 4f), Random.Range(3f, 8f));
    }
    protected abstract void Active(GameObject Player);
    private void Destroy()
    { 
        Object.Destroy(gameObject);
    }
}
