using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotations : MonoBehaviour
{
    [SerializeField]
    Transform bone1;
    [SerializeField]
    Transform bone2;
    [SerializeField]
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir1 = target.position - bone1.position;
        bone1.rotation = Quaternion.LookRotation(dir1, dir1);

        Vector3 dir2 = bone1.position - bone2.position;
        bone2.rotation = Quaternion.LookRotation(dir2, dir2);
        
    }
}
