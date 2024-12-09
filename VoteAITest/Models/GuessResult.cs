using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteAITest.Models
{
    public class GuessResult
    {
        public int slot { get; set; }
        public string guess { get; set; } = null!;
        public string result { get; set; } = null!;
    }
}
