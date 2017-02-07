using MoralesLarios.CustomsControls.Exceptions;
using MoralesLarios.CustomsControls.Helpers;
using System;
using System.Threading.Tasks;

namespace MoralesLarios.CustomsControls.MLTaskPool
{
    public class TaskPool : ObservableObject, ITaskPool
    {

        private bool _isWorking = false;

        public bool IsWorking
        {
            get { return _isWorking; }
            private set
            {
                _isWorking = value;

                OnPropertyChanged();
            }
        }




        private Action _nextActionToExecute;

        public Action NextActionToExecute
        {
            get { return _nextActionToExecute; }
            private set
            {
                _nextActionToExecute = value;

                OnPropertyChanged();
            }
        }

        private Action _actionActualExecute;
        public Action ActionActualExecute
        {
            get { return _actionActualExecute; }
            private set
            {
                _actionActualExecute = value;

                OnPropertyChanged();
            }
        }


        private ITaskPoolManager taskPoolManager;

        public TaskPool(ITaskPoolManager taskPoolManager)
        {
            this.taskPoolManager = taskPoolManager;
        }
        
        public void AddAction(Action action)
        {
            try
            {
                if (action == null) throw new ArgumentNullException(nameof(action), $"The parameter action can't be null");

                if (NextActionToExecute == null && ActionActualExecute == null)
                {
                    NextActionToExecute = action;
                    InitExecuted();
                }
                else
                {
                    NextActionToExecute = action;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
        }




        private async void InitExecuted()
        {
            IsWorking = true;

            try
            {
                while (NextActionToExecute != null)
                {
                    taskPoolManager.ChangeTask(ref _actionActualExecute, ref _nextActionToExecute);

                    await taskPoolManager.ExecuteTaskAsync(ActionActualExecute);

                    ActionActualExecute = null;
                }
            }
            catch (PropertyNotSupportedException ex)
            {
                throw ex;
            }
            finally
            {
                IsWorking = false;
            }

            
        }


    }
}