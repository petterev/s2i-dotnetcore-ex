using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventcodeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EventcodeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select ""Index"" as ""Id"",
                        ""Description"" as ""Description""
                from ""Event_codes""
            ";

       


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SamAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();

                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }


    }
}
