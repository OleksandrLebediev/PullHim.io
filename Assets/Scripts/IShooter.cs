using UnityEngine;

public interface IShooter 
{
    public void Shoot();
    public bool IsShooting { get; }
}
