using UnityEngine;

public class GrapplingRope
{
    private LineRenderer _lineRenderer;
    private Vector3 _currentGrappleLocalPosition;
    private Spring _spring;
    
    public GrapplingGun GrapplingGun;
    
    [Header("Grappling Rope Properties")]
    private int RopePoints = 270;
    private float Damper = 14;
    private float Strength = 150;
    private float Velocity = 13;
    private float WaveCount = 3;
    private float WaveHeight = 11;
    public AnimationCurve AffectCurve;

    public GrapplingRope(AnimationCurve affectCurve, GrapplingGun grapplingGun, LineRenderer lineRenderer)
    {
        AffectCurve = affectCurve;
        GrapplingGun = grapplingGun;
        _lineRenderer = lineRenderer;
        Initialize();
    }
    
    private void Initialize()
    {
        _lineRenderer.enabled = true;
        _spring = new Spring();
        _spring.SetTarget(0);
    }
    
    public void DrawRope()
    {
        if (!GrapplingGun.IsGrappling())
        {
            _currentGrappleLocalPosition = GrapplingGun.GunTip.transform.position;
            _spring.Reset();
            
            if (_lineRenderer.positionCount > 0)
            {
                _lineRenderer.positionCount = 0;
            }
            return;
        }

        if (_lineRenderer.positionCount == 0)
        {
            _spring.SetVelocity(Velocity);
            _lineRenderer.positionCount = RopePoints + 1;
        }

        _spring.SetDamper(Damper);
        _spring.SetStrength(Strength);
        _spring.Update(Time.deltaTime);

        Vector3 grapplePoint = GrapplingGun.GetGrapplePoint();
        Vector3 gunTipPosition = GrapplingGun.GunTip.position;
        Vector3 up = Quaternion.LookRotation(grapplePoint - gunTipPosition).normalized * Vector3.up;
        
        _currentGrappleLocalPosition = Vector3.Lerp(_currentGrappleLocalPosition, grapplePoint, Time.deltaTime * 12f);

        for (int i = 0; i < RopePoints + 1; i++)
        {
            var delta = i / (float) RopePoints;
            var offset = up * WaveHeight * 
                         Mathf.Sin(delta * WaveCount * Mathf.PI) * _spring.Value * AffectCurve.Evaluate(delta);
            _lineRenderer.SetPosition(
                i, 
                Vector3.Lerp(gunTipPosition, _currentGrappleLocalPosition, delta) + offset);
        }
        
    }
}
