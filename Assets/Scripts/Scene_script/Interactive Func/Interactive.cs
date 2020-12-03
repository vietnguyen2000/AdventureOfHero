using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactive : MonoBehaviour
{
    protected GameObject MainCam;
    protected GameObject keyUpSprite;
    public bool canDo;
    protected virtual void Start()
    {
        keyUpSprite = transform.GetChild(0).gameObject;
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");
        keyUpSprite.SetActive(false);
    }

    abstract protected void DoWork(object arg = null);
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(canDo) keyUpSprite.SetActive(true);
            if (Input.GetKeyDown(OptionsScript.keys["Up"]))
            {
                if (canDo)
                {
                    DoWork(collision);
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            keyUpSprite.transform.position = keyUpSprite.GetComponent<KeyUpSprite>().pos;
            keyUpSprite.SetActive(false);

        }
    }
}
