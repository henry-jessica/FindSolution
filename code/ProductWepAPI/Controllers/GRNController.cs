using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductModel;
using System;
using System.Collections.Generic;


namespace ProductWepAPI.Controllers

{
    [Route("[controller]")]
     public class GRNController : ControllerBase
    {
        private readonly IGRN<GRN> _repository;
        public GRNController(IGRN<GRN> repository)
        {
            _repository = repository;
        }
        // Must decorate for swagger
        [HttpGet]
        public IEnumerable<GRN> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet("id/{id}")]
        public GRN GetById(int id)
        {
            return _repository.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] GRN p)
        {
            _repository.Add(p);

        }
    }
}
