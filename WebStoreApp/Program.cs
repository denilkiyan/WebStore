using Microsoft.EntityFrameworkCore;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Application.Services;
using WebStoreApp.Infrastructure.DBContext;
using WebStoreApp.Infrastructure.Repositories;
using WebStoreApp.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using WebStoreApp.Application.Services.Auth;
using WebStoreApp.Application.Services.Auth.Extension;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { 
     In=ParameterLocation.Header,
     Description="Please enter token",
     Name="Authorization",
     Type=SecuritySchemeType.Http,
     BearerFormat="JWT",
     Scheme="bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { 
         new OpenApiSecurityScheme
         { 
           Reference=new OpenApiReference
           { 
            Type =ReferenceType.SecurityScheme,
            Id="Bearer"
           }         
         },
         new string[]{ }
        }
    });
});
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddScoped<IProductsRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
