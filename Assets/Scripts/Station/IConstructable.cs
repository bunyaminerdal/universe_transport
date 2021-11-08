using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConstructable
{
    Sprite BttnTexture { get; set; }
    void Place(ConstructionNode node);

}
