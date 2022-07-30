using System.Collections;
using System.Collections.Generic;
using Game;
using GardenManagement;
using UnityEngine;

public class GardenHundler : MonoBehaviour
{
    [SerializeField] private GrassBlock grassBlockPrefab;
    [SerializeField] private float gardenGrowthTime = 10;
    [SerializeField] private int gardenBedCosts = 15;

    private GardenBed[] _gardenBeds;
    private GardenBedFeatures _gardenBedFeatures;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _gardenBeds = GetComponentsInChildren<GardenBed>();
        _gardenBedFeatures = new GardenBedFeatures(gardenGrowthTime, gardenBedCosts);
        foreach (var gardenBed in _gardenBeds)
        {
            gardenBed.SetGardenBedFeatures(_gardenBedFeatures);
            gardenBed.SetGrassBlock(grassBlockPrefab);
        }
    }
}
