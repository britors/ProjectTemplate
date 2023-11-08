﻿using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectTemplate.Infra.AutoMapper;

public static class AutoMapperExtension
{
    public static void AddAutoMapperApi(this IServiceCollection services, Assembly assembly)
    {
        var profiles = assembly.GetTypes()
            .Where(type => typeof(Profile).IsAssignableFrom(type))
            .ToList();

        foreach (var profile in profiles)
            services.AddAutoMapper(profile);
    }
}