using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletStats : MonoBehaviour
{
    public int flatHP;
    public int flatEP;
    [Range(1f, 3f)]
    public float factorHP;
    [Range(1f, 3f)]
    public float factorEP;
    [Range(1f, 3f)]
    public float HPrecovery;
    [Range(1f, 3f)]
    public float EPrecovery;
    [Range(1f, 3f)]
    public float bonusSpeed;
    [Range(1f, 3f)]
    public float bonusShield;
    [Range(1f, 3f)]
    public float bonusAttack;

}
