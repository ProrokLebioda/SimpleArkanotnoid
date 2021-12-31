using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int points = 100;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
