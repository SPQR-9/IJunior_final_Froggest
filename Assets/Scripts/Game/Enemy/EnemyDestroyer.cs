using UnityEngine;

public class EnemyDestroyer : StateMachineBehaviour
{ 
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
    }
}
