using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotationInput
{
    bool isPressingRotation { get; }
    float rotationAmount { get; }
}
