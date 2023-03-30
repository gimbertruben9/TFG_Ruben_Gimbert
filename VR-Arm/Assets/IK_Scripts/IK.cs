using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{

    [SerializeField]
    Transform bone1;
    [SerializeField]
    Transform bone2;
    [SerializeField]
    Transform target;
    
    private float lengthB1;
    private float lengthB2;


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Posicion objetivo: " + target.position);

        // BONE 1
        float h1 = bone1.localScale.y;
        Debug.Log("La altura de bone1 es " + h1);

        // Obtener la mitad de la escala del cubo
        Vector3 halfScale_B1 = bone1.localScale * 0.5f;

        // Obtener la matriz de rotación del cubo
        Matrix4x4 rotationMatrix_B1 = Matrix4x4.TRS(Vector3.zero, bone1.rotation, Vector3.one);

        // Calcular la posición del centro de la base superior del cubo en local space
        Vector3 localTopCenter_B1 = new Vector3(0f, halfScale_B1.y, 0f);

        // Calcular la posición del centro de la base inferior del cubo en local space
        Vector3 localBottomCenter_B1 = new Vector3(0f, -halfScale_B1.y, 0f);

        // Transformar las posiciones del centro de las bases a world space
        Vector3 worldTopCenter_B1 = rotationMatrix_B1.MultiplyPoint(localTopCenter_B1) + bone1.position;
        Vector3 worldBottomCenter_B1 = rotationMatrix_B1.MultiplyPoint(localBottomCenter_B1) + bone1.position;
        Debug.Log("Extremo superior bone1: " + worldTopCenter_B1);
        Debug.Log("Extremo inferior bone1: " + worldBottomCenter_B1);

        // BONE 2
        float h2 = bone2.localScale.y;
        Debug.Log("La altura de bone2 es " + h2);

        // Obtener la mitad de la escala del cubo
        Vector3 halfScale_B2 = bone2.localScale * 0.5f;

        // Obtener la matriz de rotación del cubo
        Matrix4x4 rotationMatrix_B2 = Matrix4x4.TRS(Vector3.zero, bone2.rotation, Vector3.one);

        // Calcular la posición del centro de la base superior del cubo en local space
        Vector3 localTopCenter_B2 = new Vector3(0f, halfScale_B2.y, 0f);

        // Calcular la posición del centro de la base inferior del cubo en local space
        Vector3 localBottomCenter_B2 = new Vector3(0f, -halfScale_B2.y, 0f);

        // Transformar las posiciones del centro de las bases a world space
        Vector3 worldTopCenter_B2 = rotationMatrix_B2.MultiplyPoint(localTopCenter_B2) + bone2.position;
        Vector3 worldBottomCenter_B2 = rotationMatrix_B2.MultiplyPoint(localBottomCenter_B2) + bone2.position;
        Debug.Log("Extremo superior bone2: " + worldTopCenter_B2);
        Debug.Log("Extremo inferior bone2: " + worldBottomCenter_B2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
