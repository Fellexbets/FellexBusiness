


var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccountPersistence, AccountPersistence>();
builder.Services.AddScoped<IMovementPersistence, MovementPersistence>();
builder.Services.AddScoped<ITransferPersistence, TransferPersistence>();
builder.Services.AddScoped<IUserPersistence, UserPersistence>();
builder.Services.AddScoped<ISessionPersistence, SessionPersistence>();
builder.Services.AddScoped<IAccountBusiness, AccountBusiness>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();
builder.Services.AddScoped<IMovementBusiness, MovementBusiness>();
builder.Services.AddScoped<ITransferBusiness, TransferBusiness>();
builder.Services.AddScoped<ISessionBusiness, SessionBusiness>();
builder.Services.AddTransient<IBasePersistence<User>, UserPersistence>();
builder.Services.AddTransient<IBasePersistence<Account>, AccountPersistence>();
builder.Services.AddTransient<IBasePersistence<Transfer>, TransferPersistence>();
builder.Services.AddTransient<IBasePersistence<Movement>, MovementPersistence>();
builder.Services.AddTransient<IBasePersistence<Session>, SessionPersistence>();
builder.Services.AddSingleton<IJwtServices, JwtServices>();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});


builder.Services.AddAuthentication(configureOptions: x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(configuration["Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseRouting();
app.UseDeveloperExceptionPage();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseAuthorization();

app.MapControllers();

app.Run();
