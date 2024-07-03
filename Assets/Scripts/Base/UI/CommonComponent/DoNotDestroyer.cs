using UnityEngine;
using Saem;
namespace Saem
{
    public class DoNotDestroyer : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}