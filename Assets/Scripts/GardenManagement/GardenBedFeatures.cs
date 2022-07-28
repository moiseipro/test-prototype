using System;
using EzySlice;
using UnityEngine;

namespace GardenManagement
{
    public class GardenBedFeatures: MonoBehaviour
    {
        [SerializeField]private float growthTime;
        [SerializeField]private int cost;

        public GameObject objectToSlice; // non-null


        public SlicedHull Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection) {
            return objectToSlice.Slice(planeWorldPosition, planeWorldDirection);
        }
        
        private void Start()
        {
            SlicedHull test = Slice(Vector3.up, Vector3.up);
            objectToSlice.GetComponent<MeshFilter>().mesh = test.lowerHull;
        }
    }
}