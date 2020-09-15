using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public IMessageService _message { get; set; }

        public MessageController(IMessageService message)
        {
            _message = message;
        }


        public void Get()
        {
            Response.WriteAsync( _message.Text);
        }
    }
}
