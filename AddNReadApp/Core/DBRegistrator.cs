using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace AddNReadApp.Core
{
	internal class DBRegistrator
	{
		public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
			.AddDBContext();
		;
	}
}
