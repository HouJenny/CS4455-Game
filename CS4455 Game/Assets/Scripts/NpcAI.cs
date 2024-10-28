using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent nav_mesh_agent;

    public GameObject[] waypoints;

    public GameObject cat;

    public Animator anim;

    public int currWaypoint;

    public enum AIState {
        CatPetting,
        WalkingAround,
        Attacked
    }

    public AIState ai_state;

    public float cat_offset;

    void Awake()
    {
        nav_mesh_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ai_state = AIState.WalkingAround;
        cat_offset = (transform.position - cat.transform.position).magnitude; 

    }

    // Update is called once per frame
    void Update()
    {
        switch (ai_state) {
            case AIState.WalkingAround:
                nav_mesh_agent.isStopped = false;
                cat_offset = (transform.position - cat.transform.position).magnitude; 
                // if (Input.GetKeyDown(KeyCode.R) && cat_offset <= 5) {
                //     ai_state = AIState.CatPetting;
                // }
                if (Input.GetKeyDown(KeyCode.Z) && cat_offset <= 5) {
                    ai_state = AIState.Attacked;
                }
                if ((nav_mesh_agent.pathPending == false) && (nav_mesh_agent.remainingDistance <= 0.5)) {
                    setNextWaypoint();
                }
            break;

            // case AIState.CatPetting:
            //     // play petting animation
            //     nav_mesh_agent.isStopped = true;
            //     anim.SetTrigger("PetTrigger");
            //     ai_state = AIState.WalkingAround;
            // break;

            case AIState.Attacked:
                nav_mesh_agent.isStopped = true;
                anim.SetTrigger("FallTrigger");
            break;
        }
    }

        private void setNextWaypoint() {
        currWaypoint += 1;
        if (currWaypoint > waypoints.Length - 1) {
            currWaypoint = 0;
        }

        if (waypoints.Length > 0) {
            nav_mesh_agent.SetDestination(waypoints[currWaypoint].transform.position);            
        } else {
            print("No waypoints found.");
        }
        

    }
}
