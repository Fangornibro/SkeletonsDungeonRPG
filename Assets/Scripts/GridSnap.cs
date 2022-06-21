using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnap : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        VectorRound();
    }
    private void VectorRound()
    {
        Vector3 vect;
        if (transform.CompareTag("Wall"))
        {
            vect = new Vector3(Mathf.RoundToInt(this.transform.position.x), this.transform.position.y, Mathf.RoundToInt(this.transform.position.z));
        }
        else
        {
            vect = new Vector3Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), Mathf.RoundToInt(this.transform.position.z));
        }
        this.transform.position = vect;
    }
}
