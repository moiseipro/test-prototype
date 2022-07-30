using System;
using EzySlice;
using Game;
using UnityEngine;

namespace GardenManagement
{
    public class GardenBed: MonoBehaviour
    {
        [SerializeField]private GameObject objectToSlice;
        private GardenBedFeatures _gardenBedFeatures;
        private GrassBlock _grassBlockPrefab;

        private Mesh _baseMesh;
        private MeshFilter _meshFilter;
        
        private bool isCutted = false;
        public bool IsCutted => isCutted;

        public void SetGardenBedFeatures(GardenBedFeatures gardenBedFeatures)
        {
            _gardenBedFeatures = gardenBedFeatures;
        }

        public void SetGrassBlock(GrassBlock grassBlock)
        {
            _grassBlockPrefab = grassBlock;
        }

        private void Awake()
        {
            _meshFilter = objectToSlice.GetComponent<MeshFilter>();
            _baseMesh = objectToSlice.GetComponent<MeshFilter>().mesh;
        }

        public SlicedHull Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection) {
            return objectToSlice.Slice(planeWorldPosition, planeWorldDirection);
        }

        public void Cut()
        {
            SlicedHull test = Slice(Vector3.up, Vector3.up);
            _meshFilter.mesh = test.lowerHull;
            isCutted = true;
            GrassBlock grassBlock = Instantiate(_grassBlockPrefab, transform.position+transform.up, Quaternion.identity);
            grassBlock.Initialization(_gardenBedFeatures);
            grassBlock.Toss();
        }
    }
}