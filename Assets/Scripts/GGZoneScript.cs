using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGZoneScript : MonoBehaviour
{
    public ScoreManagerScript _managerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cube")
        {
            Destroy(other.gameObject);
            _managerScript.RemoveCombo();
        }

    }
}
