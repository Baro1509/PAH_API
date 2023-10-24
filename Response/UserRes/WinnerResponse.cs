﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respon.UserRes
{
    public class WinnerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? ProfilePicture { get; set; }
        public int? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public decimal? FinalBid { get; set; }
    }
}
