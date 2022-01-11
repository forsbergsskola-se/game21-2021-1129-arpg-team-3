using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public  class State 
{
   public bool patrol;
   public enum STATE
   {
      Idle, Patrol, Pursue, Attack, Rest, Wander
   }

   public enum EVENT
   {
      Enter, Update, Exit
   }

   public STATE Name;
   protected EVENT Stage;
   protected GameObject Npc;
   protected Animator Anim;
   public Transform Player;
   protected State NextState;
   protected NavMeshAgent Agent;
   protected WayPointManager WpManager;
   public Seeing Seeing;

   public State(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
   {
      Npc = npc;
      Stage = EVENT.Enter;
      Agent = agent;
      Anim = anim;
      Player = player;
      WpManager = GameObject.FindWithTag("wpManager").GetComponent<WayPointManager>();
   }

   public virtual void Enter()
   {
      Stage = EVENT.Update;
   }
   public virtual void Update()
   {
      Stage = EVENT.Update;
   }
   public virtual void Exit()
   {
      Stage = EVENT.Exit;
   }

   public State Process()
   {
      switch (Stage)
      {
         case EVENT.Enter:
            Enter();
            break;
         case EVENT.Update:
            Update();
            break;
         case EVENT.Exit:
            Exit();
            return NextState;
      }
      return this;
   }
   
}

public class Idle : State
{
   public Idle(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Idle;
   }

  // public override void Enter()
   //{
     // base.Enter();
   //}
   public override void Update()
   {
      if (Seeing == Seeing.Player)
      {
         NextState = new Pursue(Npc, Agent, Anim, Player);
         Stage = EVENT.Exit;
      }
      
      else if (Seeing != Seeing.Player) 
      {
         NextState = new Idle(Npc, Agent, Anim, Player);
         Stage = EVENT.Exit;
      }
   }
   //public override void Exit()
   //{
     // base.Exit();
   //}
}


public class Patrol : State
{
   private int currentIndex = -1;
   public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Patrol;
      Agent.speed = 2;
      Agent.isStopped = false;
   }

   public override void Enter()
   {
      currentIndex = 0;
      Anim.SetFloat("Speed", 1f);
      Agent.SetDestination(WpManager.GetLocationOfPoint(currentIndex));
      base.Enter();
   }
   public override void Update()
   {
      if (Agent.remainingDistance < 0.5f)
      {
         
         if (currentIndex >= WpManager.CurrentNumberPoints - 1)
         {
            currentIndex = -1;
         }

         else
         {
            currentIndex++;
            Agent.SetDestination(WpManager.GetLocationOfPoint(currentIndex));
         }
      }
      
      if (Seeing == Seeing.Player)
      {
         NextState = new Pursue(Npc, Agent, Anim, Player);
         Stage = EVENT.Exit;
      }
   }
   //public override void Exit()
   //{
     // base.Exit();
   //}
}

public class Wander : State
{
   private float WanderRadius = 2;
   private float WanderDistance = 2;
   private float WanderJitter = 1;
   Vector3 WanderTarget = Vector3.zero;
   public Wander(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Wander;
      SetupWander();
   }

   private void SetupWander()
   {
      WanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * WanderJitter, 0,
         Random.Range(-1.0f, 1.0f) * WanderJitter);
      WanderTarget.Normalize();
      WanderTarget *= WanderRadius;

      Vector3 targetLocal = WanderTarget + new Vector3(0, 0, WanderDistance);
      Vector3 targetWorld = Npc.transform.InverseTransformVector(targetLocal);
      Seek(targetWorld);
   }

   void Seek(Vector3 location)
   {
      Agent.SetDestination(location);
   }
   
  // public override void Enter()
   //{
     // base.Enter();
      
   //}
   public override void Update()
   {
        SetupWander();
        
        if (Random.Range(0, 1000) < 10)
        {
           NextState = new Idle(Npc, Agent, Anim, Player);
           Stage = EVENT.Exit;
        }
        
        if (Seeing == Seeing.Player)
        {
           NextState = new Pursue(Npc, Agent, Anim, Player);
           Stage = EVENT.Exit;
        }
   }
   //public override void Exit()
   //{
     // base.Exit();
   //}
}

public class Pursue : State
{
   public Pursue(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Pursue;
      Agent.speed = 3;
      Agent.isStopped = false;
      Anim.SetFloat("Speed", 3);
   }

   public override void Enter()
   {
      base.Enter();
      Agent.SetDestination(Player.position);
      
   }
   public override void Update()
   {
      if (Agent.remainingDistance > 3)
      {
         NextState = new Idle(Npc, Agent, Anim, Player);
         Stage = EVENT.Exit;
      }

      if (Agent.remainingDistance < 1.2f)
      {
         Agent.isStopped = true;
         NextState = new Attack(Npc, Agent, Anim, Player);
         Stage = EVENT.Exit;
         Anim.SetFloat("Speed", 0);
      }
   }
  // public override void Exit()
   //{
     // base.Exit();
   //}
}

public class Attack : State
{
   public EnemyAttackAnimation AttackAnimation;
   public Attack(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Attack;
      Anim.SetBool("CombatMode", true);
   }
   
   
  // public override void Enter()
   //{
      
     // base.Enter();
   //}
   public override void Update()
   {
      AttackAnimation.TryAttack(Anim);
      
      if (Vector3.Distance(Player.position, Npc.transform.position) > 2)
      {
         NextState = new Pursue(Npc, Agent, Anim, Player);
         Anim.SetBool("CombatMode", false);
         Stage = EVENT.Exit;
      }
   }
   // public override void Exit()
   //{
     // base.Exit();
   //}
}