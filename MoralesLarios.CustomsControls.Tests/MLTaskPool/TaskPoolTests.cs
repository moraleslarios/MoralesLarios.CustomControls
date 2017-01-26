using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using MoralesLarios.CustomsControls.MLTaskPool;
using Moq;

namespace MoralesLarios.CustomsControls.Tests.MLTaskPool
{


    [TestClass]
    public class TaskPoolTests
    {

        private ITaskPool instance;

        private ITaskPoolManager taskPoolManger;



        [TestInitialize]
        public void Initialize()
        {
            taskPoolManger = new TaskPoolManager();

            instance = new TaskPool(taskPoolManger);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddTask_NextActionToExecuteNull_ThrowException()
        {
            instance.AddAction(null);
        }


        [TestMethod]
        public void AddTask_NextActionToExecuteNull_TaskWithDelay_OK()
        {
            Action actionAdd = () => { System.Threading.Thread.Sleep(3000); };
            //Action actualAction = null;

            //mockTaskPoolManger.Setup(a => a.ChangeTask(ref actualAction, ref actionAdd));

            instance.AddAction(actionAdd);

            Assert.IsNull(instance.NextActionToExecute);
            Assert.AreEqual(actionAdd, instance.ActionActualExecute);
            Assert.IsNotNull(instance.ActionActualExecute);
            Assert.IsTrue(instance.IsWorking);
        }

        [TestMethod]
        public void AddTask_NextActionToExecuteNull_InitExecuted_OK()
        {
            bool isActionExecuted = false;

            Action action = () =>
            {
                //System.Threading.Thread.Sleep(2000);

                isActionExecuted = true;
            };

            instance.AddAction(action);

            Assert.IsNull(instance.NextActionToExecute);
            Assert.IsFalse(isActionExecuted);
            Assert.IsTrue(instance.IsWorking);
        }

        [TestMethod]
        public void AddTask_NextActionToExecuteNull_InitExecuted_2_OK()
        {
            bool isActionExecuted = false;

            Action action = () =>
            {
                //System.Threading.Thread.Sleep(1000);

                isActionExecuted = true;
            };

            instance.AddAction(action);

            System.Threading.Thread.Sleep(200);

            Assert.IsNull(instance.NextActionToExecute);
            Assert.IsTrue(isActionExecuted);
            Assert.IsFalse(instance.IsWorking);
        }



        [TestMethod]
        public void AddTask_NextActionToExecuteNull_ManyAddAction_OK()
        {
            bool isActionExecuted = false;

            int intResult = -1;

            Action action = () =>
            {
                //System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));

                System.Threading.Thread.Sleep(200);

                intResult++;

                isActionExecuted = false;
            };

            instance.AddAction(action);

            for (int i = 0; i < 5; i++)
            {
                instance.AddAction(() => { isActionExecuted = false; intResult++; });
            }

            instance.AddAction(() => { isActionExecuted = true; intResult++; });

            System.Threading.Thread.Sleep(300);

            //Assert.IsNull(instance.NextActionToExecute);
            //Assert.IsTrue(isActionExecuted);

            Assert.AreEqual(isActionExecuted, true);
            Assert.AreEqual(intResult, 1);
            Assert.IsFalse(instance.IsWorking);
        }


        [TestMethod]
        public void MyTestMethod()
        {
            var properties1 = typeof(System.IO.FileInfo).GetProperties();

            var properties2 = new System.IO.FileInfo(@"C:\database_166.xml").GetType().GetProperties();

            Assert.AreEqual(properties1.Length, properties2.Length);
        }


    }
}
