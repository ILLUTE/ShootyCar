using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;

    public static ResourceManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ResourceManager>();
            }

            return instance;
        }
    }
    public Dictionary<int, CarBaseData> carData = new Dictionary<int, CarBaseData>();
    public Dictionary<int, TurretBaseData> turretData = new Dictionary<int, TurretBaseData>();

    private void Awake()
    {
        var data = Resources.LoadAll("ScriptableObjects/Cars");

        foreach(CarBaseData x in data)
        {
            if(!carData.ContainsKey(x.carId))
            {
                carData.Add(x.carId, x);
            }
        }

        var turret = Resources.LoadAll("ScriptableObjects/Turrets");

        foreach(TurretBaseData x in turret)
        {
            if(!turretData.ContainsKey(x.turretId))
            {
                turretData.Add(x.turretId, x);
            }
        }
    }
}
