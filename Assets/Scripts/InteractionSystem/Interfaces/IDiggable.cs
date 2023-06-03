
public interface IDiggable
{
    public bool IsDug { get; set; }

    public void Dig();
    public void Fill();
    public void DigHover();
    public void DigUnHover();
}
