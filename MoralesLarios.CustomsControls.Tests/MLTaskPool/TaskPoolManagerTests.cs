using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoralesLarios.CustomsControls.MLTaskPool;
using System.Threading.Tasks;

namespace MoralesLarios.CustomsControls.Tests.MLTaskPool
{
    [TestClass]
    public class TaskPoolManagerTests
    {

        private ITaskPoolManager instance;


        [TestInitialize]
        public void Initialize()
        {
            instance = new TaskPoolManager();
        }




        [TestMethod]
        public void ChangeTask_actualTaskNull_OK()
        {
            Action actualTask = null;
            Action nextTask = () =>
            {

            };

            instance.ChangeTask(ref actualTask, ref nextTask);

            Assert.IsNotNull(actualTask);
            Assert.IsNull(nextTask);
        }


        [TestMethod]
        public void ChangeTask_actualTaskNotNull_OK()
        {
            Action actualTask = () =>
            {
                Console.Write("");
            };
            Action nextTask = () =>
            {

            };

            instance.ChangeTask(ref actualTask, ref nextTask);

            Assert.IsNotNull(actualTask);
            Assert.IsNull(nextTask);
        }


        [TestMethod]
        public async Task ExecuteTaskAsync_GoodTask_OK()
        {
            bool isExecuted = false;

            Action action =  () =>
            {
                isExecuted = true;
            };

            await instance.ExecuteTaskAsync(action);

            Assert.IsTrue(isExecuted);
        }





    }
}
