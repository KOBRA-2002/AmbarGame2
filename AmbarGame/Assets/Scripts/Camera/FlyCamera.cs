using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyCamera : MonoBehaviour
{
    [SerializeField] private Transform TargetForCamera;

    private Vector3 startOffset;
    void Start()
    {
        startOffset = TargetForCamera.position - transform.position;
    }

    void Update()
    {
        Fly();
    }

    private void Fly()
    {
        var currentOffset = TargetForCamera.position - transform.position;
        var deltaOffset = currentOffset - startOffset;
        transform.DOMove(deltaOffset, 1f, false).SetRelative();
    }
}
