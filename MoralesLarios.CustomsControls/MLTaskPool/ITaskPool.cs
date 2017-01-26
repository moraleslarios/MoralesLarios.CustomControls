using System;
using System.ComponentModel;

namespace MoralesLarios.CustomsControls.MLTaskPool
{
    public interface ITaskPool
    {
        bool IsWorking { get; }
        Action NextActionToExecute { get; }
        Action ActionActualExecute { get; }

        void AddAction(Action action);

        event PropertyChangedEventHandler PropertyChanged;



    }
}