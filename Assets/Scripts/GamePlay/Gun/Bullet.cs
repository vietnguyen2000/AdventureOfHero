using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed;
    public float MaxLength;
    public bool isThrough;
    public GameObject EffectDestroy;

    protected float m_BulletSpeed;
    private PowerValue power;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        m_BulletSpeed = BulletSpeed;
        rb = GetComponent<Rigidbody2D>();
        power = GetComponentInParent<PowerValue>();
        Object.Destroy(gameObject, (MaxLength/m_BulletSpeed));
        Object.Destroy(transform.parent.gameObject, MaxLength);

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(m_BulletSpeed * transform.lossyScale.x, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isThrough)
        {
            if (EffectDestroy != null)MakeEffect(EffectDestroy);
            Object.Destroy(gameObject, 0.02f);
           
        }
    }
    private void MakeEffect(GameObject Effect)
    {
        GameObject effect = Instantiate(Effect, transform.position,new Quaternion());
        effect.transform.localScale = transform.localScale * Mathf.Max( 0.3f, power.damage);
        Object.Destroy(effect, 2f);
    }
}
