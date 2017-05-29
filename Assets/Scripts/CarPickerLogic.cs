using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPickerLogic : MonoBehaviour
{
    [SerializeField]
    GameObject carPrefab;
    [SerializeField]
    float rotationSpeed = 5f;

    int currentCar = 0;
    int numberOfCarModels;
    bool isBlue;

    GameObject pickedCar;

    public int CurrentCar
    {
        get
        {
            return currentCar;
        }

        set
        {
            currentCar = value % numberOfCarModels;
        }
    }

    void UpdateCarToPick(int index, bool isBlue)
    {
        Destroy(pickedCar);
        GameObject go =
            Instantiate(carPrefab, transform.position,
            transform.rotation, transform);
        pickedCar = go;
        pickedCar.GetComponent<Rigidbody>().isKinematic = true;
        pickedCar.GetComponent<RocketCarManager>().IsBlueTeam = isBlue;
        pickedCar.GetComponent<RocketCarManager>().CarVisualsIndex = index;
    }

    // Use this for initialization
    void Start()
    {
        isBlue = SceneData.Instance.GetData("is_blue") == 0 ? false : true;
        numberOfCarModels = SceneData.Instance.GetData("car_models");
        UpdateCarToPick(CurrentCar, isBlue);
    }

    public void NextCar()
    {
        ++CurrentCar;
        UpdateCarToPick(CurrentCar, isBlue);
    }

    public void PreviousCar()
    {
        --CurrentCar;
        if (CurrentCar < 0)
            CurrentCar = 2;
        UpdateCarToPick(CurrentCar, isBlue);
    }

    public void ToggleColor()
    {
        isBlue = !isBlue;
        SceneData.Instance.SetData("is_blue", isBlue ? 1 : 0);
        UpdateCarToPick(CurrentCar, isBlue);
    }

    public void GoToMenu()
    {
        SceneData.Instance.SetData("picked_car", CurrentCar);
        LevelManager.LoadLevel(1);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }
}
