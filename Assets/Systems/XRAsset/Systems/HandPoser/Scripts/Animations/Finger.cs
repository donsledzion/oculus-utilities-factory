public class Finger
{
    public enum FingerType
    {
        None,Thumb,Index,Middle,Ring,Pinky
    }

    public FingerType type = FingerType.None;
    public float current = 0f;
    public float target = 0f;

    public Finger(FingerType type)
    {
        this.type = type;
    }
}
