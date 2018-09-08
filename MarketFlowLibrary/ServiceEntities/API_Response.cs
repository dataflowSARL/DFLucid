﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFLibrary.API
{
    public class API_Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> Content { get; set; }

    }
}