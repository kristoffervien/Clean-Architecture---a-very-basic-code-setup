﻿using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Domain.CommandHandlers;
using CleanArch.Domain.Commands;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Domain.Queries;
using CleanArch.Domain.QueriesHandlers;
using CleanArch.Infra.Bus;
using CleanArch.Infra.Data.Common;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain InMemoryBus MediatR
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            //Domain Handlers
            services.AddScoped<IRequestHandler<CreateCourseCommand, bool>, CourseCommandHandler>();

            services.AddScoped<IRequestHandler<CreateCourseQuery, Course>, CourseQueryHandler>();

            //Application Layer 
            services.AddScoped<ICourseService, CourseService>();

            //Infra.Data Layer
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<BaseRepository<Claimant>, ClaimantRepository>(x =>

             new ClaimantRepository(dbContext: x.GetRequiredService<UniversityDBContext>())

            );

            services.AddScoped<UniversityDBContext>();
        }
    }
}
