using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPITesting.Models;

namespace WebAPITesting.Controllers
{
    public class Testing: ApiController
    {
        private List<RegisterCategory> RegisterCategories;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [Route("api/GetCategoriesTest")]
        public IHttpActionResult GetCategories()
        {
            IHttpActionResult response = null;

            if (RegisterCategories == null)
            {
                RegisterCategories = new List<RegisterCategory>();
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

                    RegisterCategories.Add(category);
                }
            }
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
    }
}
