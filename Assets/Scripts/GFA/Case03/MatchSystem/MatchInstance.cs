using UnityEngine;

namespace GFA.Case03.MatchSystem
{
    [CreateAssetMenu(menuName = "MatchInstance")]
    public class MatchInstance : ScriptableObject
    {
        public float Time { get; private set; }
        public GameObject Player { get; set; }

        public void AddTime(float delta)
        {
            Time += delta;
        }

        public void Reset()
        {
            Time = 0;
        }
    }
}
