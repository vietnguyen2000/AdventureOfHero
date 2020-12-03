using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : MonoBehaviour
{
    public GameObject SmallerSlug;
    public bool Spawn;
    private bool Spawned;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        if (Spawn) SpawnSlug();
        Spawned = Spawned && Spawn;
        
    }
    private void SpawnSlug()
    {
        if (!Spawned && SmallerSlug != null)
        {
            Spawned = true;
            SmallerSlug.transform.localScale =  new Vector3(SmallerSlug.transform.localScale.x * -1, 1, 1);
            Instantiate(SmallerSlug, new Vector3(transform.position.x + 2f*transform.localScale.x, transform.position.y + transform.position.z), new Quaternion());
        }
    }
}
