using System;
using System.Net;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

  


    public class ValuesController : ControllerBase
    {
        private readonly IDataPublisher dataPublisher;

        public ValuesController(IDataPublisher dataPublisher)
        {
            this.dataPublisher = dataPublisher; 
        }

        [HttpPost]
        public async Task post([FromBody] Precisely precisely)
        {
            await dataPublisher.PublishAsync(precisely);
        }
    }
}