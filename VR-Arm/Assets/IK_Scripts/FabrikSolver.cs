using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabrikSolver : MonoBehaviour
{

    [SerializeField]
    Transform[] bones;
    float[] bonesLengths;

    [SerializeField]
    int solverIterations = 5; // cuantas veces aplicamos FABRIK 

    [SerializeField]
    Transform targetPosition;

    // Start is called before the first frame update
    private void Start()
    {
        bonesLengths = new float[bones.Length];

        // calculamos la longitud de cada hueso
        for (int i=0; i< bones.Length; i++){
            if (i < bones.Length - 1){
                float boneLength = Vector3.Distance(bones[i].position, bones[i+1].position);
                Debug.Log("La longitud del hueso " + i + " es: " + boneLength);
                bonesLengths[i] = boneLength;
            }else{
                // si es el ultimo hueso, la longitud es 0
                bonesLengths[i] = 0f;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        SolveIK();
    }

    void SolveIK(){
        Vector3[] finalBonesPositions = new Vector3[bones.Length];

        // guardamos las posiciones actuales de los huesos
        for (int i = 0; i < bones.Length; i++){
            finalBonesPositions[i] = bones[i].position;
        }

        // aplicamos FABRIK  tantas veces como se indique en "solverIterations"
        for (int i = 0; i < solverIterations; i++){
            finalBonesPositions = solveForwardPositions(SolveInversePositions(finalBonesPositions));
        }

        // aplicamos los resultados a cada hueso
        for (int i = 0; i < bones.Length; i++){
            bones[i].position = finalBonesPositions[i];

            // aplicamos rotaciones
            if ( i != bones.Length - 1){
                bones[i].LookAt(finalBonesPositions[i + 1]);
            }else{
                bones[i].LookAt(targetPosition.position);
            }
        }
    }

    Vector3[] SolveInversePositions(Vector3[] forwardPositions){
        Vector3[] inversePositions = new Vector3[forwardPositions.Length];

        // calculamos las posiciones "ideales" des del ultimo hasta el primer hueso en base a las posiciones actuales
        for (int i = (forwardPositions.Length - 1); i >= 0; i--){
            if (i == forwardPositions.Length - 1){
                // si es el ultimo hueso, la posicion prima es la misma que la posicion objetivo
                inversePositions[i] = targetPosition.position;
            }else{
                Vector3 posPrimaSiguiente = inversePositions[i + 1];
                Vector3 posBaseActual = forwardPositions[i];
                Vector3 direccion = (posBaseActual - posPrimaSiguiente).normalized; // vector unitario desde posPrimaSiguiente hasta posBaseActual
                float longitud = bonesLengths[i];
                inversePositions[i] = posPrimaSiguiente + (direccion * longitud);
            }
        }
        return inversePositions;
    }

    Vector3[] solveForwardPositions(Vector3[] inversePositions){
        Vector3[] forwardPositions = new Vector3[inversePositions.Length];

        // calculamos las posiciones "reales" des del primer hasta el ultimo hueso en base a las posiciones "ideales"
        for ( int i = 0; i < inversePositions.Length; i++){
            if (i == 0){
                // si es el primer hueso, la posicion es la misma que la posicion del primer hueso base
                forwardPositions[i] = bones[0].position;
            }else {
                Vector3 posPrimaActual = inversePositions[i];
                Vector3 posPrimaSegundaAnterior = forwardPositions[i - 1];
                Vector3 direccion = (posPrimaActual - forwardPositions[i - 1]).normalized; // vector unitario desde posPrimaSegundaAnterior hasta posPrimaActual
                float longitud = bonesLengths[i - 1];
                forwardPositions[i] = posPrimaSegundaAnterior + (direccion * longitud);
            }
        }

        return forwardPositions;
    }

}