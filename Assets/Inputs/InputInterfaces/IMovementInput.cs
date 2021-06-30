using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementInput 
{
    Vector3 moveDirection { get; }
    bool isPressingMovement { get; }
}
