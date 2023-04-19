﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.DTO
{
    public class Topics
    {        
        public List<string> GetTopicsHeaders { get; set; }
        public List<Topic> GetTopics { get; set; }
        public Topics()
        {
            if(GetTopicsHeaders == null)
            {
                GetTopicsHeaders = new List<string>
                {
                    "Id",
                    "MyTopics",
                    "Status",
                    "topicCode",
                    "createdBy",
                    "created",
                    "modifiedBy",
                    "modified"
                };
            }
        }
    }
}
