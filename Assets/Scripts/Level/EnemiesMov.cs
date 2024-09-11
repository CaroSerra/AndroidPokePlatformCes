using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMov : MonoBehaviour
{
    public Transform ObjectToFollow = null;
    public float Speed = 3;
    float h;
    // Start is called before the first frame update
    void Start()
    {
        ObjectToFollow = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjectToFollow ==null)
            return;
        
        transform.position = Vector2.MoveTowards(transform.position,
            ObjectToFollow.transform.position, Speed * Time.deltaTime);

        ApplyRotation();
    
    }
    private void ApplyRotation()
    {
    
        h = Input.GetAxisRaw("Horizontal");
        if (h < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (h > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
