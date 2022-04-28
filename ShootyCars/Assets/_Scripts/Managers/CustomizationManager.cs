using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationManager : MonoBehaviour
{
    private static CustomizationManager instance;

    public static CustomizationManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CustomizationManager>();
            }

            return instance;
        }
    }

    [Header("Reference Positions")]
    [SerializeField]
    private Transform m_CarSpawn;

    private CarController currentCarController;
    private TurretController currentTurret;

    private CarBaseData currentCarData;
    private TurretBaseData currentTurretData;

    private SelectionState m_State = SelectionState.Car_Selection;

    public static event Action<SelectionState> OnStateChanged;
    public static event Action<CarBaseData> OnCarChanged;
    public static event Action<TurretBaseData> OnTurretChanged;

    private float gameTime = 30;

    private string savePath;

    public void LoadCar(CarBaseData car)
    {
        if(currentCarController != null)
        {
            Destroy(currentCarController.gameObject);
        }

        currentCarController = Instantiate(car.carPrefab);

        //currentCarController.IsKinematic(true);

        currentCarController.transform.position = m_CarSpawn.position;

        currentCarData = car;

        if(currentTurret!= null)
        {
            currentTurret.transform.position = currentCarController.carAttachmentPoint.turretPoint.position;
        }

        OnCarChanged?.Invoke(car);
    }

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "LevelData");
        m_State = SelectionState.Car_Selection;
        OnStateChanged?.Invoke(m_State);
    }

    public void LoadTurret(TurretBaseData turret)
    {
        if(currentTurret!= null)
        {
            Destroy(currentTurret.gameObject);
        }

        currentTurretData = turret;

        currentTurret = Instantiate(turret.turret);

        currentTurret.IsTurretSearching = false;

        currentTurret.transform.position = currentCarController.carAttachmentPoint.turretPoint.position;

        OnTurretChanged?.Invoke(turret);
    }

    public void ToggleSelection()
    {
        m_State = m_State == SelectionState.Car_Selection ? SelectionState.Turret_Selection : SelectionState.Car_Selection;

        OnStateChanged?.Invoke(m_State);
    }

    public CarBaseData GetCurrentCar()
    {
        return currentCarData;
    }

    public TurretBaseData GetCurrentTurret()
    {
        return currentTurretData;
    }

    public void SetGameTime(float time)
    {
        gameTime = time;
    }

    public void StartGame()
    {
        LevelStartData startData = new LevelStartData();
        startData.carId = currentCarData.carId;
        startData.turretId = currentTurretData.turretId;
        startData.gameTime = gameTime;

        if(!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        string json = JsonConvert.SerializeObject(startData);

        string file = Path.Combine(savePath, "level.json");

        File.WriteAllText(file, json);

        SceneManager.LoadScene("ShootyCars");
    }
}
