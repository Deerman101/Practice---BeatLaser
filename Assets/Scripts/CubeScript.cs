using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public float speed;

    private void Update() => transform.position += Time.deltaTime * transform.forward * speed;
}