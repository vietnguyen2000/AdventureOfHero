using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public float FollowSpeed = 2f;
    public Transform Target;
    public float distance = 4f;
    public Vector3 positionMax;
    public Vector3 positionMin;

    private void Start()
    {
        distance = 4f;
        FollowSpeed = 2f;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (Target != null)
        {

            Vector3 newPosition = Target.position;
            if (Target.transform.localScale.x == -1f)
            {
                newPosition.x -= distance;
            }
            else
            {
                newPosition.x += distance;
            }
            newPosition.y += 2f;

            newPosition.z = -10;
            transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
            if (transform.position.x > positionMax.x) transform.position = new Vector3(positionMax.x, transform.position.y, transform.position.z);
            if (transform.position.y > positionMax.y) transform.position = new Vector3(transform.position.x, positionMax.y, transform.position.z);
            if (transform.position.x < positionMin.x) transform.position = new Vector3(positionMin.x, transform.position.y, transform.position.z);
            if (transform.position.y < positionMin.y) transform.position = new Vector3(transform.position.x, positionMin.y, transform.position.z);
        }
    }
}