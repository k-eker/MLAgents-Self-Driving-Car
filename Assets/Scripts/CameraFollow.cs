using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    [SerializeField] private Vector3 m_Offset;

    private void Update()
    {
        transform.position = m_Target.position + m_Offset;
    }
}
