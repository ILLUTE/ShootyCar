using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }

            return instance;
        }
    }
    private string savePath;

    private float startTime, gameTime;

    private int totalEnemies = 7;

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "LevelData", "level.json");

        LevelStartData data = new LevelStartData();

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            data = (LevelStartData)JsonConvert.DeserializeObject<LevelStartData>(json);
        }

        if (data == null)
        {
            data = new LevelStartData();
        }

        LoadCar(data);

        startTime = Time.time;
    }

    [SerializeField]
    private Transform m_CarSpawn;

    private void LoadCar(LevelStartData data)
    {
        CarBaseData c = ResourceManager.Instance.carData[data.carId];
        TurretBaseData t = ResourceManager.Instance.turretData[data.turretId];

        CarController car = Instantiate(c.carPrefab);
        car.IsKinematic(true);
        car.transform.position = m_CarSpawn.position;
        car.IsKinematic(false);
        car.SetAttributes(c);
        TurretController turret = Instantiate(t.turret);
        turret.transform.SetParent(car.carAttachmentPoint.turretPoint);
        turret.transform.localPosition = Vector3.zero;

        gameTime = data.gameTime;
    }

    public void EnemyTurretDestroyed()
    {
        totalEnemies--;

        if (totalEnemies <= 0)
        {
            if (Time.time - startTime <= gameTime)
            {
                Debug.Log("Player Win");
            }
        }
    }
}
