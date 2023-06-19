using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductMvc.Data;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

#region Fixed Window RateLimiter
//builder.Services.AddRateLimiter(options =>
//{
//    //options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
//    options.OnRejected = async (context, token) =>
//    {
//        context.HttpContext.Response.StatusCode = 429;
//        if(context.Lease.TryGetMetadata(MetadataName.RetryAfter,out var retryAfter))
//        {
//            await context.HttpContext.Response.WriteAsync($"Too many requests. Please retry after {retryAfter.TotalSeconds} seconds");
//        }
//    };
//    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//    RateLimitPartition.GetFixedWindowLimiter(
//        partitionKey:httpContext.User.Identity?.Name?? httpContext.Request.Headers.Host.ToString(),
//        factory: partition=>new FixedWindowRateLimiterOptions
//        {
//            AutoReplenishment=true,
//            PermitLimit=10,
//            QueueLimit=0,
//            Window=TimeSpan.FromSeconds(100)
//        }));
//});
#endregion
#region Sliding Window RateLimiter
//builder.Services.AddRateLimiter(options =>
//{
//    options.OnRejected = async (context, token) =>
//    {
//        context.HttpContext.Response.StatusCode = 429;
//        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
//        {
//            await context.HttpContext.Response.WriteAsync($"Too many requests. Please retry after {retryAfter.TotalSeconds} seconds");
//        }
//    };
//    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//    RateLimitPartition.GetSlidingWindowLimiter(
//        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//        factory: partition => new SlidingWindowRateLimiterOptions
//        {
//            AutoReplenishment = true,
//            PermitLimit = 60,
//            SegmentsPerWindow=10,
//            QueueLimit = 0,
//            Window = TimeSpan.FromSeconds(100)
//        }));
//});
#endregion
#region TokenBucket RateLimiter
//builder.Services.AddRateLimiter(options =>
//{
//    options.OnRejected = async (context, token) =>
//    {
//        context.HttpContext.Response.StatusCode = 429;
//        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
//        {
//            await context.HttpContext.Response.WriteAsync($"Too many requests. Please retry after {retryAfter.TotalSeconds} seconds");
//        }
//    };
//    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//    RateLimitPartition.GetTokenBucketLimiter(
//        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//        factory: partition => new TokenBucketRateLimiterOptions
//        {
//            AutoReplenishment = true,
//            TokenLimit=10,
//            TokensPerPeriod=3,
//            QueueLimit = 0,
//            ReplenishmentPeriod=TimeSpan.FromSeconds(10)
//        }));
//});
#endregion


// Add services to the container.




builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRateLimiter();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();