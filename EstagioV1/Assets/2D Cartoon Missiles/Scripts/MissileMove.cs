using UnityEngine;
using System.Collections;

public class MissileMove : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(0).GetComponent<ParticleEmitter>().emit = true;
	}

    void FixedUpdate()
    {
        transform.Translate(0, 0.5f, 0);
    }
}
