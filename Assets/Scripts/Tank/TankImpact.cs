using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankImpact : MonoBehaviour
{
    private Rigidbody m_Rigidbody;         

    // Start is called before the first frame update
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void OnBombImpact(){
        Vector3 impulse = new Vector3(Random.Range(25.0f, -25.0f), 0.0f, Random.Range(25.0f, -25.0f));
        m_Rigidbody.AddForce(impulse, ForceMode.Impulse);
    }
}
