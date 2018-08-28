using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitLottery.Api.Models
{
  public class DrawConfiguration
  {
    public DateTime DrawDate { get; set; }
    public int NumberOfBallots { get; set; }
  }
}
