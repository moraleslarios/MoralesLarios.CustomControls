using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoralesLarios.CustomsControls.Tests.TEstGenerateTypes;
using System.Linq;
using System.Collections.Generic;
using MoralesLarios.CustomsControls.Extensions;

namespace MoralesLarios.CustomsControls.Tests
{
    [TestClass]
    public class ExtensionsTests
    {
        // coment.

        public IEnumerable<Customer> customers;



        [TestInitialize]
        public void Initialize()
        {
            customers = Customer.GetData().ToList();
        }














        [TestMethod]
        public void WhereAllFunc_WithCont_OK()
        {

            var result = customers.WhereAllForFunc("i", (a, b) => a.ToString().Contains(b.ToString()), 2);

            Assert.AreEqual(2, result.Count());
        }


        [TestMethod]
        public void WhereAllFunc_WithCont_WithExceptColumns_OK()
        {
            var columnsExcept = typeof(Customer).GetProperties().Select(a => a.Name).Except(new string[] { "City" }).ToArray();

            var result = customers.WhereAllForFunc("i", (a, b) => a.ToString().Contains(b.ToString()), columnsExcept);

            Assert.AreEqual(4, result.Count());
        }


        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Select_StrPropertyNameNotExist_ThrowException()
        //{
        //    string strProperty = "NOT_EXISTS";

        //    var result = customers.Select(strProperty);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void Select_StrPropertyNameNull_ThrowException()
        //{
        //    string strProperty = null;

        //    var result = customers.Select(strProperty);
        //}




        [TestMethod]
        public void Select_GooParameter_OK()
        {
            string strProperty = "Name";

            var result = customers.Select(strProperty);

            Assert.AreEqual(customers.Count(), result.Count());

            for (int i = 0; i < result.Count(); i++)
            {
                Assert.AreEqual(customers.ElementAt(i).Name, result.ElementAt(i));
            }

        }




        [TestMethod]
        public void GetExceptionsFieldsByAceptFields_GoodStrFieldName_OK()
        {
            var result = Extensions.Extensions.GetExceptionsFieldsByAceptFields<Customer>(customers,  "Name" );

            Assert.AreEqual(3, result.Count());

        }

        [TestMethod]
        public void GetExceptionsFieldsByAceptFields_FieldsWithNulls_OK()
        {
            var result = Extensions.Extensions.GetExceptionsFieldsByAceptFields<Customer>(customers, strFieldsName:  null );

            Assert.AreEqual(0, result.Count());

        }

        [TestMethod]
        public void GetExceptionsFieldsByAceptFields_FieldsNamesWithNulls_OK()
        {
            var result = Extensions.Extensions.GetExceptionsFieldsByAceptFields<Customer>(customers, strFieldsNames: null);

            Assert.AreEqual(0, result.Count());

        }


        [TestMethod]
        public void GetExceptionsFieldsByAceptFields_FieldsNamesWithNotNulls_OK()
        {
            IEnumerable<string> fields = new string[] { "ID", "Sales" };

            var result = Extensions.Extensions.GetExceptionsFieldsByAceptFields<Customer>(customers, strFieldsNames: fields);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Name", result.ElementAt(0));
            Assert.AreEqual("City", result.ElementAt(1));
        }



        [TestMethod]
        public void GetPropertiesObj_IEnumerableGeneric_OK()
        {
            var expected = typeof(Customer).GetProperties();

            var result = Extensions.Extensions.GetPropertiesObj<Customer>(customers);

            Assert.AreEqual(expected.Count(), result.Count());
        }

        [TestMethod]
        public void GetPropertiesObj_IEnumerableObject_OK()
        {
            var expected = typeof(Customer).GetProperties();

            var result = Extensions.Extensions.GetPropertiesObj<object>(customers);

            Assert.AreEqual(expected.Count(), result.Count());
        }

        [TestMethod]
        public void GetPropertiesObj_IEnumerableObjectEmpty_OK()
        {
            var customers = Enumerable.Empty<Customer>();

            var expected = typeof(Customer).GetProperties();

            var result = Extensions.Extensions.GetPropertiesObj<object>(customers);

            Assert.AreEqual(0, result.Count());
        }


        [TestMethod]
        public void WhereAllForFuncInOrder_GoodParams_OK()
        {
            string objSearch = "i";
            Func<object, object, bool> comp = (a, b) => a?.ToString().Contains(b?.ToString()) ?? false;
            int count = 5;
            IEnumerable<string> orderStrFieldNames = new string[] { "Name", "City" };

            var expected = new string[] { "Philips", "Pionner", "Sony Music", "Pepsi", "Madrid" };

            var result = Extensions.Extensions.WhereAllForFuncInOrder(customers, objSearch, comp, count, orderStrFieldNames);

            Assert.AreEqual(count, result.Count());
            Assert.AreEqual(expected.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(expected.ElementAt(1), result.ElementAt(1));
            Assert.AreEqual(expected.ElementAt(2), result.ElementAt(2));
            Assert.AreEqual(expected.ElementAt(3), result.ElementAt(3));
            Assert.AreEqual(expected.ElementAt(4), result.ElementAt(4));
        }


        [TestMethod]
        public void WhereAllForFuncInFields_GoodParameters_OK()
        {
            string objSearch = "i";
            Func<object, object, bool> comp = (a, b) => a?.ToString().Contains(b?.ToString()) ?? false;
            string[] fieldSearchs = new string[] { "Name", "City" };

            var result = Extensions.Extensions.WhereAllForFuncInFields(customers, objSearch, comp, fieldSearchs);

            Assert.AreEqual(6, result.Count());
        }



    }
}
