using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salesforce;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public void TestQueryLeadFeed()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(
                "SELECT Body, Title, Type, InsertedById, LinkUrl, ParentId, RelatedRecordId, Id FROM LeadFeed");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.LeadFeed lead = (Salesforce.SalesforceProxy.LeadFeed)objects[i];
                Console.WriteLine("{0}\tId:\t{1}\nTitle:\t{2}\nType:\t{3}\nInsertedById:\t{4}\nLinkUrl:\t{5}\nParentId:\t{6}\nRelatedRecordId:\t{7}\nBody:\t{8}\n", 
                    i + 1, lead.Id, lead.Title, lead.Type, lead.InsertedById, lead.LinkUrl, lead.ParentId, lead.RelatedRecordId, lead.Body);
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
        public void TestQueryFeedAttachments()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query("SELECT Title, Value, Id, Type, RecordId, FeedEntityId FROM FeedAttachment");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.FeedAttachment att = (Salesforce.SalesforceProxy.FeedAttachment)objects[i];
                Console.WriteLine("{0}\t{1}\nValue:\t{2}\nId:\t{3}\nType:\t{4}\nRecordId:{5}\nFeedEntityId:{6}\n", 
                    i + 1, att.Title, att.Value, att.Id, att.Type, att.RecordId, att.FeedEntityId);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryFeedItem()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(
                "SELECT Title, Body, Id, Type, HasContent, HasFeedEntity, ParentId, RelatedRecordId FROM FeedItem");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.FeedItem att = (Salesforce.SalesforceProxy.FeedItem)objects[i];
                Console.WriteLine(
                    "{0}\t{1}\nBody:\t{2}\nId:\t{3}\nType:\t{4}\nHasContent:{5}\nHasFeedEntity:\t{6}\nParentId:\t{7}\nRelatedRecordId:\t{8}", 
                    i + 1, att.Title, att.Body, att.Id, att.Type, att.HasContent, att.HasFeedEntity, att.ParentId, att.RelatedRecordId);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryAttachments()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query("SELECT Name, Description, Id, ParentId, OwnerId FROM Attachment");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.Attachment att = (Salesforce.SalesforceProxy.Attachment)objects[i];
                Console.WriteLine("{0}\t{1}\nDescription:\t{2}\nId:\t{3}\nParentId:\t{4}\nOwnerId:{5}\n", i + 1, att.Name, att.Description, att.Id, att.ParentId, att.OwnerId);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryContentAsset()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(
                "SELECT MasterLabel, DeveloperName, Id, CreatedDate, CreatedById, ContentDocumentId FROM ContentAsset");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.ContentAsset att = (Salesforce.SalesforceProxy.ContentAsset)objects[i];
                Console.WriteLine("{0}\t{1}\nMasterLabel:\t{2}\nId:\t{3}\nCreatedDate:\t{4}\nCreatedById:{5}\n",
                    i + 1, att.ContentDocumentId, att.MasterLabel, att.Id, att.CreatedDate, att.CreatedById);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryContentDocument()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(
                "SELECT Title, Description, Id, ParentId, OwnerId, ContentAssetId, ContentSize, LatestPublishedVersionId FROM ContentDocument");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.ContentDocument att = (Salesforce.SalesforceProxy.ContentDocument)objects[i];
                Console.WriteLine("{0}\t{1}\nDescription:\t{2}\nId:\t{3}\nParentId:\t{4}\nOwnerId:{5}\nContentAssetId:\t{6}\nContentSize:\t{7}\nLatestPublishedVersionId:\t{8}", 
                    i + 1, att.Title, att.Description, att.Id, att.ParentId, att.OwnerId, att.ContentAssetId, att.ContentSize, att.LatestPublishedVersionId);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryDocument()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(
                "SELECT Name, Description, Id, BodyLength FROM Document");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.Document att = (Salesforce.SalesforceProxy.Document)objects[i];
                Console.WriteLine("{0}\t{1}\nDescription:\t{2}\nId:\t{3}\nBodyLength:\t{4}\n",
                    i + 1, att.Name, att.Description, att.Id, att.BodyLength);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryFolder()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(
                "SELECT Name, Id, AccessType, IsReadonly, Type, ParentId, DeveloperName, CreatedById FROM Folder");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.Folder att = (Salesforce.SalesforceProxy.Folder)objects[i];
                Console.WriteLine("{0}\t{1}\tId:\t{2}\nAccessType:\t{3}\nIsReadonly:\t{4}\nType:\t{5}\nParentId:\t{6}\nDeveloperName:\t{7}\nCreatedById:\t{8}\n",
                    i + 1, att.Name, att.Id, att.AccessType, att.IsReadonly, att.Type, att.ParentId, att.DeveloperName, att.CreatedById);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryContentVersion()
        {
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(
                "SELECT Title, Description, Id, OwnerId, ContentSize, ContentBodyId FROM ContentVersion");

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.ContentVersion att = (Salesforce.SalesforceProxy.ContentVersion)objects[i];
                Console.WriteLine("{0}\t{1}\nDescription:\t{2}\nId:\t{3}\nOwnerId:{4}\nContentSize:\t{5}\nContentBodyId:{6}",
                    i + 1, att.Title, att.Description, att.Id,att.OwnerId, att.ContentSize, att.ContentBodyId);
            }
        }

        [TestMethod]
        [TestCategory("Query")]
        public void TestQueryLeadByName()
        {
            string soqlQuery = "SELECT" +
                " Name,Id,ConvertedAccountId,MasterRecordId" +
                " FROM Lead WHERE FirstName = 'LeadFN' AND LastName='LeadLN'";
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(soqlQuery);

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.Lead lead = (Salesforce.SalesforceProxy.Lead)objects[i];
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", i + 1, lead.Name, lead.Id, lead.ConvertedAccountId, lead.MasterRecordId);
            }
        }
        #endregion

        #region TestCategory("Retrieve")
        [TestMethod]
        [TestCategory("Retrieve")]
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
        [TestCategory("Retrieve")]
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
        [TestCategory("Retrieve")]
        public void TestRetrieveAttachmentById()
        {
            string fields = "Name, Id, ContentType, CreatedById, CreatedDate, IsDeleted, IsPrivate, OwnerId, ParentId";
            string[] ids = { "00P0b00000tZuVoEAK" };
            Salesforce.SalesforceProxy.sObject[] objects = sf.Retrieve(fields, "Attachment", ids);

            for (int i = 0; i < objects.Length; i++)
            {
                Salesforce.SalesforceProxy.Attachment att = (Salesforce.SalesforceProxy.Attachment)objects[i];
                Console.WriteLine("{0}\nName:\t{1}\nId:\t{2}\nContentType:\t{3}\nCreated:\t{4}\nCreatedBy:\t{5}\nIsDeleted:\t{6}\nIsPrivate:\t{7}" +
                    "\nOwner:\t{8}\nParent:\t{9}", 
                    i + 1, att.Name, att.Id,
                    att.ContentType, att.CreatedById, att.CreatedDate, att.IsDeleted, att.IsPrivate,
                    att.OwnerId, att.ParentId);
            }
        }
        #endregion

        [TestMethod]
        public void TestGetAttachment()
        {
            string soqlQuery = "SELECT  Name, Body, ContentType from Attachment" +
                " where OwnerId = '0050b0000032fxTAAQ'";
            Salesforce.SalesforceProxy.sObject[] objects = sf.Query(soqlQuery);
            Salesforce.SalesforceProxy.Attachment att = (Salesforce.SalesforceProxy.Attachment)objects[0];
            byte[] body = att.Body;
            Console.WriteLine(att.Name);
        }

        #region TestCategory("Create")
        [TestMethod]
        [TestCategory("Create")]
        public void TestCreateFeedItem() // "0D50b00004tPIGgCAO"
        {
            Salesforce.SalesforceProxy.FeedItem att = new Salesforce.SalesforceProxy.FeedItem()
            {
                Body = "Create Feed Item from code with existing feed attachment",
                Type = "ContentPost",
                HasContent= true,
                ParentId = "00Q0b00001XoqW5EAJ", // Lead ID - LeadFN1 LeadLN1
                RelatedRecordId = "0680b0000038iyHAAQ" // existing Feed Attachment created by hand on LeadFN
            };

            Salesforce.SalesforceProxy.SaveResult result = sf.Create(att);
        }

        [TestMethod]
        [TestCategory("Create")]
        public void TestCreateFeedAttachment() // ERROR - need a ContentVersion based on a ContentDocument which is not createable :(((((
        {
            Salesforce.SalesforceProxy.FeedAttachment att = new Salesforce.SalesforceProxy.FeedAttachment()
            {
                Type = "Content",
                RecordId = "00P0b00000tZuVoEAK" // existing Attachment created by API
            };

            Salesforce.SalesforceProxy.SaveResult result = sf.Create(att);
        }

        [TestMethod]
        [TestCategory("Create")]
        public void TestCreateAttachment() // "00P0b00000tZuVoEAK"
        {
            Salesforce.SalesforceProxy.Attachment att = new Salesforce.SalesforceProxy.Attachment()
            {
                Body = Encoding.UTF8.GetBytes("abcde"),
                Description = "API-uploaded attachment",
                Name = "TestFromCode",
                OwnerId = "0050b0000032fxTAAQ", // User ID - Mihaela Armanasu
                ParentId = "00Q0b00001XoqUnEAJ" // Lead ID - LeadFN LeadLN
            };

            Salesforce.SalesforceProxy.SaveResult result = sf.Create(att);
        }

        [TestMethod]
        [TestCategory("Create")]
        // same as before, but set the Salesforce Files Settings: "Files uploaded to the Attachments related list on records are uploaded as Salesforce Files, not as attachments"
        public void TestCreateAttachmentAsFile() // "00P0b00000tZyBQEA0"
        {
            Salesforce.SalesforceProxy.Attachment att = new Salesforce.SalesforceProxy.Attachment()
            {
                Body = Encoding.UTF8.GetBytes("abcde"),
                Description = "API-uploaded attachment as file",
                Name = "TestAttAsFileFromCode",
                OwnerId = "0050b0000032fxTAAQ", // User ID - Mihaela Armanasu
                ParentId = "00Q0b00001XoqUnEAJ" // Lead ID - LeadFN LeadLN
            };

            Salesforce.SalesforceProxy.SaveResult result = sf.Create(att);
        }

        [TestMethod]
        [TestCategory("Create")]
        public void TestCreateContentDocument() // Not Allowed
        {
            Salesforce.SalesforceProxy.ContentDocument att = new Salesforce.SalesforceProxy.ContentDocument()
            {
               // Body = Encoding.UTF8.GetBytes("abcde"),
                Description = "API-create Content Document",
                Title = "TestContentDocumentFromCode",
                OwnerId = "0050b0000032fxTAAQ", // User ID - Mihaela Armanasu
                ParentId = "00Q0b00001XoqUnEAJ" // Lead ID - LeadFN LeadLN
            };

            Salesforce.SalesforceProxy.SaveResult result = sf.Create(att);
        }

        [TestMethod]
        [TestCategory("Create")]
        public void TestCreateContentVersion() // ERROR - "One of these fields must be set: PathOnClient, ContentUrl."
        {
            Salesforce.SalesforceProxy.ContentVersion att = new Salesforce.SalesforceProxy.ContentVersion()
            {
                VersionData = Encoding.UTF8.GetBytes("abcde"),
                Description = "API-create Content Version",
                Title = "TestContentVersionFromCode",
                OwnerId = "0050b0000032fxTAAQ", // User ID - Mihaela Armanasu
            };

            Salesforce.SalesforceProxy.SaveResult result = sf.Create(att);
        }

        [TestMethod]
        [TestCategory("Create")]
        public void TestCreateFolder() // "00l0b000001u51dAAA"
        {
            Salesforce.SalesforceProxy.Folder att = new Salesforce.SalesforceProxy.Folder()
            {
                Name = "TestFolderFromCode",
                DeveloperName = "MihaelaA",
                IsReadonly = false,
                AccessType = "Public",
                Type = "Document"
            };

            Salesforce.SalesforceProxy.SaveResult result = sf.Create(att);
        }

        [TestMethod]
        [TestCategory("Create")]
        public void TestCreateDocument() // "0150b000001gZ9NAAU" "0150b000001gZ9SAAU"
        {
            Salesforce.SalesforceProxy.Document att = new Salesforce.SalesforceProxy.Document()
            {
                Body = Encoding.UTF8.GetBytes("abcde"),
                Description = "API-create Document",
                Name = "TestDocumentFromCode",
                AuthorId = "0050b0000032fxTAAQ", // User ID - Mihaela Armanasu
                IsPublic = true,
                FolderId = "00l0b000001u51sAAA"
            };

            Salesforce.SalesforceProxy.SaveResult result = sf.Create(att);
        }
        #endregion

        #region TestCategory("Describe")
        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeContentAsset()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("ContentAsset");
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeContentBody()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("ContentBody");
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeContentVersion()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("ContentVersion");
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeContentDocument()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("ContentDocument");
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeDocument()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("Document");
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeAttachment()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("Attachment");
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeFeedAttachment()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("FeedAttachment");
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeChatterActivity()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("ChatterActivity");
            Console.WriteLine("createable = {0}", result.createable);
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
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeFolder()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("Folder");
            Console.WriteLine("createable = {0}", result.createable);
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
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i+1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeLeadFeed()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("LeadFeed");
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeFeedItem()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("FeedItem");
            Console.WriteLine("createable = {0}", result.createable);
            for (int i = 0; i < result.fields.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i + 1, result.fields[i].name);
            }
        }

        [TestMethod]
        [TestCategory("Describe")]
        public void TestDescribeLeadCleanInfo()
        {
            Salesforce.SalesforceProxy.DescribeSObjectResult result = sf.DescribeSObject("LeadCleanInfo");
            Console.WriteLine("createable = {0}", result.createable);
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
            Console.WriteLine("createable = {0}", result.createable);
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
            Console.WriteLine("createable = {0}", result.createable);
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
            Console.WriteLine("createable = {0}", result.createable);
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
                Console.WriteLine("{0}\t{1}\t{2}", i + 1, result.sobjects[i].name, result.sobjects[i].createable);
            }
        }
        #endregion
    }
}
