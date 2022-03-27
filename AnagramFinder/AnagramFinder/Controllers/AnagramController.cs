using AnagramFinder.DTOs;
using Logic.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AnagramFinder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnagramController : ControllerBase
    {
        private readonly IAnagramFinderLogic _anagramFinderLogic;
        
        public AnagramController(IAnagramFinderLogic anagramFinderLogic)
        {
            _anagramFinderLogic = anagramFinderLogic;
        }

        [HttpPost]
        public IActionResult GetAllByWord(SearchTermDTO searchTerm)
        {
            try
            {
                return Ok(_anagramFinderLogic.GetAnagramsByWord(searchTerm.Word));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
    