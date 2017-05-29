using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupLogic : MonoBehaviour {


    [SerializeField]
    float timeToRegen = 3f;
    [SerializeField]
    float amountToRegen = 3;
    [SerializeField]
    AudioClip powerupSound;
    AudioSource powerupSource;


    BoxCollider _collider;

    [SerializeField]
    GameObject activeVisuals;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        powerupSource = gameObject.AddComponent<AudioSource>();
        powerupSource.clip = powerupSound;
        powerupSource.spatialBlend = .5f;
        powerupSource.Stop();

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if(other.tag == "CarCollider")
        {
            other.transform.parent.parent.GetComponent<RocketCarManager>().AddBoost(amountToRegen);
            activeVisuals.SetActive(false);
            _collider.enabled = false;
            Invoke("Regenerated", timeToRegen);

            if (SceneData.Instance.GetData("sound") == 1)
                powerupSource.Play();
        }
    }

    void Regenerated()
    {
        activeVisuals.SetActive(true);
        _collider.enabled = true;
    }

}
