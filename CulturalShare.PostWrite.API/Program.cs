using CulturalShare.PostWrite.API.Configuration.Base;
using CulturalShare.PostWrite.API.DependencyInjection;
using CulturalShare.PostWrite.API.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.InstallServices(typeof(IServiceInstaller).Assembly);

var app = builder.Build();

// Seed database.
//using (var scope = app.Services.CreateScope())
//{
//    try
//    {
//        var dbContextDealerPortal = scope.ServiceProvider.GetRequiredService<PostWriteDBContext>();
//        for (int i = 0; i < 10000; i++)
//        {
//            dbContextDealerPortal.Posts.Add(new PostEntity()
//            {
//                Caption = "test",
//                CreatedAt = DateTime.UtcNow,
//                ImageUrl = "AAA",
//                Likes = 0,
//                OwnerId = 1,
//                Comments = new List<CommentEntity>()
//                {
//                    new()
//                    {
//                        OwnerId = 1,
//                        Text = "test",
//                        CreatedAt = DateTime.UtcNow,
//                        Username = "test",
//                    }
//                }
//            });

//            dbContextDealerPortal.SaveChanges();

//            Thread.Sleep(1000);
//        }        
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//        throw ex;
//    }
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGrpcService<PostsWriteService>();

app.UseAuthorization();

app.MapHealthChecks("/_health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

app.Run();
