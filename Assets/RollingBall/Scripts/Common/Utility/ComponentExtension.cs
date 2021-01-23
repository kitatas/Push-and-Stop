using System.Collections.Generic;
using UnityEngine;

namespace RollingBall.Common.Utility
{
    public static class ComponentExtension
    {
        public static void SetActiveAll(this IEnumerable<Component> components, bool value)
        {
            foreach (var component in components)
            {
                component.gameObject.SetActive(value);
            }
        }
    }
}