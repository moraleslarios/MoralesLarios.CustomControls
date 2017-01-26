using System;
using System.Threading.Tasks;

namespace MoralesLarios.CustomsControls.MLTaskPool
{
    public class TaskPoolManager : ITaskPoolManager
    {
        public void ChangeTask(ref Action actualTask, ref Action nextTask)
        {
            actualTask = nextTask;
            nextTask   = null;
        }

        public Task ExecuteTaskAsync(Action action)
        {
            System.Diagnostics.Debug.Print($"Ejecutando tarea {action.GetHashCode()}");

            return Task.Run(action);
        }
    }
}