using UnityEngine;

public interface ITarget
{
    public IHost Host { get; }  
    public bool IsHooked { get; }
    public void OnHooked();
    public void Remove();
    public void SetPosition(Vector3 position);
}