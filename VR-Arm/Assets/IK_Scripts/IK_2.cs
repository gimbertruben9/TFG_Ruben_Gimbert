using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_2 : MonoBehaviour
{

    [SerializeField]
    Transform elbow;
    [SerializeField]
    Transform elbowBone;

    [SerializeField]
    Transform shoulder;
    [SerializeField]
    Transform shoulderBone;

    [SerializeField]
    Transform hand;

    [SerializeField]
    Transform target;

    private Vector3 lastTargetPos;

    private float elbowBoneLength;
    private float shoulderBoneLength;

    // Start is called before the first frame update
    void Start()
    {
        lastTargetPos = target.position;
        Debug.Log("Posicion objetivo: " + lastTargetPos);

        Debug.Log("Posicion del hombro: " + shoulder.position);
        Debug.Log("Posicion del codo: " + elbow.position);
        Debug.Log("Posicion de la mano: " + hand.position);
        
        // BONE 1
        elbowBoneLength = Vector2.Distance(new Vector2(elbow.position.x, elbow.position.y), new Vector2(hand.position.x, hand.position.y));
        Debug.Log("La longitud de elbowBone es " + elbowBoneLength);

        // BONE 2
        shoulderBoneLength = Vector2.Distance(new Vector2(shoulder.position.x, shoulder.position.y), new Vector2(elbow.position.x, elbow.position.y));
        Debug.Log("La longitud de shoulderBone es " + shoulderBoneLength);
    }

    // Update is called once per frame
    void Update()
    {
        if(lastTargetPos != target.position){
            float r = Vector2.Distance(new Vector2(shoulder.position.x, shoulder.position.y), new Vector2(target.position.x, target.position.y));

            if(r <= elbowBoneLength + shoulderBoneLength){
                float cos_q1 = ((r * r) + (shoulderBoneLength * shoulderBoneLength) - (elbowBoneLength * elbowBoneLength)) / (2 * r * shoulderBoneLength);
                float q1 = Mathf.Acos(cos_q1) * Mathf.Rad2Deg;

                float cos_q2 = ((elbowBoneLength * elbowBoneLength) + (shoulderBoneLength * shoulderBoneLength) - (r * r)) / (2 * elbowBoneLength * shoulderBoneLength);
                float q2 = Mathf.Acos(cos_q2) * Mathf.Rad2Deg;

                Vector2 diff = new Vector2(target.position.x, target.position.y) - new Vector2(shoulder.position.x, shoulder.position.y);
                float atan = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

                float jointAngle1 = atan - q1;
                float jointAngle2 = q2 - 180;

                shoulder.localRotation = Quaternion.Euler(-jointAngle1, 90f, 0f);
                elbow.localRotation = Quaternion.Euler(jointAngle2, 0f, 0f);
            } else{
                shoulder.LookAt(target);
                elbow.LookAt(target);
            }
        }
    }
}
