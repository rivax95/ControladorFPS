using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpreadData", menuName = "Weapon/Spread", order = 1)]
public class SpreadConfiguration : ScriptableObject
{
   
    [Range (0f,0.1f)]
    public float PenalizationCrounch;
    [Range(0f, 0.1f)]
    public float PenalizationMoving;
    [Range(0f, 0.1f)]
    public float PenalizationGrounded;

}
 