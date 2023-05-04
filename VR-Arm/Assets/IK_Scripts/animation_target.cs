using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_target : MonoBehaviour
{

    [SerializeField]
    Transform T1;

    [SerializeField]
    Transform[] targets;

    public float duration = 2.0f;
    private int currentTargetIndex = 0;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;  
    }

    // Update is called once per frame
    void Update()
    {
        float timeElapsed = Time.time - startTime;
        float t = Mathf.Clamp01(timeElapsed / duration);

        T1.position = Vector3.Lerp(T1.position, targets[currentTargetIndex].position, t);

        if (t >= 0.25f) {
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
            startTime = Time.time;
        }

        // Transform.Translate(vector_moviment * velocitat * Time.deltaTime)
    }
}
