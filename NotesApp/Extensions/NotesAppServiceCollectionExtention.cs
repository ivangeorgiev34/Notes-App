using NotesApp.Core.Contracts;
using NotesApp.Core.Services;
using NotesApp.Infrastructure.Common;

namespace NotesApp.Extensions
{
    public static class NotesAppServiceCollectionExtention
    {
        /// <summary>
		/// Adds all the services to the IoC of the application
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
        /// 

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();

            return services;
        }

    }
}
