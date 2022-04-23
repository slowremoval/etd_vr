using System.Threading.Tasks;
using UnityEngine;

public class ItemPuller
{
    private float _returnTime;
    
    private bool _isPulling;
    
    private Vector3 _pullPosition;
    private Vector3 _offsetPoint;
    
    private Rigidbody _itemRigidbody;
    
    private Transform _pullingHandTransform;
    private Transform _pullingItemTransform;

    public async void StartWeaponPulling(Transform pullingItemTransform, HandControllerInput hand)
    {
        Debug.Log($"Item puller started!, pullingItemTransform is {pullingItemTransform}, hand is {hand}");
        _isPulling = true;
        _pullingHandTransform = hand.transform;
        _pullingItemTransform = pullingItemTransform;
        _itemRigidbody = pullingItemTransform.gameObject.GetComponent<Rigidbody>();
        _pullPosition = _pullingItemTransform.position;
        _itemRigidbody.Sleep();

        Vector3 pullingHandPosition = _pullingHandTransform.position;
        
        _offsetPoint = hand.name == "LeftHand Controller"
            ? new Vector3(pullingHandPosition.x - 1.75f, pullingHandPosition.y + 1f, pullingHandPosition.z)
            : new Vector3(pullingHandPosition.x + 1.75f, pullingHandPosition.y + 1f, pullingHandPosition.z);
        Debug.Log($"Item pulling started! You pull {pullingItemTransform.gameObject.name}!");
        await ItemPullingAsync();
    }

    private async void WeaponCatch()
    {
        _returnTime = 0;
        _isPulling = false;
        _itemRigidbody.WakeUp();
        
    }

    private async Task ItemPullingAsync()
    {
        if (_isPulling)
        {
            Debug.Log("Item Pulling Async Started!");
            {
                
            }
            while (_returnTime < 1)
            {
                _pullingItemTransform.position =
                    GetQuadraticCurvePoint(_returnTime, _pullPosition, _offsetPoint, _pullingHandTransform.position)
                    + new Vector3(0,0.15f , 0);
                _returnTime += Time.deltaTime * 1.5f;
                
                await Task.Yield();
            }
            {
                WeaponCatch();
                Debug.Log("Item Pulling Async Ended!!");
                return;
            }

        }
    }


    private Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPulling)
        {
            return;
        }
        _itemRigidbody.Sleep();
        _isPulling = false;
    }
}