﻿using BusinessLayer.Autho;
using BusinessLayer.Services;
using BusinessLayer.Services.EmailSender;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer;
using AutoMapper;
using BusinessLayer.Mapping;
using BusinessLayer.Services.CoreServices;

namespace BusinessLayer.Helper
{
	public static class BusinessServicesHelper
	{
		public static IServiceCollection AddDependencies(this IServiceCollection services)
		{
            services.AddScoped<IUserService, UserService>();
			services.AddScoped<IJwtTokenHandlerService, JwtTokenHandlerService>();
			services.AddScoped<IEmailVerificationTokenService, EmailVerificationTokenService>();
			services.AddScoped<IEmailSenderService, EmailSenderService>();
			services.AddScoped<IEmailVerificationService, EmailVerificationService>();
			services.AddScoped<ISearchEngineService, SearchEngineService>();	


			services.AddScoped<Profile, MappingProfile>();

			// repository layer
			services.AddScoped<IRegistrationRepository, RegistrationRepository>();
			services.AddScoped<ISearchEngineRepository, SearchEngineRepository>();	

			return services;
		}
	}
}
