using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Services;
using Service.Validators;

namespace GestaoDeClientesWebAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ClientesController : ControllerBase
    {
        private IBaseService<Cliente> _baseClienteService;

        public ClientesController(IBaseService<Cliente> baseClienteService, IMapper mapper)
        {
            _baseClienteService = baseClienteService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClienteModel cliente, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            if (cliente == null)
                return BadRequest("Parametros fornecidos não correspondem ao tipo cliente");

            AddModelErrorsAdd<ClienteModel, CreateClienteValidator<ClienteModel>>(cliente);

            if (ModelState.IsValid)
                try
                {
                    var inserted = _baseClienteService.Add<ClienteModel, ClienteModel>(cliente);
                    return CreatedAtAction(nameof(GetById), new { id = inserted.Id }, inserted);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }

            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ClienteModel cliente, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            if (cliente == null)
                return BadRequest("Parametros fornecidos não correspondem ao tipo cliente");
            if (id != cliente.Id)
            {
                ModelState.AddModelError("id", "Route parameter id deve ser igual ao Body id");
                return BadRequest(ModelState);
            }
            if (_baseClienteService.GetById<ClienteModel>(id) == null)
                return NotFound();

            AddModelErrorsAdd<ClienteModel, UpdateClienteValidator<ClienteModel>>(cliente);

            if (ModelState.IsValid)
                try
                {
                    try
                    {
                        var updated = _baseClienteService.Update<ClienteModel, ClienteModel>(cliente);
                        return Ok(updated);
                    }
                    catch (Exception e)
                    {
                        if (_baseClienteService.GetById<ClienteModel>(id) == null)
                            return NotFound();
                        ModelState.AddModelError("error", e.Message);
                        return BadRequest(ModelState);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                }

            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("id", "id deve ser maior que zero");
                return BadRequest(ModelState);
            }
            try
            {
                var cliente = _baseClienteService.GetById<ClienteModel>(id);

                if (cliente == null)
                    return NotFound();

                _baseClienteService.Delete(id);

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(
                _baseClienteService.Get<ClienteModel>());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("id", "id deve ser maior que zero");
                return BadRequest(ModelState);
            }
            try
            {
                var cliente = _baseClienteService.GetById<ClienteModel>(id);
                if (cliente != null)
                {
                    return Ok(
                        cliente
                    );
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        private void AddModelErrorsAdd<TInputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TValidator : IBaseValidator<TInputModel>
        {
            var validator = Activator.CreateInstance<TValidator>();
            var results = validator.Validate(inputModel);
            if (results.Count>0)
            {
                foreach (var validationResult in results)
                {
                    ModelState.AddModelError(validationResult.MemberName, validationResult.ErrorMessage);
                }
            }
        }
    }
}
