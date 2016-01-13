using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ValuesController : ApiController
    {
       
        public  FileSistem Get()
        {
            var FS = new FileSistem();
            return FS;
        }
        public FileSistem Get(string name)
        {
            var FS = new FileSistem(name);
            return FS;
        }

       
    }
}
