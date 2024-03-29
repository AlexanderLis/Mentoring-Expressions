﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample03.E3SClient.Entities;
using Sample03.E3SClient;
using System.Configuration;
using System.Linq;

namespace Sample03
{
	[TestClass]
	public class E3SProviderTests
	{
		[TestMethod]
		public void WithoutProvider()
		{
			var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"] , ConfigurationManager.AppSettings["password"]);
			var res = client.SearchFTS<EmployeeEntity>("workstation:(EPRUIZHW0249)", 0, 1);

			foreach (var emp in res)
			{
				Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
			}
		}

		[TestMethod]
		public void WithoutProviderNonGeneric()
		{
			var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
			var res = client.SearchFTS(typeof(EmployeeEntity), "workstation:(EPRUIZHW0249)", 0, 10);

			foreach (var emp in res.OfType<EmployeeEntity>())
			{
				Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
			}
		}


		[TestMethod]
		public void WithProvider()
		{
			var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation == "EPBYMINW5024"))
			{
				Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
			}
        }

        //Tests for tasks

        [TestMethod]
        public void RevertedWhereTest()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => "EPBYMINW5024" == e.workstation))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        [TestMethod]
        public void StartsWithTest()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.StartsWith("EPBYMINW5")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        [TestMethod]
        public void EndsWithTest()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.EndsWith("024")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        [TestMethod]
        public void ContainsTest()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.Contains("W5024")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        [TestMethod]
        public void AndOperatorTest()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.Contains("W502") && e.workstation.EndsWith("024")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }
	}
}
