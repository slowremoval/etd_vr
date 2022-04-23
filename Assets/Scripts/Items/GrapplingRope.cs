using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector3 _currentGrapplePosition;
    private Spring _spring;
    
    public GrapplingGun GrapplingGun;
    
    [Header("Grappling Rope Properties")]
    private int RopePoints = 270;
    private float Damper = 14;
    private float Strength = 150;
    private float Velocity = 13;
    private float WaveCount = 3;
    private float WaveHeight = 1;
    public AnimationCurve AffectCurve;

    // public GrapplingRope(AnimationCurve affectCurve, GrapplingGun grapplingGun, LineRenderer lineRenderer)
    // {
    //     AffectCurve = affectCurve;
    //     GrapplingGun = grapplingGun;
    //     _lineRenderer = lineRenderer;
    // }
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _spring = new Spring();
        _spring.SetTarget(0);
    }

    void LateUpdate()
    {
        DrawRope();
    }
    
    private void DrawRope()
    {
        if (!GrapplingGun.IsGrappling())
        {
            _currentGrapplePosition = GrapplingGun.GunTip.position;
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
        
        _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);

        for (int i = 0; i < RopePoints + 1; i++)
        {
            var delta = i / (float) RopePoints;
            var offset = up * WaveHeight * 
                         Mathf.Sin(delta * WaveCount * Mathf.PI) * _spring.Value * AffectCurve.Evaluate(delta);
            _lineRenderer.SetPosition(
                i, 
                Vector3.Lerp(gunTipPosition, _currentGrapplePosition, delta) + offset);
        }
        
    }
}
