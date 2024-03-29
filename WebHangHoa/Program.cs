﻿using System.Net.Mime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using WebHangHoa.Data;
using WebHangHoa.Service;

var builder = WebApplication.CreateBuilder(args);
var secretkey = builder.Configuration.GetSection("AppSettings:SecretKey");
var secretBytes = Encoding.UTF8.GetBytes(secretkey.Value);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<HangHoaContext>(otp =>otp.UseSqlServer(builder.Configuration.GetConnectionString("Connect")));
builder.Services.AddScoped<ILoaiHangHoaRespository, LoaiHangHoaRespository>();
//AddScoped 
builder.Services.AddScoped<IHangHoaResponsitory,HangHoaResponsitory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:SecretKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
