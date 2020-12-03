using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportMenu : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnMouseOver()
    {
        anim.SetBool("Attack", true);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
