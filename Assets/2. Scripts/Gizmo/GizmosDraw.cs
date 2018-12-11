using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDraw : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5.0f);
    }


}
