using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    protected int damage;


    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        health = 120;
        
    }

    protected override void Attack(int amount)
    {
        Debug.Log("Golem attacks!");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
