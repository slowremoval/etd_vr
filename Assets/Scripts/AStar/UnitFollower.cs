using UnityEngine;
using System.Threading.Tasks;

//[RequireComponent(typeof(ConfigurableJoint))]
public class UnitFollower
{
    private Transform _currentTarget;
    private float _speed = 2;
    private Vector3[] _path;
    private int _targetIndex;

    public float PathUpdateInterval = 0.2f;
    private float _timer;
    private ConfigurableJoint _configurableJoint;

    private bool _isTaskCanceled = false;

    //private Quaternion _startRotation;
    private Vector3 _currentWaypoint;
    private Transform _golem;
    private CapsuleCollider _golemCollider;

    public UnitFollower(ConfigurableJoint configurableJoint, Transform golem, Transform currentTarget)
    {
        _configurableJoint = configurableJoint;
        _golem = golem;
        _golemCollider = _golem.gameObject.GetComponent<CapsuleCollider>();
        _currentTarget = currentTarget;
    }


    // private void Start()
    // {
    //     _configurableJoint = GetComponent<ConfigurableJoint>();
    //     PathRequestManager.RequestPath(transform.position, _currentTarget.position, OnPathFound);
    // }

    public void FollowPath()
    {
        if (_currentTarget == null)
        {
            return;
        }


        if (_timer >= PathUpdateInterval)
        {
            PathRequestManager.RequestPath(_golem.transform.position, _currentTarget.position, OnPathFound);
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            _path = newPath;
            _targetIndex = 0;
            //_startRotation = transform.rotation;
            Rotate();
            //StopCoroutine("FollowPath");
            //StartCoroutine("FollowPath");
            StopPathFollowingTask();
            _currentWaypoint = Vector3.zero;
            StartPathFollowingTask();
        }
    }

    public void StopPathFollowingTask()
    {
        _isTaskCanceled = true;
    }

    public async void StartPathFollowingTask()
    {
        _isTaskCanceled = false;
        await Task.Run(FollowPathAsync);
    }

    private async Task FollowPathAsync()
    {
        while (_isTaskCanceled == false)
        {
            _currentWaypoint = _path[0];
            if (Vector3.Distance(_golemCollider.center, _currentWaypoint) <= 1.14f)
            {
                _targetIndex++;
                if (_targetIndex > _path.Length)
                {
                    return;
                }

                _currentWaypoint = _path[_targetIndex];
            }

            //Rotate();

            //Debug.Log("rotated!");
            await Task.Yield();
        }
    }

    private void Rotate()
    {
        Vector3 toTarget = _currentWaypoint - _golem.transform.position;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);
        Quaternion rotation = Quaternion.LookRotation(-toTargetXZ);

        _configurableJoint.targetRotation = Quaternion.Inverse(rotation);
        Debug.Log("rotated!");
    }

    // public void OnDrawGizmos()
    // {
    //     if (_path != null)
    //     {
    //         for (int i = _targetIndex; i < _path.Length; i++)
    //         {
    //             Gizmos.color = Color.black;
    //             Gizmos.DrawCube(_path[i], Vector3.one);
    //
    //             if (i == _targetIndex)
    //             {
    //                 Gizmos.DrawLine(_golem.transform.position, _path[i]);
    //             }
    //             else
    //             {
    //                 Gizmos.DrawLine(_path[i - 1], _path[i]);
    //             }
    //         }
    //     }
    // }
}