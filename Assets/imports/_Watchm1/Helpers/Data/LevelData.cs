namespace _Watchm1.Helpers.Data
{ 
    [System.Serializable]
    public class LevelData
    {
        public int currentLevelIndex;
        public int displayLevelIndex;
        public bool isLevelRandomly;

        public LevelData()
        {
            currentLevelIndex = 0;
            displayLevelIndex = 1;
            isLevelRandomly = false;
        }
    }
}
