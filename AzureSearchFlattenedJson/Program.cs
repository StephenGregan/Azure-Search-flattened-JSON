using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureSearchFlattenedJson
{
    class Program
    {
        static string searchServiceName = "";     // Learn more here: https://azure.microsoft.com/en-us/documentation/articles/search-what-is-azure-search/
        static string searchServiceAPIKey = "";
        static string indexName = "contacts";
        static SearchServiceClient serviceClient;
        static ISearchIndexClient indexClient;

        static void Main(string[] args)
        {
            // This will create an Azure Search index, load a complex JSON data file 
            // and perform some search queries

            if ((searchServiceName == "") || (searchServiceAPIKey == ""))
            {
                Console.WriteLine("Please add your searchServiceName and searchServiceAPIKey.  Press any key to continue.");
                Console.ReadLine();
                return;
            }

            serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(searchServiceAPIKey));
            indexClient = serviceClient.Indexes.GetClient(indexName);


            Console.WriteLine("Creating index...");
            ReCreateIndex();
            Console.WriteLine("Uploading documents...");
            UploadDocuments();
            Console.WriteLine("Waiting 5 seconds for content to be indexed...");
            Thread.Sleep(5000);
            Console.WriteLine("\nFinding a contact with an active status.....");
            ContactSearch results = SearchDocuments(searchText : "*", filter : "active eq 'Y')");
            //Console.WriteLine("\nFinding all people who work at the ‘Adventureworks Headquarters’...");
            //ContactSearch results = SearchDocuments(searchText: "*", filter: "locationsDescription / any(t: t eq 'Adventureworks Headquarters')");
            //Console.WriteLine("Found matches:");
            //foreach (var contact in results.Results)
            //{
            //    Console.WriteLine("- {0}", contact.Document["name"]);
            //}

            //Console.WriteLine("\nGetting a count of the number of people who work in a ‘Home Office’...");
            //results = SearchDocuments(searchText: "*", filter: "locationsDescription / any(t: t eq 'Home Office')");
            //Console.WriteLine("{0} people have Home Offices", results.Count);

            //Console.WriteLine("\nOf the people who at a ‘Home Office’ show what other offices they work in with a count of the people in each location...");
            //var locationsDescription = results.Facets.Where(item => item.Key == "locationsDescription");
            //Console.WriteLine("Found matches:");
            //foreach (var facets in locationsDescription)
            //{
            //    foreach (var facet in facets.Value)
            //    {
            //        Console.WriteLine("- Location: {0} ({1})", facet.Value, facet.Count);
            //    }
            //}

            //Console.WriteLine("\nGetting a count of people who work at a ‘Home Office’ with location Id of ‘4’...");
            //results = SearchDocuments(searchText: "*", filter: "locationsCombined / any(t: t eq '4||Home Office')");
            //Console.WriteLine("{0} people have Home Offices with Location Id '4'", results.Count);

            //Console.WriteLine("\nGetting people that have Home Offices with Location Id '4':");
            //Console.WriteLine("Found matches:");
            //foreach (var contact in results.Results)
            //{
            //    Console.WriteLine("- {0}", contact.Document["name"]);
            //}

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }

        public static void ReCreateIndex()
        {
            // Delete and re-create the index
            if (serviceClient.Indexes.Exists(indexName))
                serviceClient.Indexes.Delete(indexName);

            var definition = new Index()
            {
                Name = indexName,
                Fields = new[]
                {
                    //new Field("page", DataType.String)                                                  { IsKey = true },
                    //new Field("total", DataType.Int32)                                                  { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true },
                    //new Field("records", DataType.String)                                               { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true },

                   
                    //TEST
                    //new Field("id", DataType.String)                                                    { IsKey = true},
                    //new Field("versionValue", DataType.Int32)                                           { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("uuid", DataType.String)                                                  { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("createdBy", DataType.String)                                             { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("createdDate", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("lastModifiedBy", DataType.String)                                        { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("lastModifiedDate", DataType.String)                                      { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("companyId", DataType.Int32)                                              { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("name", DataType.String)                                                  { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("displayName", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("salutation", DataType.String)                                            { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("firstName", DataType.String)                                             { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("middleName", DataType.String)                                            { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("lastName", DataType.String)                                              { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("nickName", DataType.String)                                              { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("suffix", DataType.String)                                                { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("genderId", DataType.Int32)                                               { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("businessUnitId", DataType.String)                                        { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("dateOfBirth", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("accountingReference", DataType.String)                                   { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("referenceId", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("label", DataType.String)                                                 { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("description", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("alternatives", DataType.String)                                          { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("value", DataType.String)                                                 { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("subtag", DataType.String)                                                { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("iso639_3Tag", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("type", DataType.String)                                                  { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("alias", DataType.String)                                                 { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("enabled", DataType.Boolean)                                              { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("rating", DataType.String)                                                { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("parsedNumber", DataType.String)                                          { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("addrEntered", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("addrFormatted", DataType.String)                                         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("cityTown", DataType.String)                                              { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("stateCounty", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("postalCode", DataType.String)                                            { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("active", DataType.String)                                                { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("referenceId", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("id", DataType.String)                                                    { IsKey = true},
                    //new Field("versionValue", DataType.Int32)                                           { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("uuid", DataType.String)                                                  { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("createdBy", DataType.String)                                             { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("createdDate", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("lastModifiedBy", DataType.String)                                        { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("lastModifiedDate", DataType.String)                                      { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("companyId", DataType.Int32)                                              { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("name", DataType.String)                                                  { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("displayName", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("salutation", DataType.String)                                            { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("firstName", DataType.String)                                             { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("middleName", DataType.String)                                            { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("lastName", DataType.String)                                              { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("nickName", DataType.String)                                              { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("suffix", DataType.String)                                                { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("genderId", DataType.Int32)                                               { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("businessUnitId", DataType.String)                                        { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("dateOfBirth", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("accountingReference", DataType.String)                                   { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("referenceId", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingId", DataType.Collection(DataType.String))                { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingContactId", DataType.Collection(DataType.String))         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageId", DataType.Collection(DataType.String))        { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingCombined", DataType.Collection(DataType.String))          { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageObjId", DataType.Collection(DataType.String))     { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    ////new Field("language", DataType.String)                                                 { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageObjLabel", DataType.Collection(DataType.String))         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageObjDescription", DataType.Collection(DataType.String))         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageObjAlternates", DataType.Collection(DataType.String))         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageObjValue", DataType.Collection(DataType.String))         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageObjSubtag", DataType.Collection(DataType.String))         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageObjIso639_3Tag", DataType.Collection(DataType.String))         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageObjType", DataType.Collection(DataType.String))         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageObjAlias", DataType.Collection(DataType.String))         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("languageMappingLanguageObjEnabled", DataType.Collection(DataType.String))         { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },

                    //new Field("contactId", DataType.Collection(DataType.String))                        { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("contactId", DataType.Collection(DataType.String))                        { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("contactId", DataType.Collection(DataType.String))                        { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("contactId", DataType.Collection(DataType.String))                        { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                     //new Field("contactId", DataType.String)                                           { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("total", DataType.Int64)                                          { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false }
                    //new Field("page", DataType.String)                                          { IsKey = true },
                    //new Field("total", DataType.Int64)                                          { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("records", DataType.Int64)                                        { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("rows0Id", DataType.Int64)                                        { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("rows1Id", DataType.Int64)                                        { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false },
                     
                    //new Field("rows0versionValue", DataType.Int64)                                        { IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true },
                    //new Field("rows0uuid", DataType.String)                                               { IsKey = true, IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true },
                    //new Field("rows0createdBy", DataType.String)                                          { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true },
                    //new Field("rows0createdDate", DataType.String)                                        { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true },
                     new Field("id", DataType.String)                                                     { IsKey = true, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true },
                    //new Field("rows", DataType.Collection(DataType.String))                               { IsKey = false, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true },
                     new Field("rowsid", DataType.String)                                                     { IsKey = false, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true },
                     new Field("versionValue", DataType.Int32)                                                     { IsKey = false, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true },
                     new Field("name", DataType.String)                                                     { IsKey = false, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true }



                    //new Field("id", DataType.String)        { IsKey = true },
                    //new Field("name", DataType.String)      { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("company", DataType.String)   { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    //new Field("locationsId", DataType.Collection(DataType.String))
                    //    { IsSearchable = true, IsFilterable = true,  IsFacetable = true },
                    //new Field("locationsDescription", DataType.Collection(DataType.String))
                    //    { IsSearchable = true, IsFilterable = true,  IsFacetable = true },
                    //new Field("locationsCombined", DataType.Collection(DataType.String))
                    //    { IsSearchable = true, IsFilterable = true,  IsFacetable = true }
                }
            };

            serviceClient.Indexes.Create(definition);
        }

        public static void UploadDocuments()
        {
            // This will open the JSON file, parse it and upload the documents in a batch
            List<IndexAction> indexOperations = new List<IndexAction>();
            //JObject json = JObject.Parse(File.ReadAllText(@"contacts.json"));
            JArray json = JArray.Parse(File.ReadAllText(@"stephen.json"));
            foreach (var contact in json)
            {
                //Parse the JSON object (contact)
                var doc = new Document();
                //doc.Add("rows0versionValue", contact["rows[0].versionValue"]);
                //doc.Add("rows0uuid", contact["rows[0].uuid"]);
                //doc.Add("rows0createdBy", contact["rows[0].createdBy"]);
                //doc.Add("rows0createdDate", contact["rows[0].createdDate"]);
                doc.Add("id", contact["id"]);
                //doc.Add("rows", contact["rows"].Select(item => item["rows[0].id"]).ToList());
                //doc.Add("rowsid", contact["rows[0].id"]);
                doc.Add("versionValue", contact["versionValue"]);
                doc.Add("name", contact["name"]);
                //doc.Add("page", contact["id"]);
                //doc.Add("total", contact["total"]);
                //doc.Add("records", contact["records"]);
                //doc.Add("id", contact["id"]);
                //doc.Add("versionValue", contact["versionValue"]);
                //doc.Add("uuid", contact["uuid"]);
                //doc.Add("createdBy", contact["createdBy"]);
                //doc.Add("createdDate", contact["createdDate"]);
                //doc.Add("lastModifiedBy", contact["lastModifiedBy"]);
                doc.Add("lastModifiedDate", contact["lastModifiedDate"]);
                //doc.Add("companyId", contact["company.id"]);
                //doc.Add("name", contact["name"]);
                //doc.Add("displayName", contact["displayName"]);
                //doc.Add("salutation", contact["salutation"]);
                doc.Add("firstName", contact["firstName"]);
                //doc.Add("middleName", contact["middleName"]);
                //doc.Add("lastName", contact["lastName"]);
                //doc.Add("nickName", contact["nickName"]);
                //doc.Add("suffix", contact["suffix"]);
                //doc.Add("genderId", contact["gender.id"]);
                //doc.Add("businessUnitId", contact["businessUnit.id"]);
                //doc.Add("dateOfBirth", contact["dateOfBirth"]);
                //doc.Add("accountingReference", contact["accountingReference"]);
                //doc.Add("referenceId", contact["referenceId"]);
                //doc.Add("label", contact["label"]);
                //doc.Add("description", contact["description"]);
                //doc.Add("alternatives", contact["alternatives"]);
                //doc.Add("value", contact["value"]);
                //doc.Add("subtag", contact["subtag"]);
                //doc.Add("iso639_3Tag", contact["iso639_3Tag"]);
                //doc.Add("type", contact["type"]);
                //doc.Add("alias", contact["alias"]);
                //doc.Add("enabled", contact["enabled"]);
                //doc.Add("rating", contact["rating"]);
                //doc.Add("parsedNumber", contact["parsedNumber"]);
                //doc.Add("addrEntered", contact["addrEntered"]);
                //doc.Add("addrFormatted", contact["addrFormatted"]);
                //doc.Add("cityTown", contact["cityTown"]);
                //doc.Add("stateCounty", contact["stateCounty"]);
                //doc.Add("postalCode", contact["postalCode"]);
                //doc.Add("active", contact["active"]);
                ///////////////////////////////////////////////////////////////////////////////////////
                //doc.Add("id", contact["id"]);
                //doc.Add("versionValue", contact["versionValue"]);
                //doc.Add("uuid", contact["uuid"]);
                //doc.Add("createdBy", contact["createdBy"]);
                //doc.Add("createdDate", contact["createdDate"]);
                //doc.Add("lastModifiedBy", contact["lastModifiedBy"]);
                //doc.Add("lastModifiedDate", contact["lastModifiedDate"]);
                //doc.Add("companyId", contact["company.id"]);
                //doc.Add("name", contact["name"]);
                //doc.Add("displayName", contact["displayName"]);
                //doc.Add("salutation", contact["salutation"]);
                //doc.Add("firstName", contact["firstName"]);
                //doc.Add("middleName", contact["middleName"]);
                //doc.Add("lastName", contact["lastName"]);
                //doc.Add("nickName", contact["nickName"]);
                //doc.Add("suffix", contact["suffix"]);
                //doc.Add("genderId", contact["gender.id"]);
                //doc.Add("businessUnitId", contact["businessUnit.id"]);
                //doc.Add("dateOfBirth", contact["dateOfBirth"]);
                //doc.Add("accountingReference", contact["accountingReference"]);
                //doc.Add("referenceId", contact["referenceId"]);
                //doc.Add("languageMappingId", contact["languageMappings"].Select(item => item["id"]).ToList());
                //doc.Add("languageMappingContactId", contact["languageMappings"].Select(item => item["contact.id"]).ToList());
                //doc.Add("languageMappingLanguageId", contact["languageMappings"].Select(item => item["language.id"]).ToList());
                //doc.Add("languageMappingCombined", contact["languageMappings"].Select(item => "id " + item["id"] + " contact.id " + item["contact.id"] + " language.id " + item["language.id"]).ToList());
                //doc.Add("languageMappingLanguageObjId", contact["languageMappings"].Select(item => item["language.id"]).ToList());
                //doc.Add("languageMappingLanguageObjLabel", contact["languageMappings"].Select(item => item["label"]).ToList());
                //doc.Add("language", contact["label"]);
                //doc.Add("languageMappingLanguageObjDescription", contact["languageMappings"].Select(item => item["description"]).ToList());
                //doc.Add("languageMappingLanguageObjAlternates", contact["languageMappings"].Select(item => item["alternates"]).ToList());
                //doc.Add("languageMappingLanguageObjValue", contact["languageMappings"].Select(item => item["value"]).ToList());
                //doc.Add("languageMappingLanguageObjSubtag", contact["languageMappings"].Select(item => item["subtag"]).ToList());
                //doc.Add("languageMappingLanguageObjIso639_3Tag", contact["languageMappings"].Select(item => item["iso639_3Tag"]).ToList());
                //doc.Add("languageMappingLanguageObjType", contact["languageMappings"].Select(item => item["type"]).ToList());
                //doc.Add("languageMappingLanguageObjAlias", contact["languageMappings"].Select(item => item["alias"]).ToList());
                //doc.Add("languageMappingLanguageObjEnabled", contact["languageMappings"].Select(item => item["enabled"]).ToList());
                //doc.Add("contactId", contact["contact.id"]);
                //doc.Add("page", contact["page"]);
                //doc.Add("total", contact["total"]);
                //doc.Add("records", contact["records"]);
                //doc.Add("rows0Id", contact["rows[0].id"]);
                //doc.Add("rows1Id", contact["rows[1].id"]);
                //doc.Add("name", contact["name"]);
                //doc.Add("company", contact["company"]);
                //doc.Add("locationsId", contact["locations"].Select(item => item["id"]).ToList());
                //doc.Add("locationsDescription", contact["locations"].Select(item => item["description"]).ToList());
                //doc.Add("locationsCombined", contact["locations"].Select(item => item["id"] + "||" + item["description"]).ToList());

                indexOperations.Add(IndexAction.Upload(doc));
            }

            try
            {
                indexClient.Documents.Index(new IndexBatch(indexOperations));
            }
            catch (IndexBatchException e)
            {
                // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                // the batch. Depending on your application, you can take compensating actions like delaying and
                // retrying. For this simple demo, we just log the failed document keys and continue.
                Console.WriteLine(
                "Failed to index some of the documents: {0}",
                       String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
            }

        }


        public static ContactSearch SearchDocuments(string searchText, string filter = null)
        {
            // Search using the supplied searchText and output documents that match 
            try
            {
                var sp = new SearchParameters();
                sp.IncludeTotalResultCount = true;
                if (!String.IsNullOrEmpty(filter))
                    sp.Filter = filter;
                sp.Facets = new List<String>() {  };

                var response = indexClient.Documents.Search(searchText, sp);
                return new ContactSearch() { Results = response.Results, Facets = response.Facets, Count = Convert.ToInt32(response.Count) };

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed search: {0}", e.Message.ToString());
                return null;
            }
        }

    }

    public class ContactSearch
    {
        public FacetResults Facets { get; set; }
        public IList<SearchResult> Results { get; set; }
        public int? Count { get; set; }

    }
}

