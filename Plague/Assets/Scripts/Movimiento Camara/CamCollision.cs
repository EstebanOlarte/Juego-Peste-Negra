using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCollision : MonoBehaviour {

    public float minDistance = 1;
    public float maxDistance = 4;
    public float smooth=10;
    Vector3 dollyDir;
    public float distance;
    public LayerMask layer; 

    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;

    }

    // Update is called once per frame
    void Update () {


        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);

        RaycastHit hit;



        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit, layer))
        {
            distance = Mathf.Clamp((hit.distance*0.9f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
	}
}
