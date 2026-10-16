using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_services.Masters.Projects;
using ngn_services.Masters.Statuses;
using ngn_services.Masters.Severities;
using ngn_services.Masters.Equipment;
using ngn_services.Masters.EquipmentPartNumbers;
using ngn_services.Masters.FailureSymptoms;
using ngn_services.Masters.NatureOfProblems;
using ngn_services.Masters.CallTypes;
using ngn_services.Masters.CircleByZones;
using ngn_services.Masters.Circles;
using ngn_services.ProjectGroups;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProjectGroupService, ProjectGroupService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<ISeverityService, SeverityService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IEquipmentPartNumberService, EquipmentPartNumberService>();
builder.Services.AddScoped<IFailureSymptomsService, FailureSymptomsService>();
builder.Services.AddScoped<INatureOfProblemService, NatureOfProblemService>();
builder.Services.AddScoped<ICallTypeService, CallTypeService>();
builder.Services.AddScoped<ICircleByZoneService, CircleByZoneService>();
builder.Services.AddScoped<ICircleService, CircleService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
