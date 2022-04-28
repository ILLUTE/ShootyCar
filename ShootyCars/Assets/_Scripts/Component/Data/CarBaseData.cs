using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Cars")]
public class CarBaseData : ScriptableObject
{
    public int carId;
    public float maxHealth;
    public float maxSpeed;
    public float carControls;

    public CarController carPrefab;
    public Sprite carIcon;
}
