using System.Net;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.Infrastructure.Services;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OCRController : Controller
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	private readonly IAzureCognitiveService _azureCognitive;
	private readonly ILogger<OCRController> _logger;
	public OCRController(IMediator mediator, IMapper mapper, IAzureCognitiveService azureCognitive,ILogger<OCRController> logger)
	{
		_mediator = mediator;
		_mapper = mapper;
		_azureCognitive = azureCognitive;
		_logger = logger;
	}

	[Authorize]
	[HttpPost]
	public async Task<IActionResult> getTextFromImage([FromForm]IFormFile file)
	{
		try
		{
			//Save Image
			string FileName = file.FileName;
			string uniqueFileName = Guid.NewGuid().ToString() + "." + FileName.Split('.')[1];
			var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", uniqueFileName);
			file.CopyTo(new FileStream(imagePath, FileMode.Create));
			
			//Get Text from image
			string text = await _azureCognitive.ImageToText(uniqueFileName);
			
			//Delete Image
			System.IO.File.Delete(uniqueFileName);
			
			return Ok(text);
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (NotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception e)
		{
			_logger.LogError(e.ToString());
			return new StatusCodeResult(500);
		}
	}
}