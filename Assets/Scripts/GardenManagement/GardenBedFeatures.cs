namespace GardenManagement
{
    public class GardenBedFeatures
    {
        private float _growthTime;
        public float GrowthTime => _growthTime;
        private int _cost;
        public int Cost => _cost;

        public GardenBedFeatures(float growthTime, int cost)
        {
            _growthTime = growthTime;
            _cost = cost;
        }
    }
}