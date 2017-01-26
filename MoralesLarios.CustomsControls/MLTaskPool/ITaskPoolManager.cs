using System;
using System.Threading.Tasks;

namespace MoralesLarios.CustomsControls.MLTaskPool
{
    public interface ITaskPoolManager
    {
        void ChangeTask(ref Action actualTask, ref Action nextTask);
        Task ExecuteTaskAsync(Action action);
    }
}