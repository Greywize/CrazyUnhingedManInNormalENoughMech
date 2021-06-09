﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Make the agent idle
    /// </summary>
    [CreateAssetMenu(menuName = "AI/AgentAction/IdleAction")]
    public class IdleAction : AgentAction
    {
        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
           
        }

        public override void performAction(AgentBehaviour agent)
        {
            agent.resetBehaviour(agent, 1.2f);
        }

        public override void Tick(AgentBehaviour agent, Condition cond)
        {
            throw new System.NotImplementedException();
        }

        public override void Tick(AgentBehaviour agent)
        {
            performAction(agent);
        }

        public override void addAction(AgentBehaviour agent, int index)
        {
            IdleAction act = ScriptableObject.Instantiate(this);
            //   agent.agentActions[index] = act;
        }

        public override AgentAction addinstance(AgentBehaviour agent)
        {
            IdleAction act = ScriptableObject.Instantiate(this);
            return act;
        }
    }
}