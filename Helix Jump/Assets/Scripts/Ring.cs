using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public Transform ball;
    [SerializeField] private int ringScore;
    void Update()
    {
        if (transform.position.y+10
            >= ball.position.y)
        {
            Destroy(gameObject);
            GameManager.instance.UpdateScore(ringScore);
        }
    }
}
