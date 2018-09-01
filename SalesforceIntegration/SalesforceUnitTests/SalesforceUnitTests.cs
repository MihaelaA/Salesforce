using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salesforce;
using System.Collections.Generic;
using System.Linq;

namespace SalesforceUnitTests
{
    [TestClass]
    public class SalesforceUnitTests
    {
        private static Salesforce.SalesforceIntegration sf;

        [ClassInitialize]
        static public void Initialize(TestContext context)
        {
            try
            {
                // Create the Salesforce connection 
                sf = new Salesforce.SalesforceIntegration(
                    "", // Fill in Username
                    "", // Fill in Password
                    "" // Fill in Token,
                    );

                sf.Login();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Initialize error: {0}", ex.Message);
            }
        }

        [ClassCleanup]
        static public void Cleanup()
        {
            try
            {
                sf.Logout();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cleanup error: {0}", ex.Message);
            }
        }

        #region TestCategory("Query")
        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryContacts()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query("SELECT Name, Id FROM Contact");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.Contact contact = (Salesforce.SalesforceProxy.Contact)objects[i];
                Console.WriteLine("{0}\t{1}\t{2}", i+1, contact.Name, contact.Id);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryLeads()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query("SELECT Name, Id FROM Lead");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.Lead lead = (Salesforce.SalesforceProxy.Lead)objects[i];
                Console.WriteLine("{0}\t{1}\t{2}", i + 1, lead.Name, lead.Id);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryUsers()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query("SELECT Name, Id FROM User");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.User user = (Salesforce.SalesforceProxy.User)objects[i];
                Console.WriteLine("{0}\t{1}\t{2}", i + 1, user.Name, user.Id);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryChatterActivity()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query("SELECT Id, ParentId FROM ChatterActivity");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.ChatterActivity chatter = (Salesforce.SalesforceProxy.ChatterActivity)objects[i];
                Console.WriteLine("{0}\t{1}\t{2}", i + 1, chatter.Id, chatter.ParentId);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryLeadById()
        {
            string soqlQuery = "SELECT" +
                " Name,Id,ConvertedAccountId,MasterRecordId" +
                " FROM Lead WHERE Id='00Q0b00001XoqUnEAJ'";
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(soqlQuery);

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.Lead lead = (Salesforce.SalesforceProxy.Lead)objects[i];
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", i + 1, lead.Name, lead.Id, lead.ConvertedAccountId, lead.MasterRecordId);
            }
        }
        #endregion

        [TestMethod]
        public void TestRetrieveLeadById()
        {
            string fields = "Name,Id,ConvertedAccountId,MasterRecordId";
            string[] ids = { "00Q0b00001XoqUnEAJ" };
            Salesforce.SalesforceProxy.sObject[] objects = sf.Retrieve(fields, "Lead", ids);

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.Lead lead = (Salesforce.SalesforceProxy.Lead)objects[i];
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", i + 1, lead.Name, lead.Id, lead.ConvertedAccountId, lead.MasterRecordId);
            }
        }

        [TestMethod]
        public void TestRetrieveUserById()
        {
            string fields = "Name,Id";
            string[] ids = { "0050b0000032fxTAAQ" };
            Salesforce.SalesforceProxy.sObject[] objects = sf.Retrieve(fields, "User", ids);

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.User user = (Salesforce.SalesforceProxy.User)objects[i];
                Console.WriteLine("{0}\t{1}\t{2}", i + 1, user.Name, user.Id);
            }
        }

        [TestMethod]
        public void TestGetAttachment()
        {
            string soqlQuery = "SELECT  Name, Body, ContentType from Attachment" +
                " where ParentId = '0050b0000032fxTAAQ'";
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(soqlQuery);
            Salesforce.SalesforceProxy.Attachment att = (Salesforce.SalesforceProxy.Attachment)objects[0];
            byte[] body = att.Body;
            Console.WriteLine(att.Name);
        }

        #region TestCategory("Describe")
        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeChatterActivity()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("ChatterActivity");

            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeUser()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("User");

            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeLead()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("Lead");

            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i+1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeLeadCleanInfo()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("LeadCleanInfo");

            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeLeadFeed()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("LeadFeed");

            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeLeadHistory()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("LeadHistory");

            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]    
        public void TestDescribeLeadShare()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("LeadShare");

            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeLeadStatus()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("LeadStatus");

            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeGlobal()
        {
            Salesforce.SalesforceProxy.DescribeGlobalResult result = sf.DescribeGlobal();

            for (int i = 0; i < result.sobjects.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.sobjects[i].name);
            }
        }
        #endregion
    }
}
