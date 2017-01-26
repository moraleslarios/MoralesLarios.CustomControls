using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoralesLarios.CustomsControls.HelpControls;

namespace MoralesLarios.CustomsControls.Tests.HelpControls
{
    [TestClass]
    public class FuncFilterFactoryTests
    {

        private IFuncFilterFactory instance;


        [TestInitialize]
        public void Initialize()
        {
            instance = new FuncFilterFactory();
        }




        [TestMethod]
        public void GetFuncFilter_StarWithIsKeySensitive_OK()
        {
            object str1 = "aaa";
            object str2 = "A";

            var result = instance.GetFuncFilter(FilterType.StarWith, isKeySensitive: true);

            bool resultFunc = result(str1, str2);

            Assert.IsFalse(resultFunc);
        }

        [TestMethod]
        public void GetFuncFilter_StarWith_OK()
        {
            object str1 = "aaa";
            object str2 = "A";

            var result = instance.GetFuncFilter(FilterType.StarWith, isKeySensitive: false);

            bool resultFunc = result(str1, str2);

            Assert.IsTrue(resultFunc);
        }




        [TestMethod]
        public void GetFuncFilter_StarWithIsKeySensitive_NotOK()
        {
            object str1 = "aaa";
            object str2 = "C";

            var result = instance.GetFuncFilter(FilterType.StarWith, isKeySensitive: true);

            bool resultFunc = result(str1, str2);

            Assert.IsFalse(resultFunc);
        }

        [TestMethod]
        public void GetFuncFilter_StarWith_NotOK()
        {
            object str1 = "aaa";
            object str2 = "C";

            var result = instance.GetFuncFilter(FilterType.StarWith, isKeySensitive: false);

            bool resultFunc = result(str1, str2);

            Assert.IsFalse(resultFunc);
        }




        [TestMethod]
        public void GetFuncFilter_EndWithIsKeySensitive_OK()
        {
            object str1 = "aaa";
            object str2 = "A";

            var result = instance.GetFuncFilter(FilterType.EndWith, isKeySensitive: true);

            bool resultFunc = result(str1, str2);

            Assert.IsFalse(resultFunc);
        }

        [TestMethod]
        public void GetFuncFilter_EndWith_OK()
        {
            object str1 = "aaa";
            object str2 = "A";

            var result = instance.GetFuncFilter(FilterType.EndWith, isKeySensitive: false);

            bool resultFunc = result(str1, str2);

            Assert.IsTrue(resultFunc);
        }


        [TestMethod]
        public void GetFuncFilter_ContainsIsKeySensitive_OK()
        {
            object str1 = "aaa";
            object str2 = "A";

            var result = instance.GetFuncFilter(FilterType.Contains, isKeySensitive: true);

            bool resultFunc = result(str1, str2);

            Assert.IsFalse(resultFunc);
        }

        [TestMethod]
        public void GetFuncFilter_Contains_OK()
        {
            object str1 = "aaa";
            object str2 = "A";

            var result = instance.GetFuncFilter(FilterType.Contains, isKeySensitive: false);

            bool resultFunc = result(str1, str2);

            Assert.IsTrue(resultFunc);
        }




        [TestMethod]
        public void GetFuncFilter_EqualsIsKeySensitive_OK()
        {
            object str1 = "a";
            object str2 = "A";

            var result = instance.GetFuncFilter(FilterType.Equals, isKeySensitive: true);

            bool resultFunc = result(str1, str2);

            Assert.IsFalse(resultFunc);
        }

        [TestMethod]
        public void GetFuncFilter_Equals_OK()
        {
            object str1 = "a";
            object str2 = "A";

            var result = instance.GetFuncFilter(FilterType.Equals, isKeySensitive: false);

            bool resultFunc = result(str1, str2);

            Assert.IsTrue(resultFunc);
        }


        [TestMethod]
        public void GetFuncFilter_Custom_OK()
        {
            instance = new FuncFilterFactory();
            instance.Custom = (a, b) => a.ToString()[1] == b.ToString()[1];

            object str1 = "abc";
            object str2 = "Abs";

            var result = instance.GetFuncFilter(FilterType.Custom, isKeySensitive: false);

            bool resultFunc = result(str1, str2);

            Assert.IsTrue(resultFunc);
        }


        [TestMethod]
        public void GetFuncFilter_Custom_NotOK()
        {
            instance = new FuncFilterFactory();
            instance.Custom = (a, b) => a.ToString()[1] == b.ToString()[1];

            object str1 = "abc";
            object str2 = "Ass";

            var result = instance.GetFuncFilter(FilterType.Custom, isKeySensitive: false);

            bool resultFunc = result(str1, str2);

            Assert.IsFalse(resultFunc);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetFuncFilter_CustomNull_ThrowException()
        {
            object str1 = "abc";
            object str2 = "Ass";

            var result = instance.GetFuncFilter(FilterType.Custom, isKeySensitive: false);
        }


    }
}
