using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Bullet;
    public bool Shoot;

   Transform StartTransform;
    private bool isShooted;

    // Start is called before the first frame update
    void Start()
    {
        StartTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Shoot) Shooting(StartTransform);
        isShooted = isShooted && Shoot; //reload if Shoot = false
    }
    private void Shooting(Transform StartTransform)
    {
        if (!isShooted)
        {
            Bullet.transform.localScale = StartTransform.lossyScale;
            Instantiate(Bullet, StartTransform.position, new Quaternion());
            isShooted = true;
        }
    }
    public void setStartTransform(Transform StartTransform)
    {
        this.StartTransform = StartTransform;
    }
    
}
