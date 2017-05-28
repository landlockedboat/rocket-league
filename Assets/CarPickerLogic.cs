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
    int lastCar = 3;

    GameObject pickedCar;

    public int CurrentCar
    {
        get
        {
            return currentCar;
        }

        set
        {
            currentCar = value % lastCar;
        }
    }

    void UpdateCarToPick(int index)
    {
        Destroy(pickedCar);
        GameObject go =
            Instantiate(carPrefab, transform.position,
            transform.rotation, transform);
        pickedCar = go;
        pickedCar.GetComponent<Rigidbody>().isKinematic = true;
        pickedCar.GetComponent<RocketCarManager>().CarVisualsIndex = index;
    }

    // Use this for initialization
    void Start()
    {
        UpdateCarToPick(CurrentCar);
    }

    public void NextCar()
    {
        ++CurrentCar;
        UpdateCarToPick(CurrentCar);
    }

    public void PreviousCar()
    {
        --CurrentCar;
        UpdateCarToPick(CurrentCar);
    }

    public void GoToMenu()
    {
        SceneData.Instance.SetData("PickedCar", CurrentCar);
        LevelManager.LoadLevel(0);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }
}
