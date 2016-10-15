using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Sql;
using System.Data.SqlClient;
using WebAPITesting.Models;
using System.Data;
using System.Net.Mime;

namespace WebAPITesting.Controllers
{
    // this attribute allows that routes to this controller will start with api/
    // inidividual function Route attributes will be appended to this root url
    [RoutePrefix("api")]
    public class RegisterDataAPI : ApiController
    {

        /// </summary>
        /// <returns></returns>
		[HttpGet()]
		[Route("GetCategories")]
		public IHttpActionResult GetCategories()
		{
			IHttpActionResult response = null;
            
            if (RegisterCategories.Count > 0)
		    {
		        response = Ok(RegisterCategories);
		    }
		    else
		    {
		        response = NotFound();
		    }
			
			return response;
		}

        /// <summary>
        /// WebAPI really doesn't like to get multiple parameters 
        /// except through the query string or form submissions - not in the body of the request
        /// In order to best get parameters (especially multiple parameters sent) through the body, best to create a model
        /// (in this case the CategoryID) and pass as a json string in the body
        /// </summary>
        /// <remarks>
        /// POST http://localhost:3333/registerappapi/api/GetCategory HTTP/1.1
        ///    User-Agent: Fiddler
        ///    Host: localhost:3333
        ///    Content-Length: 16
        ///    Content-Type: application/json
        ///    {"categoryID":1}
        /// </remarks>
        /// <param name="categoryIDObject"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("GetCategory")]
        public IHttpActionResult GetCategory([FromBody]CategoryID categoryIDObject)
        {
            IHttpActionResult response = null;

            response = NotFound();
            if (RegisterCategories.Count > 0)
            {
                var category = RegisterCategories.FirstOrDefault(c => c.CategoryID == categoryIDObject.categoryID);
                if (category != null)
                {
                    response = Ok(category);                    
                }
            }

            return response;
        }

        
        
        
        [HttpPost()]
        [Route("GetRegisterEntries")]
        public IHttpActionResult GetRegisterEntries()
        {
            IHttpActionResult response = null;

            var registerEntries = new List<MainRegisterEntry>();
            var command = GetCommand("usp_GetRegisterEntries", CommandType.StoredProcedure);
            command.Connection.Open();
            var returnDataset = new DataSet();
            var dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(returnDataset);

            foreach (DataRow mainEntryRow in returnDataset.Tables[0].Rows)
            {
                var foundDetails = (from DataRow detailRow in returnDataset.Tables[1].Rows
                    select new DetailRegisterEntry()
                    {
                        RegisterDetailID = detailRow.Field<int>("RegisterDetailID"),
                        RegisterMainEntryID = detailRow.Field<int>("EntryID"),
                        RegisterCategoryID = detailRow.Field<int>("CategoryID"),
                        EntryAmount = detailRow.Field<decimal>("Amount")

                    }).ToList();

                var registerEntry = new MainRegisterEntry()
                {
                    EntryID = mainEntryRow.Field<int>("EntryID"),
                    EntryDate = Convert.ToString(mainEntryRow.Field<DateTime>("EntryDate")),
                    AccountID = mainEntryRow.Field<int>("AccountID"),
                    Payee = mainEntryRow.Field<string>("Payee"),
                    EntryDesc = mainEntryRow.Field<string>("EntryDesc") ?? "",
                    CheckNumber = mainEntryRow.Field<string>("CheckNumber") ?? "",
                    TotalAmount = mainEntryRow.Field<decimal>("TotalAmount"),
                    EntryDetails = foundDetails
                };

                registerEntries.Add(registerEntry);
            }

            if (registerEntries.Count > 0)
            {
                response = Ok(registerEntries);
            }
            else
            {
                response = NotFound();
            }

            return response;
        }
        

        private SqlCommand GetCommand(string commandName, CommandType commandType)
        {
            var connection = new SqlConnection(WebApiConfig.dbConnectionString);
            var command = new SqlCommand()
            {
                CommandText = commandName,
                Connection = connection,
                CommandType = commandType
            };
            return command;
        }

        private List<RegisterCategory> _registerCategories; 
        public List<RegisterCategory> RegisterCategories
        {
            get
            {
                if (_registerCategories == null)
                {
                    _registerCategories = new List<RegisterCategory>();
                    var command = GetCommand("usp_GetCategories", CommandType.StoredProcedure);
                    command.Connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var category = new RegisterCategory
                        {
                            MainCategoryID = reader.GetInt32(reader.GetOrdinal("MainCategoryID")),
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            CategoryName = reader.GetString(reader.GetOrdinal("Category")),
                            CategoryTypeID = reader.GetInt32(reader.GetOrdinal("CategoryTypeID")),
                            CategoryType = reader.GetString(reader.GetOrdinal("CategoryType"))
                        };

                        _registerCategories.Add(category);
                    }
            }

            return (_registerCategories);
            }
            
        }

        
    }

    public class CategoryID
    {
        public int categoryID { get; set; }
    }
   
}
