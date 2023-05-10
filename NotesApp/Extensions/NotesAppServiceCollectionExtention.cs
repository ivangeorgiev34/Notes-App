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
            //services.AddScoped<IStudentService, StudentService>();
            //services.AddScoped<IClassService, ClassService>();
            //services.AddScoped<ISubjectService, SubjectService>();
            //services.AddScoped<ITeacherService, TeacherService>();
            //services.AddScoped<IProfileService, ProfileService>();
            //services.AddScoped<IGradeService, GradeService>();
            //services.AddScoped<IRemarkService, RemarkService>();
            //services.AddScoped<IComplimentService, ComplimentService>();

            return services;
        }

    }
}
