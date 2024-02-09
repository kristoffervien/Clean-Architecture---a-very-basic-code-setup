using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Common;
using CleanArch.Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimantController : ControllerBase
    {
        private readonly ClaimantRepository claimantRepository;

        public ClaimantController(ClaimantRepository ClaimantService)
        {
            claimantRepository = ClaimantService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClaimantViewModel claimantViewModel)
        {

            await claimantRepository.AddAsync(new Claimant
            {
                Note = claimantViewModel.Note,
                UserId = claimantViewModel.UserId

            });


            return Ok(claimantViewModel);
        }
       

        //[HttpGet("get/{Id}")]
        //public async Task<IActionResult> Get(int Id)
        //{
        //   var ClaimantViewModel = await _ClaimantService.GetClaimant(Id);

        //    return Ok(ClaimantViewModel);
        //}


    }
}
