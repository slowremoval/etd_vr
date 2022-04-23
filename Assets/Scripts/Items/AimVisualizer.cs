using UnityEngine;

public class AimVisualizer
{
    private LayerMask _targetLayer;
    private GameObject _aimPrefab;
    private Transform _gunTip;
    private float _maxDistance;
    private float _aimSize = 0.033f;

    public AimVisualizer(GameObject aimPrefab, Transform gunTip, float maxDistance, LayerMask targetLayer)
    {
        _aimPrefab = aimPrefab;
        _gunTip = gunTip;
        _maxDistance = maxDistance;
        _targetLayer = targetLayer;
    }
    
    private void ActivateAim()
    {
        _aimPrefab.SetActive(true);
    }

    private void DeactivateAim()
    {
        _aimPrefab.SetActive(false);
    }

    private void AimScaling(Vector3 aimPoint)
    {
        float scale = Vector3.Distance(_gunTip.transform.position, aimPoint);
        _aimPrefab.transform.localScale = Vector3.one * scale * _aimSize;
    }

    public void Aiming()
    {
        Ray ray = new Ray(_gunTip.transform.position, _gunTip.forward);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, _maxDistance, _targetLayer))
        {
            _aimPrefab.transform.position = hitInfo.point;
            _aimPrefab.transform.rotation = Quaternion.LookRotation(hitInfo.normal);

            AimScaling(hitInfo.point);
            ActivateAim();
        }
        else
        {
            DeactivateAim();
        }
    }
}
