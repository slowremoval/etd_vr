using System;
using UnityEngine;

namespace Physics_proj
{
    public class RayHolder
    {

        private LayerMask _groundLayer;
        private Vector3 _rayDirection;

        private const int _maxRayDistance = 97;

        public RayHolder(LayerMask groundLayer, Transform origin, Vector3 direction)
        {
            _groundLayer = groundLayer;
            _rayDirection = direction;
        }

        public RaycastHit GetGroundHit(Vector3 origin)
        {
            Ray ray = new Ray(origin, _rayDirection);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, _maxRayDistance, _groundLayer))
            {
                return hitInfo;
            }

            throw new Exception("There is no ground!");
        }
    }
}