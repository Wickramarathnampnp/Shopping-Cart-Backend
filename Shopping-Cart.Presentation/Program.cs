using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shopping_Cart.Application.Service;
using Shopping_Cart.Core.Interfaces;
using Shopping_Cart.Infra.Background_Service;
using Shopping_Cart.Infra.Middleware;
using Shopping_Cart.Infra.Repository;
using Shopping_Cart.Infra.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Authority = "https://dev-83lrx10605yjwz0h.us.auth0.com/";
		options.Audience = "https://www.novestra2.us/";
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true
		};
	});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dependency injection 
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped< IDeliveryDetailsRepository , DeliveryDetailsRepository > ();
builder.Services.AddScoped<IDeliveryDetailsService, DeliveryDetailsService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();

//Add service to background service
builder.Services.AddSingleton<IEmailQueueService,EmailQueueService>();
builder.Services.AddSingleton<IEmailService,EmailService>();
//Add background service
builder.Services.AddHostedService<EmailBackgroundService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseCors(options => {
	options.AllowAnyOrigin();
	options.AllowAnyMethod();
	options.AllowAnyHeader();

});

app.UseAuthorization();

app.MapControllers();

app.Run();
