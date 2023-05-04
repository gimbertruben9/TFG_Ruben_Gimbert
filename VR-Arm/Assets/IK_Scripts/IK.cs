using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
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
    Transform target;

    private Vector3 lastTargetPos;

    private float elbowBoneLength;
    private float shoulderBoneLength;

    private Vector3 worldTopCenter_elbow;
    private Vector3 worldBottomCenter_elbow;
    private Vector3 worldTopCenter_shoulder;
    private Vector3 worldBottomCenter_shoulder;


    // Start is called before the first frame update
    void Start()
    {
        //updateBonesCoordinates();
        lastTargetPos = target.position;
        Debug.Log("Posicion objetivo: " + lastTargetPos);
        
        // BONE 1
        elbowBoneLength = elbowBone.localScale.z;
        Debug.Log("La longitud de elbowBone es " + elbowBoneLength);

        // BONE 2
        shoulderBoneLength = shoulderBone.localScale.z;
        Debug.Log("La longitud de shoulderBone es " + shoulderBoneLength);

        Debug.Log("Posicion del hombro: " + shoulder.position);
        Debug.Log("Posicion del codo: " + elbow.position);
    }

    // Update is called once per frame
    void Update()
    {

        if(lastTargetPos != target.position){
            lastTargetPos = target.position;

            Vector3 targetPos = target.position;
            Vector3 shoulderPos = shoulder.position;
            Vector3 elbowPos = elbow.position;
            Vector3 shoulderToTarget = targetPos - shoulderPos;

            float r = shoulderToTarget.magnitude;

            if(r <= elbowBoneLength + shoulderBoneLength){
                float q2 = Mathf.Acos((r*r - shoulderBoneLength*shoulderBoneLength - elbowBoneLength*elbowBoneLength) / (2 * shoulderBoneLength * elbowBoneLength));
                float q1 = Mathf.Atan2(targetPos.y, targetPos.x) - Mathf.Atan((elbowBoneLength * Mathf.Sin(q2)) / (shoulderBoneLength + elbowBoneLength * Mathf.Cos(q2)));
                
                shoulder.localRotation = Quaternion.Euler(-q1 * Mathf.Rad2Deg, 90f, 0f);
                elbow.localRotation = Quaternion.Euler(-q2 * Mathf.Rad2Deg, 0f, 0f);
                //elbow.LookAt(target);
            }
        }
    }

    void updateBonesCoordinates(){
        // BONE 1
        // Obtener la mitad de la escala
        Vector3 halfScale_elbow = elbowBone.localScale * 0.5f;

        // Obtener la matriz de rotacion
        Matrix4x4 rotationMatrix_elbow = Matrix4x4.TRS(Vector3.zero, elbowBone.rotation, Vector3.one);

        // Calcular la posicion del centro de la base superior en local space
        Vector3 localTopCenter_elbow = new Vector3(0f, 0f, halfScale_elbow.z);

        // Calcular la posicion del centro de la base inferior en local space
        Vector3 localBottomCenter_elbow = new Vector3(0f, 0f, -halfScale_elbow.z);

        // Transformar las posiciones del centro de las bases a world space
        worldTopCenter_elbow = rotationMatrix_elbow.MultiplyPoint(localTopCenter_elbow) + elbowBone.position;
        worldBottomCenter_elbow = rotationMatrix_elbow.MultiplyPoint(localBottomCenter_elbow) + elbowBone.position;

        Debug.Log("Extremo superior elbowBone: " + worldTopCenter_elbow);
        Debug.Log("Extremo inferior elbowBone: " + worldBottomCenter_elbow);

        // BONE 2
        // Obtener la mitad de la escala
        Vector3 halfScale_shoulder = shoulderBone.localScale * 0.5f;

        // Obtener la matriz de rotacion
        Matrix4x4 rotationMatrix_shoulder = Matrix4x4.TRS(Vector3.zero, shoulderBone.rotation, Vector3.one);

        // Calcular la posicion del centro de la base superior en local space
        Vector3 localTopCenter_shoulder = new Vector3(0f, 0f, halfScale_shoulder.z);

        // Calcular la posicion del centro de la base inferior en local space
        Vector3 localBottomCenter_shoulder = new Vector3(0f, 0f, -halfScale_shoulder.z);

        // Transformar las posiciones del centro de las bases a world space
        worldTopCenter_shoulder = rotationMatrix_shoulder.MultiplyPoint(localTopCenter_shoulder) + shoulderBone.position;
        worldBottomCenter_shoulder = rotationMatrix_shoulder.MultiplyPoint(localBottomCenter_shoulder) + shoulderBone.position;

        Debug.Log("Extremo superior shoulderBone: " + worldTopCenter_shoulder);
        Debug.Log("Extremo inferior shoulderBone: " + worldBottomCenter_shoulder);
    }
}
