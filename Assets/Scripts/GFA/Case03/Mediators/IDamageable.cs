using UnityEngine;

namespace GFA.Case03.Mediators
{

    public interface IDamageable
    {
        void ApplyDamage(float damage, GameObject causer = null);
    }

}