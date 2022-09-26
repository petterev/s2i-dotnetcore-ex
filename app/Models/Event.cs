using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
  public class Event
  {

    public int Id { get; set; }

    public string UserId { get; set; }

    public string Timestamp { get; set; }

    public string Vehicle { get; set; }

    public int Code { get; set; }


  }
}
