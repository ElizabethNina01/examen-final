using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyJob.API.Payments.Domain.Models;
using EasyJob.API.Payments.Domain.Services;
using EasyJob.API.Payments.Domain.Services.Communication;
using EasyJob.API.Payments.Resources;
using Go2Climb.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyJob.API.Payments.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class PaymentController: ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All payments",
            Description = "Get All payments already stored",
            Tags = new[] {"Payments"})]
        public async Task<IEnumerable<PaymentResource>> GetAllAsync()
        {
            var payments = await _paymentService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentResource>>(payments);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get Payment By Id",
            Description = "Get A Payment From The Database Identified By Its Id.",
            Tags = new[] {"Payments"})]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _paymentService.GetById(id);
        
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Register A payment",
            Description = "Add A payment to Database.",
            Tags = new[] {"Payments"})]
        public async Task<IActionResult> PostAsync([FromBody] SavePaymentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var payment = _mapper.Map<SavePaymentResource, Payment>(resource);
            var result = await _paymentService.SaveAsync(payment);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var paymentResource = _mapper.Map<Payment, PaymentResource>(result.Resource);

            return Ok(paymentResource);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edit A payment",
            Description = "Edit A payment of the Database.",
            Tags = new[] {"Payments"})]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePaymentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var payment = _mapper.Map<SavePaymentResource, Payment>(resource);

            var result = await _paymentService.UpdateAsync(id, payment);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var paymentResource = _mapper.Map<Payment, PaymentResource>(result.Resource);

            return Ok(paymentResource);
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete A payment",
            Description = "Delete A payment of the Database.",
            Tags = new[] {"Payments"})]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _paymentService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var paymentResource = _mapper.Map<Payment, PaymentResource>(result.Resource);
            
            return Ok(paymentResource);
        }   
    }
}