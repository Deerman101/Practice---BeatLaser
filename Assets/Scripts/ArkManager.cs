using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _targetTransforms;
    [SerializeField] private float _angularSpeed = 10.0f;
    [SerializeField] private List<float> _rotationOffsets;

    private void Awake()
    {
        if (_rotationOffsets.Count != _targetTransforms.Count)
        {
            Debug.LogWarning("Кол-во вращений не совпадает с кол-вом предметов вращения! " + "Заполните список rotationOffsets нулями хотя бы!");
            _rotationOffsets.Clear();
            for (int i = 0; i < _targetTransforms.Count; i++)
            {
                _rotationOffsets.Add(0.0f);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < _targetTransforms.Count; i++)
        {
            float angle = _angularSpeed * Time.deltaTime + _rotationOffsets[i];
            _targetTransforms[i].Rotate(0, 0, angle);
        }
    }

}
