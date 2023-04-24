using SpaceShipProject.Contracts.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheProblemOfVerticalScalingAndSynchronization
{
    public class EventLoop
    {
        private readonly BlockingCollection<ICommand> m_queueCommand = new BlockingCollection<ICommand>();
        private readonly Thread m_thread;

        private Func<bool> m_funcToStop;

        public bool QueueIsEmpty { get { return m_queueCommand.Count == 0; } }

        public EventLoop()
        {
            m_thread = new Thread(ExecuteCommand);
            m_thread.Start();
        }

        private void ExecuteCommand()
        {
            SetFuncToStop(null);

            while (m_funcToStop())
            {
                ICommand cmd;

                if (!m_queueCommand.TryTake(out cmd))
                    continue;

                try
                {
                    cmd.Execute();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public void Put(ICommand command)
        {
            m_queueCommand.Add(command);
        }

        public void Wait()
        {
            m_thread.Join();
        }


        public void SetFuncToStop(Func<bool> funcToStop)
        {
            m_funcToStop = funcToStop ?? (() => { return true; });
        }

    }
}
