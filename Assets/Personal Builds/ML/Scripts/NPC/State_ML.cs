using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public  class State_ML 
{
   public enum STATE
   {
      Idle, Patrol, Pursue, Attack, Rest
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
   protected State_ML NextStateMl;
   protected NavMeshAgent Agent;
   protected WayPointManager_ML WpManagerMl;
   public Seeing_ML SeeingMl;

   public State_ML(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
   {
      Npc = npc;
      Stage = EVENT.Enter;
      Agent = agent;
      Anim = anim;
      Player = player;
      WpManagerMl = GameObject.FindWithTag("wpManager").GetComponent<WayPointManager_ML>();
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

   public State_ML Process()
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
            return NextStateMl;
      }
      return this;
   }
   
}

public class Idle_ML : State_ML
{
   public Idle_ML(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Idle;
   }

   public override void Enter()
   {
      base.Enter();
   }
   public override void Update()
   {
      if (SeeingMl == Seeing_ML.Player)
      {
      //   NextStateMl = new Pursue(Npc, Agent, Anim, Player);
         Stage = EVENT.Exit;
      }
      
      else if (Random.Range(0, 200) < 10)
      {
      //   NextStateMl = new Patrol(Npc, Agent, Anim, Player);
         Stage = EVENT.Exit;
      }
   }
   public override void Exit()
   {
      base.Exit();
   }
}


public class Patrol_ML : State_ML
{
   private int currentIndex = -1;
   public Patrol_ML(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
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
      Agent.SetDestination(WpManagerMl.GetLocationOfPoint(currentIndex));
      base.Enter();
   }
   public override void Update()
   {
      if (Agent.remainingDistance < 0.6f)
      {
         
         if (currentIndex >= WpManagerMl.CurrentNumberPoints - 1)
         {
            currentIndex = -1;
         }

         else
         {
            currentIndex++;
            Agent.SetDestination(WpManagerMl.GetLocationOfPoint(currentIndex));
         }
         
         if (SeeingMl == Seeing_ML.Player)
         {
         //   NextStateMl = new Pursue(Npc, Agent, Anim, Player);
            Stage = EVENT.Exit;
         }
      }
   }
   public override void Exit()
   {
      base.Exit();
   }
}


public class Pursue_ML : State_ML
{
   public Pursue_ML(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
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
      base.Update();

      if (Agent.remainingDistance < 1.7f)
      {
         Agent.isStopped = true;
      //   NextStateMl = new Attack(Npc, Agent, Anim, Player);
         Stage = EVENT.Exit;
         Anim.SetFloat("Speed", 0);
      }
      
   }
   public override void Exit()
   {
      base.Exit();
   }
}

public class Attack_ML : State_ML
{
   public EnemyAttackScript_ML attackScript;
   public Attack_ML(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
      : base(npc, agent, anim, player)
   {
      Name = STATE.Attack;
      Anim.SetBool("CombatMode", true);
   }
   
   
   public override void Enter()
   {
      
      base.Enter();
   }
   public override void Update()
   {
      attackScript.TryAttack(Anim);
      
      if (Vector3.Distance(Player.position, Npc.transform.position) > 2)
      {
       //  NextStateMl = new Pursue(Npc, Agent, Anim, Player);
         Anim.SetBool("CombatMode", false);
         Stage = EVENT.Exit;
      }
      
      base.Update();
   }
   
   
   public override void Exit()
   {
      base.Exit();
   }
}