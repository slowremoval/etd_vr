using UnityEngine;

    public class GolemBehaviorIdleWithoutLeg : IGolemBehavior
    {
        private int animationHash = Animator.StringToHash("Stas_Idle_WithoutLegs");
        private Animator _animator;
        DetectionManager _golemDetectionManager;

        public GolemBehaviorIdleWithoutLeg(Animator animator, DetectionManager detectionManager)
        {
            _animator = animator;
            _golemDetectionManager = detectionManager;
        }
        
        public void Enter()
        {
            Debug.Log("Golem IDLE_WITHOUT_LEG Enter");
            _animator.CrossFade(animationHash, 0.03f);
            _golemDetectionManager.CurrentTarget = null;
        }

        public void Exit()
        {
            Debug.Log("Golem IDLE_WITHOUT_LEG Exit");
        }

        public void Update()
        {
            if (_golemDetectionManager.CurrentTarget == null)
            {
                _golemDetectionManager.HandleDetection();
            }
        }

        public void FixedUpdate()
        {
            //throw new System.NotImplementedException();
        }
    }