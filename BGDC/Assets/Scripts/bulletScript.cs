using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{

    public float timeToChangePos = .5f;
    float nextChangeTime = 0f;
    Vector3 backPos;
    new Renderer renderer;
    new Collider collider;

    public GameObject bulletImpact;


    private void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 direction = backPos - transform.position;
        float distance = Vector3.Distance(transform.position, backPos);
        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            Debug.Log("You hit" + hit.transform.name);
            renderer.enabled = false;
            collider.enabled = false;
            Destroy(gameObject, 1f);
            GameObject effect = Instantiate(bulletImpact, hit.point, Quaternion.identity);
            Destroy(effect, .5f);
        }
        if (Time.time >= nextChangeTime)
        {
            backPos = transform.position;
            nextChangeTime = Time.time + timeToChangePos;
        }
        Debug.DrawLine(transform.position, backPos, Color.red);
        
    }
}
