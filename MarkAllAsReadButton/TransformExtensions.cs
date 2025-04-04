using UnityEngine;

namespace MarkAllAsReadButton
{
    internal static class TransformExtensions
    {
        public static Transform FindByPath(this Transform parent, string path)
        {
            string[] objectNames = path.Split('/');
            
            if (objectNames.Length == 0)
                return null;
            
            var currentObj = parent;

            foreach (string objectName in objectNames)
            {
                currentObj = currentObj.Find(objectName);
                if (currentObj == null)
                    return null;
            }

            return currentObj;
        }
    }
}
