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
  public class EventController : ControllerBase
  {

    private readonly IConfiguration _configuration;
    public EventController(IConfiguration configuration)
    {
      _configuration = configuration;
    }


    [HttpPost]
    public JsonResult Post(Event ev)
    {
      string query = @"
                insert into ""Events"" (""UserId"",""Timestamp"",""Vehicle"",""Code"") 
                values               (@UserId,@Timestamp,@Vehicle,@Code) 
            ";

      DataTable table = new DataTable();
      string sqlDataSource = _configuration.GetConnectionString("SamAppCon");
      NpgsqlDataReader myReader;
      using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
      {
        myCon.Open();
        using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
        {

          myCommand.Parameters.AddWithValue("@UserId", ev.UserId);
          myCommand.Parameters.AddWithValue("@Timestamp", Convert.ToDateTime(ev.Timestamp));
          myCommand.Parameters.AddWithValue("@Vehicle", Convert.ToDateTime(ev.Vehicle));
          myCommand.Parameters.AddWithValue("@Code", ev.Code);
          myReader = myCommand.ExecuteReader();
          table.Load(myReader);

          myReader.Close();
          myCon.Close();

        }
      }

      return new JsonResult("Added Successfully");
    }





  }
}
