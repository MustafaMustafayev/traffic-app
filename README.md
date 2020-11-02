# traffic-app

1. How to migrate entities to db?
# Step 1:
go to traffic-app.DAL using package manager console:
# cd traffic-app.DAL

# Step 2:
Create migration:
# dotnet ef --startup-project ../traffic-app.API migrations add users1 --context TrafficDbContext

# Step 3:
Update db:
# dotnet ef --startup-project ../traffic-app.API database update users1 --context TrafficDbContext
