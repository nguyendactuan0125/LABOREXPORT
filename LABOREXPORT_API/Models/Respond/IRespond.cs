using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LABOREXPORT_API.Models.Respond
{
    public class IRespond
    {
        [Required]
        public string p_result { get; set; }
        [Required]
        public string p_msg { get; set; }
        public List<Object> Data { get; set; }
    }
}