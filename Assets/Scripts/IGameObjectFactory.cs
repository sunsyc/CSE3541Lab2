using UnityEngine;

namespace SunYinchu.Lab2
{
    public interface IGameObjectFactory
    {
        GameObject CreateAt(Vector3 position, string name = null);
    }
}
