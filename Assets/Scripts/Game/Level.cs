public struct Level
{
    //Attributes:
    public string sceneName;
    public float recordTime;
    public bool unlocked;

    //Constructor:
    public Level(string sceneName, bool unlocked = false, float recordTime = 0.0f)
    {
        this.sceneName = sceneName;
        this.recordTime = recordTime;
        this.unlocked = unlocked;
    }
}
