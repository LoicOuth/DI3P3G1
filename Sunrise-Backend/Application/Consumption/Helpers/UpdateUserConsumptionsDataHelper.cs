using Application.Common.Interfaces;
using Application.Common.Interfaces.Helpers;
using Domain.Entities;
using Domain.Model;

namespace Application.Consumption.Helpers;

public class UpdateUserConsumptionsDataHelper : IUpdateConsumptionsDataHelper
{
	private readonly IRepositoryUser _user;

	public UpdateUserConsumptionsDataHelper(IRepositoryUser user)
	{
		_user = user;
	}

	public async Task<double> Helper(UserDTO usr)
	{
		var user = await _user.GetUserAsync(usr.Id);
		if (user.ConsumptionsData != null)
		{
			if (user.ConsumptionsData.date.ToShortDateString() == DateTime.Now.ToShortDateString())
			{
				user.ConsumptionsData.consumtion += GetRandomNumber(0, 0.00001);
				await _user.UpdateAsync(user);
				return user.ConsumptionsData.consumtion;
			}
		}

		user.ConsumptionsData = new UserConsumptionsData()
			{ consumtion = GetRandomNumber(0, 0.01), date = DateTime.Now };
		await _user.UpdateAsync(user);
		return user.ConsumptionsData.consumtion;
	}
	private  double GetRandomNumber(double minimum, double maximum)
	{ 
		Random random = new Random();
		return random.NextDouble() * (maximum - minimum) + minimum;
	}
}