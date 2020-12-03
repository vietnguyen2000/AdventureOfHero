using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : Interactive
{
    protected override void DoWork(object arg = null)
    {
        Object.Destroy(gameObject);
    }
}
