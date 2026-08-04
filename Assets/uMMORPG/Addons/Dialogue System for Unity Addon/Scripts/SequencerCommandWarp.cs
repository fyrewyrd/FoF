using UnityEngine;
using UnityEngine.AI;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// Syntax: Warp(destination, [subject])
    /// 
    /// Description: Warps a subject to a destination.
    /// 
    /// - destination: GameObject name.
    /// - subject: GameObject to warp. Default: speaker.
    /// </summary>
    public class SequencerCommandWarp : SequencerCommand
    {

        public void Start()
        {
            try
            {
                var destinationTransform = GetSubject(0);
                if (destinationTransform == null)
                {
                    if (DialogueDebug.logWarnings) Debug.LogWarning("Dialogue System: Sequencer: Warp(" + GetParameters() + "): Can't find destination " + GetParameter(0));
                }
                else
                {
                    var destination = destinationTransform.position;
                    var subject = GetSubject(1, speaker);
                    if (subject == null)
                    {
                        if (DialogueDebug.logWarnings) Debug.LogWarning("Dialogue System: Sequencer: Warp(" + GetParameters() + "): Can't find subject " + GetParameter(1));
                    }
                    else if (subject.GetComponent<Player>() != null)
                    {
                        if (DialogueDebug.logInfo) Debug.Log("Dialogue System: Sequencer: Warp(" + GetParameters() + "): Moving player " + subject.GetComponent<Player>() + " to " + destination);
                        subject.GetComponent<Player>().CmdWarp_DialogueSystem(destination);
                    }
                    else if (subject.GetComponent<Npc>() != null)
                    {
                        if (DialogueDebug.logInfo) Debug.Log("Dialogue System: Sequencer: Warp(" + GetParameters() + "): Moving NPC " + subject.GetComponent<Npc>() + " to " + destination);
                        subject.GetComponent<Npc>().CmdWarp_DialogueSystem(destination);
                    }
                    else if (subject.GetComponent<Monster>() != null)
                    {
                        if (DialogueDebug.logInfo) Debug.Log("Dialogue System: Sequencer: Warp(" + GetParameters() + "): Moving monster " + subject.GetComponent<Monster>() + " to " + destination);
                        subject.GetComponent<Monster>().CmdWarp_DialogueSystem(destination);
                    }
                    else if (subject.GetComponent<NavMeshAgent>() != null)
                    {
                        if (DialogueDebug.logInfo) Debug.Log("Dialogue System: Sequencer: Warp(" + GetParameters() + "): Moving local NavMeshAgent " + subject + " to " + destination);
                        subject.GetComponent<NavMeshAgent>().Warp(destination);
                    }
                    else if (subject.GetComponent<Rigidbody>() != null)
                    {
                        if (DialogueDebug.logInfo) Debug.Log("Dialogue System: Sequencer: Warp(" + GetParameters() + "): Moving local rigidbody " + subject + " to " + destination);
                        subject.GetComponent<Rigidbody>().MovePosition(destination);
                    }
                    else
                    {
                        if (DialogueDebug.logInfo) Debug.Log("Dialogue System: Sequencer: Warp(" + GetParameters() + "): Moving local " + subject + " to " + destination);
                        subject.position = destination;
                    }
                }
            }
            finally
            {
                Stop();
            }
        }
    }
}
