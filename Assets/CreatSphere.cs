using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatSphere : MonoBehaviour
{
    private Vector3 centerPos;
    private float m_radius = 1000;
    public float radius { get { return m_radius; }set { m_radius = value; } }
    private void Awake()
    {
        CreatMySphere();
    }

    public void CreatMySphere()
    {
        centerPos = transform.position;

        for(int i =0; i<3000;i++)
        {
            Vector3 p = Random.insideUnitSphere * radius;
            Vector3 pos = p.normalized * (2 + p.magnitude);

            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.position = pos;
        }
    }

}
