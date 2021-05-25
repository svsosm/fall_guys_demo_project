using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platform
{

    public enum PlatformTypes
    {
        STATIC,
        ROTATING
    }

    public enum Directions
    {
        LEFT,
        RIGHT
    }

    [CreateAssetMenu(fileName = "New Platform", menuName = "Platform")]
    public class PlatformScriptableObject : ScriptableObject
    {
        public PlatformTypes platformType;
        public Directions direction;
        public float rotateSpeed;
    }

}