using Application;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductionController : Controller
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	private readonly IRepositoryIoTData _data;
	public ProductionController(IMediator mediator , IMapper mapper, IRepositoryIoTData data)
	{
		_mediator = mediator;
		_mapper = mapper;
		_data = data;
	}

	[Authorize]
	[HttpGet("yesterday")]
	public async Task<IActionResult> getYesterdayProduction()
	{
		try
		{ 
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			var productions = await _data.GetAnyDataAsync();
			List<ProductionInfo> prodUser = new List<ProductionInfo>();
			productions = productions.Where(p => p.IoTHub.ConnectionDeviceId == usr.DeviceId);
			DateTime yesterday = DateTime.Now.AddDays(-1);
			productions = productions.Where(p => p.EventEnqueuedUtcTime.ToShortDateString() == yesterday.ToShortDateString());
			for (int i = 0; i < 24; i++)
			{
				var prods = productions.Where(p => p.EventEnqueuedUtcTime.Hour == i);
				double value = 0;
				foreach (var prod in prods)
				{
						
					value += prod.voltage/400;
				}
				prodUser.Add(new ProductionInfo($"{i}H", value));
			}
			
			return Ok(prodUser);
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (NotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception)
		{
			return new StatusCodeResult(500);
		}
	}
	[Authorize]
	[HttpGet("current")]
	public async Task<IActionResult> getCurrenProduction()
	{
		try
		{ 
			UserDTO usr = _mapper.Map<UserDTO>(HttpContext.Items["User"]);
			//Instane
			double currentProd = await _mediator.Send(new GetInstantProductionQuery(usr));
			double currentCons = GetRandomNumber(0, 100);

			
			//Calc
			double currentConsumption = await _mediator.Send(new UpdateUserConsumptionsDataCommand(usr));
			double production = await _mediator.Send(new GetCurrentProductionQuery(usr));
			double autonomy = calcAutonomy(production,currentConsumption);
			ProductionInstant prd = new ProductionInstant()
				{ ProductionInstantane = currentProd, TauxAutonomie = autonomy , ConsommationInstantane = currentCons};
			
			return Ok(prd);
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (NotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception)
		{
			return new StatusCodeResult(500);
		}
	}

	private double calcAutonomy(double prod, double consum)
	{
		if (consum >= prod)
		{
			return 0;
		}
		
		return 100 - ((consum / prod) * 100) ;
	}
	private  double GetRandomNumber(double minimum, double maximum)
	{ 
		Random random = new Random();
		return random.NextDouble() * (maximum - minimum) + minimum;
	}
}