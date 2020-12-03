using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUpSprite : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Sprite[] KeyBoard;
    int KeyUpIndex;
    public Vector3 pos;
    // Start is called before the first frame update
    void Awake()
    {
        pos = transform.position;
    }
    void OnEnable()
    {

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        KeyUpIndex = OptionsScript.keys["Up"] - KeyCode.A;
        KeyBoard = Resources.LoadAll<Sprite>("KeyBoard/Keyboard");
        spriteRenderer.sprite = KeyBoard[KeyUpIndex +72];
        transform.position = new Vector3(pos.x, pos.y + 0.25f, pos.z);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(0, (pos.y - transform.position.y)*25f));
    }
}
