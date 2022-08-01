using System;
using System.Collections;
using EzySlice;
using Game;
using UnityEngine;

namespace GardenManagement
{
    public class GardenBed: MonoBehaviour
    {
        [SerializeField]private GameObject objectToSlice;
        [SerializeField] private ParticleSystem _cutEffect;
        private GardenBedFeatures _gardenBedFeatures;
        private GrassBlock _grassBlockPrefab;

        private Mesh _baseMesh;
        private MeshFilter _meshFilter;
        private SlicedHull _slicedHull;
        
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
            _slicedHull = objectToSlice.Slice(Vector3.up, Vector3.up);
        }

        public void Cut()
        {
            _meshFilter.mesh = _slicedHull.lowerHull;
            isCutted = true;
            GrassBlock grassBlock = Instantiate(_grassBlockPrefab, transform.position+transform.up, Quaternion.identity);
            grassBlock.Initialization(_gardenBedFeatures);
            grassBlock.Toss();
            _cutEffect.Play();
            StartCoroutine(GrassGrowth());
        }

        private void Grow()
        {
            _meshFilter.mesh = _baseMesh;
            isCutted = false;
        }
        
        IEnumerator GrassGrowth()
        {
            yield return new WaitForSeconds(_gardenBedFeatures.GrowthTime);
            Grow();
        }
    }
}